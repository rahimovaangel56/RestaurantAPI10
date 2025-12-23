using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI10.Data;
using RestaurantAPI10.DTOs.OrderDTO;
using RestaurantAPI10.Models;

namespace RestaurantAPI10.Services
{
    /// <summary>
    /// Сервис для работы с заказами
    /// </summary>
    public class OrderService : IOrderService
    {
        private readonly RestaurantDbContext _context;
        private readonly IMapper _mapper;

        /// <summary>
        /// Конструктор сервиса
        /// </summary>
        public OrderService(RestaurantDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Получить все заказы
        /// </summary>
        public async Task<IEnumerable<OrderReadDto>> GetAllOrdersAsync()
        {
            try
            {
                var orders = await _context.Orders
                    .Include(o => o.Customer)
                    .Include(o => o.OrderItems)
                        .ThenInclude(oi => oi.Dish)
                    .OrderByDescending(o => o.OrderDate)
                    .ToListAsync();

                return _mapper.Map<IEnumerable<OrderReadDto>>(orders);
            }
            catch (Exception ex)
            {
                throw new Exception("Ошибка при получении списка заказов", ex);
            }
        }

        /// <summary>
        /// Получить заказ по ID
        /// </summary>
        public async Task<OrderReadDto> GetOrderByIdAsync(int id)
        {
            try
            {
                var order = await _context.Orders
                    .Include(o => o.Customer)
                    .Include(o => o.OrderItems)
                        .ThenInclude(oi => oi.Dish)
                    .FirstOrDefaultAsync(o => o.Id == id);

                if (order == null)
                    throw new KeyNotFoundException($"Заказ с ID {id} не найден");

                return _mapper.Map<OrderReadDto>(order);
            }
            catch (KeyNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception($"Ошибка при получении заказа с ID {id}", ex);
            }
        }

        /// <summary>
        /// Создать новый заказ
        /// </summary>
        public async Task<OrderReadDto> CreateOrderAsync(OrderCreateDto orderCreateDto)
        {
            try
            {
                var customer = await _context.Customers
                    .FirstOrDefaultAsync(c => c.Id == orderCreateDto.CustomerId);

                if (customer == null)
                    throw new ArgumentException($"Клиент с ID {orderCreateDto.CustomerId} не найден");

                var order = new Order
                {
                    CustomerId = orderCreateDto.CustomerId,
                    OrderDate = orderCreateDto.OrderDate,
                    Status = orderCreateDto.Status,
                    OrderItems = new List<OrderItem>()
                };

                decimal totalAmount = 0;
                foreach (var itemDto in orderCreateDto.OrderItems)
                {
                    var dish = await _context.Dishes.FindAsync(itemDto.DishId);
                    if (dish == null)
                        continue;

                    var orderItem = new OrderItem
                    {
                        DishId = itemDto.DishId,
                        Quantity = itemDto.Quantity,
                        Price = dish.Price,
                        Order = order
                    };

                    order.OrderItems.Add(orderItem);
                    totalAmount += orderItem.Quantity * orderItem.Price;
                }

                order.TotalAmount = totalAmount;

                _context.Orders.Add(order);
                await _context.SaveChangesAsync();

                return await GetOrderByIdAsync(order.Id);
            }
            catch (ArgumentException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception("Ошибка при создании заказа", ex);
            }
        }

        /// <summary>
        /// Обновить заказ
        /// </summary>
        public async Task UpdateOrderAsync(int id, OrderUpdateDto orderUpdateDto)
        {
            try
            {
                var order = await _context.Orders
                    .Include(o => o.OrderItems)
                    .FirstOrDefaultAsync(o => o.Id == id);

                if (order == null)
                    throw new KeyNotFoundException($"Заказ с ID {id} не найден");

                if (orderUpdateDto.CustomerId.HasValue)
                {
                    var customer = await _context.Customers
                        .FirstOrDefaultAsync(c => c.Id == orderUpdateDto.CustomerId.Value);

                    if (customer == null)
                        throw new ArgumentException($"Клиент с ID {orderUpdateDto.CustomerId} не найден");

                    order.CustomerId = orderUpdateDto.CustomerId.Value;
                }

                if (!string.IsNullOrEmpty(orderUpdateDto.Status))
                {
                    order.Status = orderUpdateDto.Status;
                }

                if (orderUpdateDto.TotalAmount.HasValue)
                {
                    order.TotalAmount = orderUpdateDto.TotalAmount.Value;
                }

                await _context.SaveChangesAsync();
            }
            catch (KeyNotFoundException)
            {
                throw;
            }
            catch (ArgumentException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception($"Ошибка при обновлении заказа с ID {id}", ex);
            }
        }

        /// <summary>
        /// Удалить заказ
        /// </summary>
        public async Task DeleteOrderAsync(int id)
        {
            try
            {
                var order = await _context.Orders
                    .Include(o => o.OrderItems)
                    .FirstOrDefaultAsync(o => o.Id == id);

                if (order == null)
                    throw new KeyNotFoundException($"Заказ с ID {id} не найден");

                _context.OrderItems.RemoveRange(order.OrderItems);
                _context.Orders.Remove(order);

                await _context.SaveChangesAsync();
            }
            catch (KeyNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception($"Ошибка при удалении заказа с ID {id}", ex);
            }
        }
    }
}