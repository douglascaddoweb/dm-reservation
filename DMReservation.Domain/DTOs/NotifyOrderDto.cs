namespace DMReservation.Domain.DTOs
{
    public class NotifyOrderDto
    {
        public int Id { get; set; }

        public int IdDeliveryMan { get; set; }

        public int IdOrder { get; set; }

        public DateTime CreatedAt { get; set; }

        public virtual DeliveryManDto DeliveryMan { get; set; }

        public virtual OrderDto Order { get; set; }
    }
}
