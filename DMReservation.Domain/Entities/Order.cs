using DMReservation.Domain.Enums;

namespace DMReservation.Domain.Entities
{
    public class Order : Entity<Order, int>
    {
        public DateTime CreatedAt { get; private set; }

        public decimal Price { get; private set; }

        public StatusOrder Status { get; private set; }

        public virtual OrderDeliveryMan OrderDeliveryMan { get; set; } 

        protected Order()
        {
            
        }

        public Order(DateTime createdAt, decimal price, StatusOrder status)
        {
            CreatedAt = createdAt;
            Price = price;
            Status = status;
        }

        public void CreateOrderDelivery(DeliveryMan delivery)
        {
            OrderDeliveryMan = new OrderDeliveryMan(delivery, this);         

        }

        public void AcceptDelivery() => Status = StatusOrder.Accept;

        public void Delivered() => Status = StatusOrder.Delivered;
    }
}
