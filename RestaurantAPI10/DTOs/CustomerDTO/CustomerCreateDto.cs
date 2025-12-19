namespace RestaurantAPI10.DTOs.CustomerDTO
{
    public class CustomerCreateDto
    {
        public string Name { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string? Phone { get; set; }
    }
}