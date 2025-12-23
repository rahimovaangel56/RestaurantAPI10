using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantAPI10.Models
{
    /// <summary>
    /// Бронирование столика
    /// </summary>
    public class Reservation
    {
        /// <summary>
        /// ID бронирования
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Дата бронирования
        /// </summary>
        public DateTime ReservationDate { get; set; }

        /// <summary>
        /// Время начала
        /// </summary>
        public TimeSpan StartTime { get; set; }

        /// <summary>
        /// Время окончания
        /// </summary>
        public TimeSpan EndTime { get; set; }

        /// <summary>
        /// Количество гостей
        /// </summary>
        public int NumberOfGuests { get; set; }

        /// <summary>
        /// Особые пожелания
        /// </summary>
        public string SpecialRequests { get; set; } = string.Empty;

        /// <summary>
        /// ID клиента
        /// </summary>
        public int CustomerId { get; set; }

        /// <summary>
        /// ID столика
        /// </summary>
        public int TableId { get; set; }

        /// <summary>
        /// Клиент
        /// </summary>
        [ForeignKey("CustomerId")]
        public virtual Customer? Customer { get; set; }

        /// <summary>
        /// Столик
        /// </summary>
        [ForeignKey("TableId")]
        public virtual Table? Table { get; set; }
    }
}