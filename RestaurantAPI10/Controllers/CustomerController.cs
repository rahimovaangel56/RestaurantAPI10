using Microsoft.AspNetCore.Mvc;
using RestaurantAPI10.DTOs.CustomerDTO;
using RestaurantAPI10.Helpers;
using RestaurantAPI10.Services;

namespace RestaurantAPI10.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _service;
        public CustomersController(ICustomerService service) => _service = service;

        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<CustomerReadDto>>>> GetAll()
            => Ok(ApiResponse<IEnumerable<CustomerReadDto>>.Ok(await _service.GetAllCustomersAsync()));

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<CustomerReadDto>>> Get(int id)
            => Ok(ApiResponse<CustomerReadDto>.Ok(await _service.GetCustomerByIdAsync(id)));

        [HttpPost]
        public async Task<ActionResult<ApiResponse<CustomerReadDto>>> Create(CustomerCreateDto dto)
        {
            var result = await _service.CreateCustomerAsync(dto);
            return CreatedAtAction(nameof(Get), new { id = result.Id },
                ApiResponse<CustomerReadDto>.Ok(result, "Клиент создан"));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CustomerCreateDto dto)
        {
            await _service.UpdateCustomerAsync(id, dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteCustomerAsync(id);
            return NoContent();
        }
    }
}