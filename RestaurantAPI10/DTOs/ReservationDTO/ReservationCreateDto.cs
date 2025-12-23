namespace RestaurantAPI10.DTOs.ReservationDTO
{
    /// <summary>
    /// DTO для создания бронирования
    /// </summary>
    public class ReservationCreateDto
    {
        public DateTime ReservationDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public int NumberOfGuests { get; set; }
        public string SpecialRequests { get; set; } = string.Empty;
        public int CustomerId { get; set; }
        public int TableId { get; set; }
    }
}