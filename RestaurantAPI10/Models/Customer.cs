namespace RestaurantAPI10.Models
{
    /// <summary>
    /// Клиент ресторана
    /// </summary>
    public class Customer
    {
        /// <summary>
        /// ID клиента
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Имя клиента
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Email клиента
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// Телефон клиента
        /// </summary>
        public string? Phone { get; set; }

        /// <summary>
        /// Заказы клиента
        /// </summary>
        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}