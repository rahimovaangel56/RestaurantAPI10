using Microsoft.AspNetCore.Mvc;
using RestaurantAPI10.DTOs.DishDTO;
using RestaurantAPI10.DTOs.RestaurantDTO;
using RestaurantAPI10.Helpers;
using RestaurantAPI10.Services;

namespace RestaurantAPI10.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly IDishService _dishService;
        private readonly IRestaurantService _restaurantService;

        public MenuController(IDishService dishService, IRestaurantService restaurantService)
        {
            _dishService = dishService;
            _restaurantService = restaurantService;
        }

        [HttpGet("dishes")]
        public async Task<ActionResult<ApiResponse<IEnumerable<DishReadDto>>>> GetDishes()
            => Ok(ApiResponse<IEnumerable<DishReadDto>>.Ok(await _dishService.GetAllDishesAsync()));

        [HttpGet("dishes/{id}")]
        public async Task<ActionResult<ApiResponse<DishReadDto>>> GetDish(int id)
            => Ok(ApiResponse<DishReadDto>.Ok(await _dishService.GetDishByIdAsync(id)));

        [HttpPost("dishes")]
        public async Task<ActionResult<ApiResponse<DishReadDto>>> CreateDish(DishCreateDto dto)
        {
            var result = await _dishService.CreateDishAsync(dto);
            return CreatedAtAction(nameof(GetDish), new { id = result.Id },
                ApiResponse<DishReadDto>.Ok(result, "Блюдо создано"));
        }

        [HttpPut("dishes/{id}")]
        public async Task<IActionResult> UpdateDish(int id, DishCreateDto dto)
        {
            await _dishService.UpdateDishAsync(id, dto);
            return NoContent();
        }

        [HttpDelete("dishes/{id}")]
        public async Task<IActionResult> DeleteDish(int id)
        {
            await _dishService.DeleteDishAsync(id);
            return NoContent();
        }
    }
}