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
        private readonly IOrderService _service;
        public OrdersController(IOrderService service) => _service = service;

        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<OrderReadDto>>>> GetAll()
            => Ok(ApiResponse<IEnumerable<OrderReadDto>>.Ok(await _service.GetAllOrdersAsync()));

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<OrderReadDto>>> Get(int id)
            => Ok(ApiResponse<OrderReadDto>.Ok(await _service.GetOrderByIdAsync(id)));

        [HttpPost]
        public async Task<ActionResult<ApiResponse<OrderReadDto>>> Create(OrderCreateDto dto)
        {
            var result = await _service.CreateOrderAsync(dto);
            return CreatedAtAction(nameof(Get), new { id = result.Id },
                ApiResponse<OrderReadDto>.Ok(result, "Заказ создан"));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, OrderUpdateDto dto)
        {
            await _service.UpdateOrderAsync(id, dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteOrderAsync(id);
            return NoContent();
        }
    }
}