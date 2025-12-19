using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI10.Data;
using RestaurantAPI10.DTOs.OrderDTO;
using RestaurantAPI10.Models;

namespace RestaurantAPI10.Services
{
    /// <summary>
    /// Сервис для работы с заказами, реализующий бизнес-логику
    /// </summary>
    public class OrderService : IOrderService
    {
        private readonly RestaurantDbContext _context;
        private readonly IMapper _mapper;

        /// <summary>
        /// Конструктор сервиса заказов
        /// </summary>
        public OrderService(RestaurantDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Получить все заказы с детализацией
        /// </summary>
        /// <returns>Коллекция DTO заказов</returns>
        public async Task<IEnumerable<OrderReadDto>> GetAllOrdersAsync()
        {
            try
            {
                var orders = await _context.Orders
                    .Include(o => o.Customer)
                    .Include(o => o.OrderItems)
                        .ThenInclude(oi => oi.Dish)
                    .OrderByDescending(o => o.OrderDate)
                    .ToListAsync();

                return _mapper.Map<IEnumerable<OrderReadDto>>(orders);
            }
            catch (Exception ex)
            {
                throw new Exception("Ошибка при получении списка заказов", ex);
            }
        }

        /// <summary>
        /// Получить заказ по ID с проверкой существования
        /// </summary>
        /// <param name="id">ID заказа</param>
        /// <returns>DTO заказа</returns>
        /// <exception cref="KeyNotFoundException">Заказ не найден</exception>
        public async Task<OrderReadDto> GetOrderByIdAsync(int id)
        {
            try
            {
                var order = await _context.Orders
                    .Include(o => o.Customer)
                    .Include(o => o.OrderItems)
                        .ThenInclude(oi => oi.Dish)
                    .FirstOrDefaultAsync(o => o.Id == id);

                if (order == null)
                    throw new KeyNotFoundException($"Заказ с ID {id} не найден");

                return _mapper.Map<OrderReadDto>(order);
            }
            catch (KeyNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception($"Ошибка при получении заказа с ID {id}", ex);
            }
        }

        /// <summary>
        /// Создать новый заказ с валидацией данных
        /// </summary>
        /// <param name="orderCreateDto">DTO для создания заказа</param>
        /// <returns>DTO созданного заказа</returns>
        /// <exception cref="ArgumentException">Неверные данные заказа</exception>
        public async Task<OrderReadDto> CreateOrderAsync(OrderCreateDto orderCreateDto)
        {
            try
            {
                var customer = await _context.Customers
                    .FirstOrDefaultAsync(c => c.Id == orderCreateDto.CustomerId);

                if (customer == null)
                    throw new ArgumentException($"Клиент с ID {orderCreateDto.CustomerId} не найден");

                var dishIds = orderCreateDto.OrderItems.Select(oi => oi.DishId).Distinct();
                var existingDishes = await _context.Dishes
                    .Where(d => dishIds.Contains(d.Id))
                    .Select(d => d.Id)
                    .ToListAsync();

                var missingDishes = dishIds.Except(existingDishes).ToList();
                if (missingDishes.Any())
                    throw new ArgumentException($"Блюда с ID {string.Join(", ", missingDishes)} не найдены");

                var order = new Order
                {
                    CustomerId = orderCreateDto.CustomerId,
                    OrderDate = orderCreateDto.OrderDate,
                    OrderItems = new List<OrderItem>()
                };

                decimal totalAmount = 0;
                foreach (var itemDto in orderCreateDto.OrderItems)
                {
                    var dish = await _context.Dishes.FindAsync(itemDto.DishId);

                    if (dish == null)
                        continue; 

                    var orderItem = new OrderItem
                    {
                        DishId = itemDto.DishId,
                        Quantity = itemDto.Quantity,
                        Price = dish.Price,
                        Order = order
                    };

                    order.OrderItems.Add(orderItem);
                    totalAmount += orderItem.Quantity * orderItem.Price;
                }

                order.TotalAmount = totalAmount;

                _context.Orders.Add(order);
                await _context.SaveChangesAsync();

                return await GetOrderByIdAsync(order.Id);
            }
            catch (ArgumentException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception("Ошибка при создании заказа", ex);
            }
        }

        /// <summary>
        /// Обновить существующий заказ
        /// </summary>
        /// <param name="id">ID заказа</param>
        /// <param name="orderUpdateDto">DTO для обновления заказа</param>
        /// <exception cref="KeyNotFoundException">Заказ не найден</exception>
        /// <summary>
        /// Обновить существующий заказ
        /// </summary>
        /// <param name="id">ID заказа</param>
        /// <param name="orderUpdateDto">DTO для обновления заказа</param>
        /// <exception cref="KeyNotFoundException">Заказ не найден</exception>
        public async Task UpdateOrderAsync(int id, OrderUpdateDto orderUpdateDto)
        {
            try
            {
                var order = await _context.Orders
                    .Include(o => o.OrderItems)
                    .FirstOrDefaultAsync(o => o.Id == id);

                if (order == null)
                    throw new KeyNotFoundException($"Заказ с ID {id} не найден");

                if (orderUpdateDto.CustomerId.HasValue)
                {
                    var customer = await _context.Customers
                        .FirstOrDefaultAsync(c => c.Id == orderUpdateDto.CustomerId.Value);

                    if (customer == null)
                        throw new ArgumentException($"Клиент с ID {orderUpdateDto.CustomerId} не найден");

                    order.CustomerId = orderUpdateDto.CustomerId.Value;
                }

                if (!string.IsNullOrEmpty(orderUpdateDto.Status))
                {
                    order.Status = orderUpdateDto.Status;
                }

                if (orderUpdateDto.TotalAmount.HasValue)
                {
                    order.TotalAmount = orderUpdateDto.TotalAmount.Value;
                }

                await _context.SaveChangesAsync();
            }
            catch (KeyNotFoundException)
            {
                throw;
            }
            catch (ArgumentException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception($"Ошибка при обновлении заказа с ID {id}", ex);
            }
        }

        /// <summary>
        /// Удалить заказ по ID
        /// </summary>
        /// <param name="id">ID заказа</param>
        /// <exception cref="KeyNotFoundException">Заказ не найден</exception>
        public async Task DeleteOrderAsync(int id)
        {
            try
            {
                var order = await _context.Orders
                    .Include(o => o.OrderItems)
                    .FirstOrDefaultAsync(o => o.Id == id);

                if (order == null)
                    throw new KeyNotFoundException($"Заказ с ID {id} не найден");

                _context.OrderItems.RemoveRange(order.OrderItems);

                _context.Orders.Remove(order);

                await _context.SaveChangesAsync();
            }
            catch (KeyNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception($"Ошибка при удалении заказа с ID {id}", ex);
            }
        }

        /// <summary>
        /// Получить заказы по ID клиента
        /// </summary>
        /// <param name="customerId">ID клиента</param>
        /// <returns>Коллекция DTO заказов клиента</returns>
        public async Task<IEnumerable<OrderReadDto>> GetOrdersByCustomerIdAsync(int customerId)
        {
            try
            {
                var orders = await _context.Orders
                    .Where(o => o.CustomerId == customerId)
                    .Include(o => o.Customer)
                    .Include(o => o.OrderItems)
                        .ThenInclude(oi => oi.Dish)
                    .OrderByDescending(o => o.OrderDate)
                    .ToListAsync();

                return _mapper.Map<IEnumerable<OrderReadDto>>(orders);
            }
            catch (Exception ex)
            {
                throw new Exception($"Ошибка при получении заказов клиента с ID {customerId}", ex);
            }
        }

        /// <summary>
        /// Рассчитать общую сумму заказа на основе его позиций
        /// </summary>
        /// <param name="orderId">ID заказа</param>
        /// <returns>Общая сумма заказа</returns>
        /// <exception cref="KeyNotFoundException">Заказ не найден</exception>
        public async Task<decimal> CalculateOrderTotalAsync(int orderId)
        {
            try
            {
                var order = await _context.Orders
                    .Include(o => o.OrderItems)
                        .ThenInclude(oi => oi.Dish)
                    .FirstOrDefaultAsync(o => o.Id == orderId);

                if (order == null)
                    throw new KeyNotFoundException($"Заказ с ID {orderId} не найден");

                decimal total = order.OrderItems.Sum(oi => oi.Quantity * oi.Price);

                if (order.TotalAmount != total)
                {
                    order.TotalAmount = total;
                    await _context.SaveChangesAsync();
                }

                return total;
            }
            catch (KeyNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception($"Ошибка при расчете суммы заказа с ID {orderId}", ex);
            }
        }
    }
}