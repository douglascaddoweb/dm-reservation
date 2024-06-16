namespace DMReservation.Domain.Entities
{
    public class Motorcycle : Entity<Motorcycle, int>
    {
        public short Year { get; private set; }

        public string Model { get; private set; }

        public string LicensePlate { get; private set; }

        protected  Motorcycle()
        {
            
        }

        public Motorcycle(short year, string model, string licensePlate)
        {
            Year = year;
            Model = model;
            LicensePlate = licensePlate;
        }

        public void ChangeLicensePlate(string licensePlate) => LicensePlate = licensePlate;
    }
}
