namespace RestaurantAPI10.Models
{
    /// <summary>
    /// Блюдо в меню
    /// </summary>
    public class Dish
    {
        /// <summary>
        /// ID блюда
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Название блюда
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Описание блюда
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Цена блюда
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Позиции заказа с этим блюдом
        /// </summary>
        public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}