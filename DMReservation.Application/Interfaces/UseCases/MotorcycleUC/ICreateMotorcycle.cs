using DMReservation.Domain.DTOs;

namespace DMReservation.Application.Interfaces.UseCases.MotorcycleUC
{
    public interface ICreateMotorcycle
    {

        /// <summary>
        /// Cria o cadastro de uma motocicleta
        /// </summary>
        /// <param name="motor"></param>
        /// <returns></returns>
        Task ExecuteAsync(CreateMotorcycleDto motor);
    }
}
