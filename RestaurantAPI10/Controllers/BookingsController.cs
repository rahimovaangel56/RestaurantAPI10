using Microsoft.AspNetCore.Mvc;
using RestaurantAPI10.DTOs.ReservationDTO;
using RestaurantAPI10.DTOs.TableDTO;
using RestaurantAPI10.Helpers;
using RestaurantAPI10.Services;

namespace RestaurantAPI10.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly IReservationService _reservationService;
        private readonly ITableService _tableService;

        public BookingsController(IReservationService reservationService, ITableService tableService)
        {
            _reservationService = reservationService;
            _tableService = tableService;
        }

       
        [HttpGet("reservations")]
        public async Task<ActionResult<ApiResponse<IEnumerable<ReservationReadDto>>>> GetReservations()
            => Ok(ApiResponse<IEnumerable<ReservationReadDto>>.Ok(await _reservationService.GetAllReservationsAsync()));

        
        [HttpGet("reservations/{id}")]
        public async Task<ActionResult<ApiResponse<ReservationReadDto>>> GetReservation(int id)
            => Ok(ApiResponse<ReservationReadDto>.Ok(await _reservationService.GetReservationByIdAsync(id)));

        [HttpPost("reservations")]
        public async Task<ActionResult<ApiResponse<ReservationReadDto>>> CreateReservation(ReservationCreateDto dto)
        {
            var result = await _reservationService.CreateReservationAsync(dto);
            return CreatedAtAction(nameof(GetReservation), new { id = result.Id },
                ApiResponse<ReservationReadDto>.Ok(result, "Бронирование создано"));
        }

        [HttpPut("reservations/{id}")]
        public async Task<IActionResult> UpdateReservation(int id, ReservationCreateDto dto)
        {
            await _reservationService.UpdateReservationAsync(id, dto);
            return NoContent();
        }

        [HttpDelete("reservations/{id}")]
        public async Task<IActionResult> DeleteReservation(int id)
        {
            await _reservationService.DeleteReservationAsync(id);
            return NoContent();
        }        
    }
}