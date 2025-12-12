using System.ComponentModel.DataAnnotations;

namespace RestaurantAPI10.Models
{
    public class Table
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Number { get; set; }

        [Range(1, 20)]
        public int Capacity { get; set; }

        [Required]
        public string Status { get; set; } 

        public int RestaurantId { get; set; }

        public Restaurant Restaurant { get; set; }
        public ICollection<Reservation> Reservations { get; set; }
    }
}
