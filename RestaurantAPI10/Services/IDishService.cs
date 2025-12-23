using RestaurantAPI10.DTOs.DishDTO;

namespace RestaurantAPI10.Services
{
    /// <summary>
    /// Интерфейс сервиса для работы с блюдами
    /// </summary>
    public interface IDishService
    {
        /// <summary>
        /// Получить все блюда
        /// </summary>
        Task<IEnumerable<DishReadDto>> GetAllDishesAsync();

        /// <summary>
        /// Получить блюдо по ID
        /// </summary>
        Task<DishReadDto> GetDishByIdAsync(int id);

        /// <summary>
        /// Создать новое блюдо
        /// </summary>
        Task<DishReadDto> CreateDishAsync(DishCreateDto dishCreateDto);

        /// <summary>
        /// Обновить блюдо
        /// </summary>
        Task UpdateDishAsync(int id, DishCreateDto dishUpdateDto);

        /// <summary>
        /// Удалить блюдо
        /// </summary>
        Task DeleteDishAsync(int id);
    }
}