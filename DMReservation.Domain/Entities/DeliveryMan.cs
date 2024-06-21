using DMReservation.Domain.ValueObjects;

namespace DMReservation.Domain.Entities
{
    public class DeliveryMan : Entity<DeliveryMan, int>
    {
        public string Name { get; private set; }

        public Cnpj Cnpj { get; private set; }

        public DateTime BirthDate { get; private set; }

        public Cnh Cnh { get; private set; }

        public TypeCnh TypeCnh { get; private set; }

        public string? Image { get; private set; }

        public virtual List<OrderDeliveryMan> OrderDeliveryMan { get; set; }

        public virtual List<Rental> Rental { get; set; }

        protected DeliveryMan() { }

        public DeliveryMan(string name, Cnpj cnpj, DateTime birthdate, Cnh cnh, TypeCnh typecnh)
        {
            Name = name;
            Cnpj = cnpj;
            BirthDate = birthdate;
            Cnh = cnh;
            TypeCnh = typecnh;
        }

        public void SetImage(string image) => Image = image;

    }
}
