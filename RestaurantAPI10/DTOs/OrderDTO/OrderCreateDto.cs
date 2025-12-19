namespace RestaurantAPI10.DTOs.OrderDTO
{
    public class OrderCreateDto
    {
        public int CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; } = "Pending";
        public List<OrderItemCreateDto> OrderItems { get; set; } = new();
    }
}