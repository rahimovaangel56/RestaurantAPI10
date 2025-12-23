using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantAPI10.Models
{
    /// <summary>
    /// Заказ в ресторане
    /// </summary>
    public class Order
    {
        /// <summary>
        /// ID заказа
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Дата заказа
        /// </summary>
        public DateTime OrderDate { get; set; }

        /// <summary>
        /// Общая сумма заказа
        /// </summary>
        public decimal TotalAmount { get; set; }

        /// <summary>
        /// Статус заказа
        /// </summary>
        public string Status { get; set; } = "Pending";

        /// <summary>
        /// ID клиента
        /// </summary>
        public int CustomerId { get; set; }

        /// <summary>
        /// Клиент
        /// </summary>
        [ForeignKey("CustomerId")]
        public virtual Customer? Customer { get; set; }

        /// <summary>
        /// Позиции заказа
        /// </summary>
        public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}