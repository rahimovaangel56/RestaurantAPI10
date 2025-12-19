namespace RestaurantAPI10.DTOs.OrderDTO
{
    public class OrderItemReadDto
    {
        public int DishId { get; set; }
        public string? DishName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}