namespace RestaurantAPI10.DTOs.TableDTO
{
    public class TableReadDto
    {
        public int Id { get; set; }
        public string Number { get; set; } = string.Empty;
        public int Capacity { get; set; }
        public string Status { get; set; } = "Available";
        public int RestaurantId { get; set; }
        public string? RestaurantName { get; set; } 
    }
}