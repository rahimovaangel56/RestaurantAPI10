using RestaurantAPI10.DTOs.OrderDTO;

namespace RestaurantAPI10.Services
{
    /// <summary>
    /// Интерфейс сервиса для работы с заказами
    /// </summary>
    public interface IOrderService
    {
        Task<IEnumerable<OrderReadDto>> GetAllOrdersAsync();

        Task<OrderReadDto> GetOrderByIdAsync(int id);

        Task<OrderReadDto> CreateOrderAsync(OrderCreateDto orderCreateDto);

        Task UpdateOrderAsync(int id, OrderUpdateDto orderUpdateDto);

        Task DeleteOrderAsync(int id);
    }
}