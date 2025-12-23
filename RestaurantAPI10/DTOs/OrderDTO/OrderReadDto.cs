namespace RestaurantAPI10.DTOs.OrderDTO
{
    /// <summary>
    /// DTO для чтения данных заказа
    /// </summary>
    public class OrderReadDto
    {
        /// <summary>
        /// ID заказа
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Дата заказа
        /// </summary>
        public DateTime OrderDate { get; set; }

        /// <summary>
        /// Общая сумма заказа
        /// </summary>
        public decimal TotalAmount { get; set; }

        /// <summary>
        /// Статус заказа
        /// </summary>
        public string Status { get; set; } = "Pending";

        /// <summary>
        /// Имя клиента
        /// </summary>
        public string? CustomerName { get; set; }

        /// <summary>
        /// Позиции заказа
        /// </summary>
        public List<OrderItemReadDto> OrderItems { get; set; } = new();
    }
}