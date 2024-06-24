namespace DMReservation.Domain.DTOs
{
    public class MotorcycleDto
    {
        public int Id { get; set; }
        public short Year { get; set; }

        public string Model { get; set; }

        public LicensePlateDto LicensePlate { get; set; }

    }
}
