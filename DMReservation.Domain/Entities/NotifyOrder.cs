namespace DMReservation.Domain.Entities
{
    public class NotifyOrder : Entity<NotifyOrder, int>
    {
        public int IdDeliveryMan { get; private set; }

        public int IdOrder { get; set; }

        public DateTime CreatedAt { get; private set; }

        public virtual DeliveryMan DeliveryMan { get; private set; }

        public virtual Order Order { get; private set; }

        protected NotifyOrder()
        {
            
        }

        public NotifyOrder(DeliveryMan deliveryMan, Order order)
        {
            IdDeliveryMan = deliveryMan.Id;
            IdOrder = order.Id;
            DeliveryMan = deliveryMan;
            Order = order;
            CreatedAt = DateTime.Now;
        }
    }
}
