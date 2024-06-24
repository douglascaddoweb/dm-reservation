using DMReservation.Domain.DTOs;

namespace DMReservation.Application.Interfaces.UseCases.MotorcycleUC
{
    public interface IUpdateMotorcycle
    {
        /// <summary>
        /// Atualiza a motocicleta somente a placa
        /// </summary>
        /// <param name="motor"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        Task<MotorcycleDto> ExecuteAsync(UpdateMotorcycleDto motor);
    }
}
