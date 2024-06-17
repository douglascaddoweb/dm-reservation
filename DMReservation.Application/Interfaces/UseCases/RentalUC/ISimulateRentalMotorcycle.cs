using DMReservation.Domain.DTOs;

namespace DMReservation.Application.Interfaces.UseCases.RentalUC
{
    public interface ISimulateRentalMotorcycle
    {
        Task<DetailSimulateRentalDto> ExecuteAsync(RentalMotorcycleDto rentalMotorcycle);
    }
}
