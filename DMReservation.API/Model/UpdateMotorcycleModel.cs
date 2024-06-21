using System.ComponentModel.DataAnnotations;

namespace DMReservation.API.Model
{
    public class UpdateMotorcycleModel
    {
        [Required]
        public string LicensePlate { get; set; }
    }
}
