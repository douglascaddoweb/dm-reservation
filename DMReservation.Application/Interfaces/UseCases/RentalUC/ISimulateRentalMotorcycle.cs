using DMReservation.Domain.DTOs;

namespace DMReservation.Application.Interfaces.UseCases.RentalUC
{
    public interface ISimulateRentalMotorcycle
    {

        /// <summary>
        /// Realiza uma simulação para os valores de locação aproximada
        /// </summary>
        /// <param name="rentalMotorcycle"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        Task<DetailSimulateRentalDto> ExecuteAsync(RentalMotorcycleDto rentalMotorcycle);
    }
}
