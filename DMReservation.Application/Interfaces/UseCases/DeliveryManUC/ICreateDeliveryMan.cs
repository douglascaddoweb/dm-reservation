using DMReservation.Domain.DTOs;

namespace DMReservation.Application.Interfaces.UseCases.DeliveryManUC
{
    public interface ICreateDeliveryMan
    {
        Task ExecuteAsync(CreateDeliveryManDto model);
    }
}
