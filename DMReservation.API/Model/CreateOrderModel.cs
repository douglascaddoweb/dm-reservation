using System.ComponentModel.DataAnnotations;

namespace DMReservation.API.Model
{
    public class CreateOrderModel
    {
        [Required]
        public decimal Price { get; set; }
    }
}
