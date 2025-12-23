namespace RestaurantAPI10.DTOs.CustomerDTO
{
    /// <summary>
    /// DTO для создания клиента
    /// </summary>
    public class CustomerCreateDto
    {
        /// <summary>
        /// Имя клиента
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Email клиента
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// Телефон клиента
        /// </summary>
        public string? Phone { get; set; }
    }
}