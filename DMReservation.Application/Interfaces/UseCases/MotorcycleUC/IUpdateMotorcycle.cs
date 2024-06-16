using DMReservation.Domain.DTOs;

namespace DMReservation.Application.Interfaces.UseCases.MotorcycleUC
{
    public interface IUpdateMotorcycle
    {
        Task ExecuteAsync(UpdateMotorcycleDto motor);
    }
}
