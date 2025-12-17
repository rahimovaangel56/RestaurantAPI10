namespace RestaurantAPI10.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; } = "Pending";
        public decimal TotalAmount { get; set; }
        public int CustomerId { get; set; }
        public int TableId { get; set; }
    }
}