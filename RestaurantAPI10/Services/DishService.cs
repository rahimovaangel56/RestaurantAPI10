using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI10.Data;
using RestaurantAPI10.DTOs.DishDTO;
using RestaurantAPI10.Models;

namespace RestaurantAPI10.Services
{
    /// <summary>
    /// Сервис для работы с блюдами
    /// </summary>
    public class DishService : IDishService
    {
        private readonly RestaurantDbContext _context;
        private readonly IMapper _mapper;

        /// <summary>
        /// Конструктор сервиса
        /// </summary>
        public DishService(RestaurantDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Получить все блюда
        /// </summary>
        public async Task<IEnumerable<DishReadDto>> GetAllDishesAsync()
        {
            try
            {
                var dishes = await _context.Dishes
                    .OrderBy(d => d.Name)
                    .ToListAsync();

                return _mapper.Map<IEnumerable<DishReadDto>>(dishes);
            }
            catch (Exception ex)
            {
                throw new Exception("Ошибка при получении списка блюд", ex);
            }
        }

        /// <summary>
        /// Получить блюдо по ID
        /// </summary>
        public async Task<DishReadDto> GetDishByIdAsync(int id)
        {
            try
            {
                var dish = await _context.Dishes
                    .FirstOrDefaultAsync(d => d.Id == id);

                if (dish == null)
                    throw new KeyNotFoundException($"Блюдо с ID {id} не найден");

                return _mapper.Map<DishReadDto>(dish);
            }
            catch (KeyNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception($"Ошибка при получении блюда с ID {id}", ex);
            }
        }

        /// <summary>
        /// Создать новое блюдо
        /// </summary>
        public async Task<DishReadDto> CreateDishAsync(DishCreateDto dishCreateDto)
        {
            try
            {
                var dish = _mapper.Map<Dish>(dishCreateDto);

                _context.Dishes.Add(dish);
                await _context.SaveChangesAsync();

                return _mapper.Map<DishReadDto>(dish);
            }
            catch (Exception ex)
            {
                throw new Exception("Ошибка при создании блюда", ex);
            }
        }

        /// <summary>
        /// Обновить блюдо
        /// </summary>
        public async Task UpdateDishAsync(int id, DishCreateDto dishUpdateDto)
        {
            try
            {
                var dish = await _context.Dishes
                    .FirstOrDefaultAsync(d => d.Id == id);

                if (dish == null)
                    throw new KeyNotFoundException($"Блюдо с ID {id} не найден");

                _mapper.Map(dishUpdateDto, dish);
                await _context.SaveChangesAsync();
            }
            catch (KeyNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception($"Ошибка при обновлении блюда с ID {id}", ex);
            }
        }

        /// <summary>
        /// Удалить блюдо
        /// </summary>
        public async Task DeleteDishAsync(int id)
        {
            try
            {
                var dish = await _context.Dishes
                    .FirstOrDefaultAsync(d => d.Id == id);

                if (dish == null)
                    throw new KeyNotFoundException($"Блюдо с ID {id} не найден");

                _context.Dishes.Remove(dish);
                await _context.SaveChangesAsync();
            }
            catch (KeyNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception($"Ошибка при удалении блюда с ID {id}", ex);
            }
        }
    }
}