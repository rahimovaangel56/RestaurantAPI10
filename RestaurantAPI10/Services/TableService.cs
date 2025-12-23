using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI10.Data;
using RestaurantAPI10.DTOs.TableDTO;
using RestaurantAPI10.Models;

namespace RestaurantAPI10.Services
{
    /// <summary>
    /// Сервис для работы со столиками
    /// </summary>
    public class TableService : ITableService
    {
        private readonly RestaurantDbContext _context;
        private readonly IMapper _mapper;

        /// <summary>
        /// Конструктор сервиса
        /// </summary>
        public TableService(RestaurantDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Получить все столики
        /// </summary>
        public async Task<IEnumerable<TableReadDto>> GetAllTablesAsync()
        {
            try
            {
                var tables = await _context.Tables
                    .Include(t => t.Restaurant)
                    .OrderBy(t => t.Number)
                    .ToListAsync();

                return _mapper.Map<IEnumerable<TableReadDto>>(tables);
            }
            catch (Exception ex)
            {
                throw new Exception("Ошибка при получении списка столиков", ex);
            }
        }

        /// <summary>
        /// Получить столик по ID
        /// </summary>
        public async Task<TableReadDto> GetTableByIdAsync(int id)
        {
            try
            {
                var table = await _context.Tables
                    .Include(t => t.Restaurant)
                    .FirstOrDefaultAsync(t => t.Id == id);

                if (table == null)
                    throw new KeyNotFoundException($"Столик с ID {id} не найден");

                return _mapper.Map<TableReadDto>(table);
            }
            catch (KeyNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception($"Ошибка при получении столика с ID {id}", ex);
            }
        }

        /// <summary>
        /// Создать новый столик
        /// </summary>
        public async Task<TableReadDto> CreateTableAsync(TableCreateDto tableCreateDto)
        {
            try
            {
                var table = _mapper.Map<Table>(tableCreateDto);

                _context.Tables.Add(table);
                await _context.SaveChangesAsync();

                return _mapper.Map<TableReadDto>(table);
            }
            catch (Exception ex)
            {
                throw new Exception("Ошибка при создании столика", ex);
            }
        }

        /// <summary>
        /// Обновить столик
        /// </summary>
        public async Task UpdateTableAsync(int id, TableCreateDto tableUpdateDto)
        {
            try
            {
                var table = await _context.Tables
                    .FirstOrDefaultAsync(t => t.Id == id);

                if (table == null)
                    throw new KeyNotFoundException($"Столик с ID {id} не найден");

                _mapper.Map(tableUpdateDto, table);
                await _context.SaveChangesAsync();
            }
            catch (KeyNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception($"Ошибка при обновлении столика с ID {id}", ex);
            }
        }

        /// <summary>
        /// Удалить столик
        /// </summary>
        public async Task DeleteTableAsync(int id)
        {
            try
            {
                var table = await _context.Tables
                    .FirstOrDefaultAsync(t => t.Id == id);

                if (table == null)
                    throw new KeyNotFoundException($"Столик с ID {id} не найден");

                _context.Tables.Remove(table);
                await _context.SaveChangesAsync();
            }
            catch (KeyNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception($"Ошибка при удалении столика с ID {id}", ex);
            }
        }
    }
}