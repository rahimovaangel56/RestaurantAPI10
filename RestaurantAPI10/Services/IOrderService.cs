using RestaurantAPI10.DTOs.OrderDTO;

namespace RestaurantAPI10.Services
{
    /// <summary>
    /// Интерфейс сервиса для работы с заказами
    /// </summary>
    public interface IOrderService
    {
        /// <summary>
        /// Получить все заказы
        /// </summary>
        Task<IEnumerable<OrderReadDto>> GetAllOrdersAsync();

        /// <summary>
        /// Получить заказ по ID
        /// </summary>
        Task<OrderReadDto> GetOrderByIdAsync(int id);

        /// <summary>
        /// Создать новый заказ
        /// </summary>
        Task<OrderReadDto> CreateOrderAsync(OrderCreateDto orderCreateDto);

        /// <summary>
        /// Обновить заказ
        /// </summary>
        Task UpdateOrderAsync(int id, OrderUpdateDto orderUpdateDto);

        /// <summary>
        /// Удалить заказ
        /// </summary>
        Task DeleteOrderAsync(int id);
    }
}