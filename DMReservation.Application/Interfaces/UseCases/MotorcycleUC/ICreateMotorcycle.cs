using DMReservation.Domain.DTOs;

namespace DMReservation.Application.Interfaces.UseCases.MotorcycleUC
{
    public interface ICreateMotorcycle
    {

        Task ExecuteAsync(CreateMotorcycleDto motor);
    }
}
