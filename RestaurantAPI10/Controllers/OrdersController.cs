using Microsoft.AspNetCore.Mvc;
using RestaurantAPI10.DTOs.OrderDTO;
using RestaurantAPI10.Helpers;
using RestaurantAPI10.Services;

namespace RestaurantAPI10.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        /// <summary>
        /// Получить все заказы
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<OrderReadDto>>>> GetOrders()
        {
            var orders = await _orderService.GetAllOrdersAsync();
            return Ok(ApiResponse<IEnumerable<OrderReadDto>>.Ok(orders));
        }

        /// <summary>
        /// Получить заказ по ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<OrderReadDto>>> GetOrder(int id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);
            return Ok(ApiResponse<OrderReadDto>.Ok(order));
        }

        /// <summary>
        /// Создать новый заказ
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<ApiResponse<OrderReadDto>>> CreateOrder(OrderCreateDto orderCreateDto)
        {
            var order = await _orderService.CreateOrderAsync(orderCreateDto);
            return CreatedAtAction(nameof(GetOrder), new { id = order.Id },
                ApiResponse<OrderReadDto>.Ok(order, "Заказ успешно создан"));
        }

        /// <summary>
        /// Обновить существующий заказ
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder(int id, OrderUpdateDto orderUpdateDto)
        {
            await _orderService.UpdateOrderAsync(id, orderUpdateDto);
            return NoContent();
        }

        /// <summary>
        /// Удалить заказ
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            await _orderService.DeleteOrderAsync(id);
            return NoContent();
        }
    }
}