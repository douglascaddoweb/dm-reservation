using System.ComponentModel.DataAnnotations;

namespace DMReservation.Domain.DTOs
{
    public class UpdateMotorcycleDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string LicensePlate { get; set; }
    }
}
