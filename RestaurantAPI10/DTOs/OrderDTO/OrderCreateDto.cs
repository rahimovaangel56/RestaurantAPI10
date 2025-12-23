namespace RestaurantAPI10.DTOs.OrderDTO
{
    /// <summary>
    /// DTO для создания заказа
    /// </summary>
    public class OrderCreateDto
    {
        /// <summary>
        /// ID клиента
        /// </summary>
        public int CustomerId { get; set; }

        /// <summary>
        /// Дата заказа
        /// </summary>
        public DateTime OrderDate { get; set; }

        /// <summary>
        /// Статус заказа
        /// </summary>
        public string Status { get; set; } = "Pending";

        /// <summary>
        /// Позиции заказа
        /// </summary>
        public List<OrderItemCreateDto> OrderItems { get; set; } = new();
    }
}