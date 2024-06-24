namespace DMReservation.Domain.Entities
{
    public class RentalPlan : Entity<RentalPlan,  short>
    {
        public short Days { get; private set; }

        public decimal Price { get; private set; }

        public decimal Fine { get; private set; }

        public decimal ExtraPrice { get; private set; }

        protected RentalPlan()
        {
            
        }

        public RentalPlan(short days, decimal price, decimal fine, decimal extraPrice)
        {
            Days = days;
            Price = price;
            Fine = fine;
            ExtraPrice = extraPrice;
        }
    }
}
