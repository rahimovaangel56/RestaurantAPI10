namespace RestaurantAPI10.DTOs.DishDTO
{
    /// <summary>
    /// DTO для создания блюда
    /// </summary>
    public class DishCreateDto
    {
        /// <summary>
        /// Название блюда
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Описание блюда
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Цена блюда
        /// </summary>
        public decimal Price { get; set; }
    }
}