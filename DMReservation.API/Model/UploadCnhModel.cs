using System.ComponentModel.DataAnnotations;

namespace DMReservation.API.Model
{
    public class UploadCnhModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public IFormFile File { get; set; }
    }
}
