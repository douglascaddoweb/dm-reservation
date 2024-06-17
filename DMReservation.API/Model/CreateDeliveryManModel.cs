using System.ComponentModel.DataAnnotations;

namespace DMReservation.API.Model
{
    public class CreateDeliveryManModel
    {
        [Required]
        public string  Name { get; set; }

        [Required]
        public string Cnpj { get; set; }

        [Required]
        public string Cnh { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }

        [Required]
        public string TypeCnh { get; set; }
    }
}
