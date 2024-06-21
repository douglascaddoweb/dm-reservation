using DMReservation.Domain.Enums;

namespace DMReservation.Domain.DTOs
{
    public class OrderDto
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }

        public decimal Price { get; set; }

        public StatusOrder Status { get; set; }
    }
}
