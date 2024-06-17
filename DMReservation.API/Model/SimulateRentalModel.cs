using System.ComponentModel.DataAnnotations;

namespace DMReservation.API.Model
{
    public class SimulateRentalModel
    {
        [Required]
        public int IdDeliveryMan { get; set; }

        [Required]
        public DateTime DateFinish { get; set; }
    }
}
