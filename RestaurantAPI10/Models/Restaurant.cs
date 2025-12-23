namespace RestaurantAPI10.Models
{
    /// <summary>
    /// Ресторан
    /// </summary>
    public class Restaurant
    {
        /// <summary>
        /// ID ресторана
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Название ресторана
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Адрес ресторана
        /// </summary>
        public string Address { get; set; } = string.Empty;

        /// <summary>
        /// Телефон ресторана
        /// </summary>
        public string Phone { get; set; } = string.Empty;

        /// <summary>
        /// Время открытия
        /// </summary>
        public TimeSpan OpeningTime { get; set; }

        /// <summary>
        /// Время закрытия
        /// </summary>
        public TimeSpan ClosingTime { get; set; }

        /// <summary>
        /// Столики ресторана
        /// </summary>
        public virtual ICollection<Table> Tables { get; set; } = new List<Table>();
    }
}