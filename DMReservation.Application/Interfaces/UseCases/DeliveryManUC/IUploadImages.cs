using Microsoft.AspNetCore.Http;

namespace DMReservation.Application.Interfaces.UseCases.DeliveryManUC
{
    public interface IUploadImages
    {
        Task ExecuteAsync(IFormFile file, int id);
    }

}
