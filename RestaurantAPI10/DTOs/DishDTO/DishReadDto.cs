namespace RestaurantAPI10.DTOs.DishDTO
{
    public class DishReadDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
    }
}