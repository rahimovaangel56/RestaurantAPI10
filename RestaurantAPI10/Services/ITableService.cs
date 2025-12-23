using RestaurantAPI10.DTOs.TableDTO;

namespace RestaurantAPI10.Services
{
    /// <summary>
    /// Интерфейс сервиса для работы со столиками
    /// </summary>
    public interface ITableService
    {
        /// <summary>
        /// Получить все столики
        /// </summary>
        Task<IEnumerable<TableReadDto>> GetAllTablesAsync();

        /// <summary>
        /// Получить столик по ID
        /// </summary>
        Task<TableReadDto> GetTableByIdAsync(int id);

        /// <summary>
        /// Создать новый столик
        /// </summary>
        Task<TableReadDto> CreateTableAsync(TableCreateDto tableCreateDto);

        /// <summary>
        /// Обновить столик
        /// </summary>
        Task UpdateTableAsync(int id, TableCreateDto tableUpdateDto);

        /// <summary>
        /// Удалить столик
        /// </summary>
        Task DeleteTableAsync(int id);
    }
}