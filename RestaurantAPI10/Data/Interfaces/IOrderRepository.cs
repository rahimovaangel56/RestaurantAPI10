using RestaurantAPI10.Models;

namespace RestaurantAPI10.Data.Interfaces
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<IEnumerable<Order>> GetOrdersByStatusAsync(string status);
        Task<IEnumerable<Order>> GetOrdersByCustomerAsync(int customerId);
    }
}