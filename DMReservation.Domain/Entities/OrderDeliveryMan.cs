namespace DMReservation.Domain.Entities
{
    public class OrderDeliveryMan : Entity<OrderDeliveryMan, int>
    {
        public int DeliveryManId { get; private set; }
        public int OrderId { get; private set; }

        public virtual DeliveryMan DeliveryMan { get; private set; }

        public virtual Order Order { get; private set; }

        protected OrderDeliveryMan()
        {
            
        }

        public OrderDeliveryMan(DeliveryMan deliveryMan, Order order)
        {
            DeliveryMan = deliveryMan;
            Order = order;

            DeliveryManId = deliveryMan.Id;
            OrderId = deliveryMan.Id;
        }
    }
}
