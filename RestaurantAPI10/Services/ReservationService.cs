using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI10.Data;
using RestaurantAPI10.DTOs.ReservationDTO;
using RestaurantAPI10.Models;

namespace RestaurantAPI10.Services
{
    /// <summary>
    /// Сервис для работы с бронированиями
    /// </summary>
    public class ReservationService : IReservationService
    {
        private readonly RestaurantDbContext _context;
        private readonly IMapper _mapper;

        /// <summary>
        /// Конструктор сервиса
        /// </summary>
        public ReservationService(RestaurantDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Получить все бронирования
        /// </summary>
        public async Task<IEnumerable<ReservationReadDto>> GetAllReservationsAsync()
        {
            try
            {
                var reservations = await _context.Reservations
                    //.OrderByDescending(r => r.ReservationDate)
                    //.ThenBy(r => r.StartTime)
                    .ToListAsync();

                return _mapper.Map<IEnumerable<ReservationReadDto>>(reservations);
            }
            catch (Exception ex)
            {
                throw new Exception("Ошибка при получении списка бронирований", ex);
            }
        }

        /// <summary>
        /// Получить бронирование по ID
        /// </summary>
        public async Task<ReservationReadDto> GetReservationByIdAsync(int id)
        {
            try
            {
                var reservation = await _context.Reservations
                    .FirstOrDefaultAsync(r => r.Id == id);

                if (reservation == null)
                    throw new KeyNotFoundException($"Бронирование с ID {id} не найдено");

                return _mapper.Map<ReservationReadDto>(reservation);
            }
            catch (KeyNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception($"Ошибка при получении бронирования с ID {id}", ex);
            }
        }

        /// <summary>
        /// Создать новое бронирование
        /// </summary>
        public async Task<ReservationReadDto> CreateReservationAsync(ReservationCreateDto reservationCreateDto)
        {
            try
            {
                var reservation = _mapper.Map<Reservation>(reservationCreateDto);

                _context.Reservations.Add(reservation);
                await _context.SaveChangesAsync();

                return _mapper.Map<ReservationReadDto>(reservation);
            }
            catch (Exception ex)
            {
                throw new Exception("Ошибка при создании бронирования", ex);
            }
        }

        /// <summary>
        /// Обновить бронирование
        /// </summary>
        public async Task UpdateReservationAsync(int id, ReservationCreateDto reservationUpdateDto)
        {
            try
            {
                var reservation = await _context.Reservations
                    .FirstOrDefaultAsync(r => r.Id == id);

                if (reservation == null)
                    throw new KeyNotFoundException($"Бронирование с ID {id} не найдено");

                _mapper.Map(reservationUpdateDto, reservation);
                await _context.SaveChangesAsync();
            }
            catch (KeyNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception($"Ошибка при обновлении бронирования с ID {id}", ex);
            }
        }

        /// <summary>
        /// Удалить бронирование
        /// </summary>
        public async Task DeleteReservationAsync(int id)
        {
            try
            {
                var reservation = await _context.Reservations
                    .FirstOrDefaultAsync(r => r.Id == id);

                if (reservation == null)
                    throw new KeyNotFoundException($"Бронирование с ID {id} не найдено");

                _context.Reservations.Remove(reservation);
                await _context.SaveChangesAsync();
            }
            catch (KeyNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception($"Ошибка при удалении бронирования с ID {id}", ex);
            }
        }
    }
}