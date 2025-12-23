using RestaurantAPI10.DTOs.ReservationDTO;

namespace RestaurantAPI10.Services
{
    /// <summary>
    /// Интерфейс сервиса для работы с бронированиями
    /// </summary>
    public interface IReservationService
    {
        /// <summary>
        /// Получить все бронирования
        /// </summary>
        Task<IEnumerable<ReservationReadDto>> GetAllReservationsAsync();

        /// <summary>
        /// Получить бронирование по ID
        /// </summary>
        Task<ReservationReadDto> GetReservationByIdAsync(int id);

        /// <summary>
        /// Создать новое бронирование
        /// </summary>
        Task<ReservationReadDto> CreateReservationAsync(ReservationCreateDto reservationCreateDto);

        /// <summary>
        /// Обновить бронирование
        /// </summary>
        Task UpdateReservationAsync(int id, ReservationCreateDto reservationUpdateDto);

        /// <summary>
        /// Удалить бронирование
        /// </summary>
        Task DeleteReservationAsync(int id);
    }
}