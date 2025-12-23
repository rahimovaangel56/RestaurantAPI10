using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI10.Data;
using RestaurantAPI10.DTOs.CustomerDTO;
using RestaurantAPI10.Models;

namespace RestaurantAPI10.Services
{
    /// <summary>
    /// Сервис для работы с клиентами
    /// </summary>
    public class CustomerService : ICustomerService
    {
        private readonly RestaurantDbContext _context;
        private readonly IMapper _mapper;

        /// <summary>
        /// Конструктор сервиса
        /// </summary>
        public CustomerService(RestaurantDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Получить всех клиентов
        /// </summary>
        public async Task<IEnumerable<CustomerReadDto>> GetAllCustomersAsync()
        {
            try
            {
                var customers = await _context.Customers
                    .OrderBy(c => c.Name)
                    .ToListAsync();

                return _mapper.Map<IEnumerable<CustomerReadDto>>(customers);
            }
            catch (Exception ex)
            {
                throw new Exception("Ошибка при получении списка клиентов", ex);
            }
        }

        /// <summary>
        /// Получить клиента по ID
        /// </summary>
        public async Task<CustomerReadDto> GetCustomerByIdAsync(int id)
        {
            try
            {
                var customer = await _context.Customers
                    .FirstOrDefaultAsync(c => c.Id == id);

                if (customer == null)
                    throw new KeyNotFoundException($"Клиент с ID {id} не найден");

                return _mapper.Map<CustomerReadDto>(customer);
            }
            catch (KeyNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception($"Ошибка при получении клиента с ID {id}", ex);
            }
        }

        /// <summary>
        /// Создать нового клиента
        /// </summary>
        public async Task<CustomerReadDto> CreateCustomerAsync(CustomerCreateDto customerCreateDto)
        {
            try
            {
                var customer = _mapper.Map<Customer>(customerCreateDto);

                _context.Customers.Add(customer);
                await _context.SaveChangesAsync();

                return _mapper.Map<CustomerReadDto>(customer);
            }
            catch (Exception ex)
            {
                throw new Exception("Ошибка при создании клиента", ex);
            }
        }

        /// <summary>
        /// Обновить клиента
        /// </summary>
        public async Task UpdateCustomerAsync(int id, CustomerCreateDto customerUpdateDto)
        {
            try
            {
                var customer = await _context.Customers
                    .FirstOrDefaultAsync(c => c.Id == id);

                if (customer == null)
                    throw new KeyNotFoundException($"Клиент с ID {id} не найден");

                _mapper.Map(customerUpdateDto, customer);
                await _context.SaveChangesAsync();
            }
            catch (KeyNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception($"Ошибка при обновлении клиента с ID {id}", ex);
            }
        }

        /// <summary>
        /// Удалить клиента
        /// </summary>
        public async Task DeleteCustomerAsync(int id)
        {
            try
            {
                var customer = await _context.Customers
                    .FirstOrDefaultAsync(c => c.Id == id);

                if (customer == null)
                    throw new KeyNotFoundException($"Клиент с ID {id} не найден");

                _context.Customers.Remove(customer);
                await _context.SaveChangesAsync();
            }
            catch (KeyNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception($"Ошибка при удалении клиента с ID {id}", ex);
            }
        }
    }
}