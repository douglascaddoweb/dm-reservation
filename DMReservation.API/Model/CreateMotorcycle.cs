using DMReservation.API.Model.Attibutes;
using System.ComponentModel.DataAnnotations;

namespace DMReservation.API.Model
{
    public class CreateMotorcycle
    {
        [Required]
        [ValidateDate(ErrorMessage = "The Date should is between")]
        public short Year { get; set; }
        [Required]
        public string Model { get; set; }
        [Required]
        public string LicensePlate { get; set; }
    }
}
