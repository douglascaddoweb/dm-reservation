using DMReservation.Domain.Enums;

namespace DMReservation.Domain.Entities
{
    public class Rental : Entity<Rental, int>
    {
        public int MotorcycleId { get; private set; }

        public short RentalPlanId { get; private set; }

        public int DeliveryManId { get; private set; }

        public DateTime DateStart { get; private set; }

        public DateTime? DateFinish { get; private set; }

        public DateTime DateForecastFinish { get; private set; }

        public DateTime CreatedAt { get; private set; }

        public StatusRental Status { get; private set; }

        public decimal Price { get; private set; }

        public decimal Fine { get; private set; }

        public decimal ExtraPrice { get; private set; }
        public decimal Total { get; set; }

        public virtual Motorcycle Motorcycle { get; private set; }
        public virtual DeliveryMan  DeliveryMan { get; private set; }
        public virtual RentalPlan RentalPlan { get; private set; }

        protected Rental()
        {
            
        }

        public Rental(Motorcycle motorcycle, DeliveryMan deliveryMan, RentalPlan rentalPlan, DateTime dateStart, DateTime dateFinish)
        {
            if (dateStart < DateTime.Now) return;

            MotorcycleId = motorcycle.Id;
            DeliveryManId = deliveryMan.Id;
            RentalPlanId = rentalPlan.Id;
            DateStart = dateStart;
            DateForecastFinish = dateFinish;
            Price = rentalPlan.Price * rentalPlan.Days;
            Status = StatusRental.Leased;

            Motorcycle = motorcycle;
            DeliveryMan = deliveryMan;
            RentalPlan = rentalPlan;
        }

        public Rental(RentalPlan rentalPlan, DateTime dateStart, DateTime dateFinish)
        {
            if (dateStart < DateTime.Now) return;

            RentalPlanId = rentalPlan.Id;
            DateStart = dateStart;
            DateForecastFinish = dateFinish;
            Price = rentalPlan.Price * rentalPlan.Days;

            RentalPlan = rentalPlan;
        }


        public void CalculatePrice(DateTime dateFinish)
        {
            TimeSpan timeStampDaysD = dateFinish - DateStart;

            DateFinish = dateFinish;

            if (RentalPlan.Days == timeStampDaysD.Days)
            {
                Price = RentalPlan.Price * RentalPlan.Days;
                Fine = 0;
                ExtraPrice = 0;
                Total = Price + ExtraPrice + Fine;
            }

            if (RentalPlan.Days > timeStampDaysD.Days)
            {
                Fine = RentalPlan.Fine;
                decimal extraFine = (RentalPlan.Price * (RentalPlan.Days - timeStampDaysD.Days)) * (RentalPlan.Fine / 100);

                Price = (RentalPlan.Price * RentalPlan.Days) + extraFine;
                ExtraPrice = 0;
                Total = Price + ExtraPrice;
            }
            
            if (RentalPlan.Days < timeStampDaysD.Days)
            {
                Fine = 0;
                
                Price = (RentalPlan.Price * RentalPlan.Days);
                ExtraPrice = RentalPlan.ExtraPrice * (timeStampDaysD.Days - RentalPlan.Days);
                Total = Price + ExtraPrice;
            }
        }

    }
}
