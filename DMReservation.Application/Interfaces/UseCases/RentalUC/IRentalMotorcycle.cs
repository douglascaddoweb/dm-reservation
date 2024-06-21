using DMReservation.Domain.DTOs;

namespace DMReservation.Application.Interfaces.UseCases.RentalUC
{
    public interface IRentalMotorcycle
    {

        /// <summary>
        /// Realiza a locação da motocicleta para um entregador
        /// </summary>
        /// <param name="rentalMotorcycle"></param>
        /// <returns></returns>
        Task<DetailSimulateRentalDto> ExecuteAsync(RentalMotorcycleDto rentalMotorcycle);
    }
}
