using RestaurantAPI10.DTOs.CustomerDTO;

namespace RestaurantAPI10.Services
{
    /// <summary>
    /// Интерфейс сервиса для работы с клиентами
    /// </summary>
    public interface ICustomerService
    {
        /// <summary>
        /// Получить всех клиентов
        /// </summary>
        Task<IEnumerable<CustomerReadDto>> GetAllCustomersAsync();

        /// <summary>
        /// Получить клиента по ID
        /// </summary>
        Task<CustomerReadDto> GetCustomerByIdAsync(int id);

        /// <summary>
        /// Создать нового клиента
        /// </summary>
        Task<CustomerReadDto> CreateCustomerAsync(CustomerCreateDto customerCreateDto);

        /// <summary>
        /// Обновить клиента
        /// </summary>
        Task UpdateCustomerAsync(int id, CustomerCreateDto customerUpdateDto);

        /// <summary>
        /// Удалить клиента
        /// </summary>
        Task DeleteCustomerAsync(int id);
    }
}