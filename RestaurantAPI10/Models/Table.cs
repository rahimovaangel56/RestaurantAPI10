using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantAPI10.Models
{
    /// <summary>
    /// Столик в ресторане
    /// </summary>
    public class Table
    {
        /// <summary>
        /// ID столика
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Номер столика
        /// </summary>
        public string Number { get; set; } = string.Empty;

        /// <summary>
        /// Вместимость (количество мест)
        /// </summary>
        public int Capacity { get; set; }

        /// <summary>
        /// Статус столика
        /// </summary>
        public string Status { get; set; } = "Available";

        /// <summary>
        /// ID ресторана
        /// </summary>
        public int RestaurantId { get; set; }

        /// <summary>
        /// Ресторан
        /// </summary>
        [ForeignKey("RestaurantId")]
        public virtual Restaurant? Restaurant { get; set; }
    }
}