using DMReservation.Domain.DTOs;

namespace DMReservation.Application.Interfaces.UseCases.RentalUC
{
    public interface IRentalMotorcycle
    {
        Task<DetailSimulateRentalDto> ExecuteAsync(RentalMotorcycleDto rentalMotorcycle);
    }
}
