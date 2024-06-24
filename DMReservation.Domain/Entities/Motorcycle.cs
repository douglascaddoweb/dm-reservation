using DMReservation.Domain.Exceptions;
using DMReservation.Domain.Settings;
using DMReservation.Domain.ValueObjects;

namespace DMReservation.Domain.Entities
{
    public class Motorcycle : Entity<Motorcycle, int>
    {
        public short Year { get; private set; }

        public string Model { get; private set; }

        public LicensePlate LicensePlate { get; private set; }

        private List<Rental> _rental;

        public virtual IReadOnlyCollection<Rental> Rentals => _rental?.AsReadOnly();

        protected  Motorcycle()
        {
            _rental = new List<Rental>();
        }

        public Motorcycle(short year, string model, string licensePlate)
        {
            Year = year;
            Model = model.ToUpper();

            LicensePlate license = new LicensePlate(licensePlate);
            if (!license.IsValid)
                throw new ApplicationBaseException(MessageSetting.LincensePlateInvalid, "DMMT01");
            
            _rental = new List<Rental>();
            LicensePlate =  license;
        }

        public void ChangeLicensePlate(string licensePlate)
        {
            LicensePlate license = new LicensePlate(licensePlate);
            if (!license.IsValid)
                throw new ApplicationBaseException(MessageSetting.LincensePlateInvalid, "DMMT02");

            LicensePlate = license;
        }

        public void AddRentals(Rental rental)
        {
            if (_rental == null)
                _rental = new List<Rental>();

            _rental.Add(rental);
        }
    }
}
