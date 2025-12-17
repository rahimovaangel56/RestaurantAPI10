namespace RestaurantAPI10.Models
{
    public class Table
    {
        public int Id { get; set; }
        public string Number { get; set; } = string.Empty;
        public int Capacity { get; set; }
        public string Status { get; set; } = "Available";
        public int RestaurantId { get; set; }
    }
}