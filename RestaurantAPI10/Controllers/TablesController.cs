using Microsoft.AspNetCore.Mvc;
using RestaurantAPI10.DTOs.TableDTO;
using RestaurantAPI10.Helpers;
using RestaurantAPI10.Services;

namespace RestaurantAPI10.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TablesController : ControllerBase
    {
        private readonly IReservationService _reservationService;
        private readonly ITableService _tableService;

        public TablesController(IReservationService reservationService, ITableService tableService)
        {
            _reservationService = reservationService;
            _tableService = tableService;
        }
        [HttpGet("tables")]
        public async Task<ActionResult<ApiResponse<IEnumerable<TableReadDto>>>> GetTables()
            => Ok(ApiResponse<IEnumerable<TableReadDto>>.Ok(await _tableService.GetAllTablesAsync()));

        [HttpGet("tables/{id}")]
        public async Task<ActionResult<ApiResponse<TableReadDto>>> GetTable(int id)
            => Ok(ApiResponse<TableReadDto>.Ok(await _tableService.GetTableByIdAsync(id)));

        [HttpPost("tables")]
        public async Task<ActionResult<ApiResponse<TableReadDto>>> CreateTable(TableCreateDto dto)
        {
            var result = await _tableService.CreateTableAsync(dto);
            return CreatedAtAction(nameof(GetTable), new { id = result.Id },
                ApiResponse<TableReadDto>.Ok(result, "Столик создан"));
        }

        [HttpPut("tables/{id}")]
        public async Task<IActionResult> UpdateTable(int id, TableCreateDto dto)
        {
            await _tableService.UpdateTableAsync(id, dto);
            return NoContent();
        }

        [HttpDelete("tables/{id}")]
        public async Task<IActionResult> DeleteTable(int id)
        {
            await _tableService.DeleteTableAsync(id);
            return NoContent();
        }
    }
}
