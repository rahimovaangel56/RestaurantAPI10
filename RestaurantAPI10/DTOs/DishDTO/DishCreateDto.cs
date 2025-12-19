namespace RestaurantAPI10.DTOs.DishDTO
{
    public class DishCreateDto
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal Price { get; set; }
    }
}