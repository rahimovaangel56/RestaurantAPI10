namespace RestaurantAPI10.DTOs.OrderDTO
{
    /// <summary>
    /// DTO для обновления существующего заказа
    /// </summary>
    public class OrderUpdateDto
    {
        public int? CustomerId { get; set; }
        public string? Status { get; set; }
        public decimal? TotalAmount { get; set; }
    }
}