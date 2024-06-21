using DMReservation.Domain.ValueObjects;

namespace DMReservation.Domain.DTOs
{
    public class DeliveryManDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public CnpjDto Cnpj { get; set; }

        public DateTime BirthDate { get; set; }

        public CnhDto Cnh { get; set; }
    }
}
