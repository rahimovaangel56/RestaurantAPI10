namespace RestaurantAPI10.DTOs.TableDTO
{
    public class TableCreateDto
    {
        public string Number { get; set; } = string.Empty;
        public int Capacity { get; set; }
        public string Status { get; set; } = "Available";
        public int RestaurantId { get; set; }
    }
}