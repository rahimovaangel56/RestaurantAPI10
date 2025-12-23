using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantAPI10.Models
{
    /// <summary>
    /// Позиция в заказе
    /// </summary>
    public class OrderItem
    {
        /// <summary>
        /// ID позиции
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Количество
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Цена позиции
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// ID заказа
        /// </summary>
        public int OrderId { get; set; }

        /// <summary>
        /// Заказ
        /// </summary>
        [ForeignKey("OrderId")]
        public virtual Order? Order { get; set; }

        /// <summary>
        /// ID блюда
        /// </summary>
        public int DishId { get; set; }

        /// <summary>
        /// Блюдо
        /// </summary>
        [ForeignKey("DishId")]
        public virtual Dish? Dish { get; set; }
    }
}