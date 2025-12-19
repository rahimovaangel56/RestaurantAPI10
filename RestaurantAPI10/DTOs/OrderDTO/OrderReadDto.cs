namespace RestaurantAPI10.DTOs.OrderDTO
{
    public class OrderReadDto
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; } = "Pending";
        public string? CustomerName { get; set; }


        public List<OrderItemReadDto> OrderItems { get; set; } = new();
    }
}