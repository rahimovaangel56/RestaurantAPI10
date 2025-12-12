using System.ComponentModel.DataAnnotations;

namespace RestaurantAPI10.Models
{
    public class Reservation
    {
        public int Id { get; set; }

        [Required]
        public DateTime ReservationDate { get; set; }

        [Required]
        public TimeSpan StartTime { get; set; }

        [Required]
        public TimeSpan EndTime { get; set; }

        public int NumberOfGuests { get; set; }

        [MaxLength(500)]
        public string SpecialRequests { get; set; }

        public int CustomerId { get; set; }
        public int TableId { get; set; }

        public Customer Customer { get; set; }
        public Table Table { get; set; }
    }
}
