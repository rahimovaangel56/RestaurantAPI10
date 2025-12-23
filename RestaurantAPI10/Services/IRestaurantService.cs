using RestaurantAPI10.DTOs.RestaurantDTO;

namespace RestaurantAPI10.Services
{
    /// <summary>
    /// Интерфейс сервиса для работы с ресторанами
    /// </summary>
    public interface IRestaurantService
    {
        /// <summary>
        /// Получить все рестораны
        /// </summary>
        Task<IEnumerable<RestaurantReadDto>> GetAllRestaurantsAsync();

        /// <summary>
        /// Получить ресторан по ID
        /// </summary>
        Task<RestaurantReadDto> GetRestaurantByIdAsync(int id);

        /// <summary>
        /// Создать новый ресторан
        /// </summary>
        Task<RestaurantReadDto> CreateRestaurantAsync(RestaurantCreateDto restaurantCreateDto);

        /// <summary>
        /// Обновить ресторан
        /// </summary>
        Task UpdateRestaurantAsync(int id, RestaurantCreateDto restaurantUpdateDto);

        /// <summary>
        /// Удалить ресторан
        /// </summary>
        Task DeleteRestaurantAsync(int id);
    }
}