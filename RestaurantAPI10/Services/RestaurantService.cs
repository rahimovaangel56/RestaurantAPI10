using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI10.Data;
using RestaurantAPI10.DTOs.RestaurantDTO;
using RestaurantAPI10.Models;

namespace RestaurantAPI10.Services
{
    /// <summary>
    /// Сервис для работы с ресторанами
    /// </summary>
    public class RestaurantService : IRestaurantService
    {
        private readonly RestaurantDbContext _context;
        private readonly IMapper _mapper;

        /// <summary>
        /// Конструктор сервиса
        /// </summary>
        public RestaurantService(RestaurantDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Получить все рестораны
        /// </summary>
        public async Task<IEnumerable<RestaurantReadDto>> GetAllRestaurantsAsync()
        {
            try
            {
                var restaurants = await _context.Restaurants
                    .OrderBy(r => r.Name)
                    .ToListAsync();

                return _mapper.Map<IEnumerable<RestaurantReadDto>>(restaurants);
            }
            catch (Exception ex)
            {
                throw new Exception("Ошибка при получении списка ресторанов", ex);
            }
        }

        /// <summary>
        /// Получить ресторан по ID
        /// </summary>
        public async Task<RestaurantReadDto> GetRestaurantByIdAsync(int id)
        {
            try
            {
                var restaurant = await _context.Restaurants
                    .FirstOrDefaultAsync(r => r.Id == id);

                if (restaurant == null)
                    throw new KeyNotFoundException($"Ресторан с ID {id} не найден");

                return _mapper.Map<RestaurantReadDto>(restaurant);
            }
            catch (KeyNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception($"Ошибка при получении ресторана с ID {id}", ex);
            }
        }

        /// <summary>
        /// Создать новый ресторан
        /// </summary>
        public async Task<RestaurantReadDto> CreateRestaurantAsync(RestaurantCreateDto restaurantCreateDto)
        {
            try
            {
                var restaurant = _mapper.Map<Restaurant>(restaurantCreateDto);

                _context.Restaurants.Add(restaurant);
                await _context.SaveChangesAsync();

                return _mapper.Map<RestaurantReadDto>(restaurant);
            }
            catch (Exception ex)
            {
                throw new Exception("Ошибка при создании ресторана", ex);
            }
        }

        /// <summary>
        /// Обновить ресторан
        /// </summary>
        public async Task UpdateRestaurantAsync(int id, RestaurantCreateDto restaurantUpdateDto)
        {
            try
            {
                var restaurant = await _context.Restaurants
                    .FirstOrDefaultAsync(r => r.Id == id);

                if (restaurant == null)
                    throw new KeyNotFoundException($"Ресторан с ID {id} не найден");

                _mapper.Map(restaurantUpdateDto, restaurant);
                await _context.SaveChangesAsync();
            }
            catch (KeyNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception($"Ошибка при обновлении ресторана с ID {id}", ex);
            }
        }

        /// <summary>
        /// Удалить ресторан
        /// </summary>
        public async Task DeleteRestaurantAsync(int id)
        {
            try
            {
                var restaurant = await _context.Restaurants
                    .FirstOrDefaultAsync(r => r.Id == id);

                if (restaurant == null)
                    throw new KeyNotFoundException($"Ресторан с ID {id} не найден");

                _context.Restaurants.Remove(restaurant);
                await _context.SaveChangesAsync();
            }
            catch (KeyNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception($"Ошибка при удалении ресторана с ID {id}", ex);
            }
        }
    }
}