using Microsoft.EntityFrameworkCore;
using RestaurantAPI10.Data.Interfaces;
using RestaurantAPI10.Models;

namespace RestaurantAPI10.Data.Repositories
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        private readonly RestaurantDbContext _dbContext;

        public OrderRepository(RestaurantDbContext context) : base(context)
        {
            _dbContext = context;
        }

        public async Task<IEnumerable<Order>> GetOrdersByStatusAsync(string status)
        {
            return await _dbContext.Orders
                .Where(o => o.Status == status)
                .ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetOrdersByCustomerAsync(int customerId)
        {
            return await _dbContext.Orders
                .Where(o => o.CustomerId == customerId)
                .ToListAsync();
        }
    }
}