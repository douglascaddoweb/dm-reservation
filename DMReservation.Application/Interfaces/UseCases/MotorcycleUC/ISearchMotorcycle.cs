using DMReservation.Domain.DTOs;

namespace DMReservation.Application.Interfaces.UseCases.MotorcycleUC
{
    public interface ISearchMotorcycle
    {
        /// <summary>
        /// Realiza a busca por motocicletas cadastradas podendo filtrar pela placa
        /// </summary>
        /// <param name="plate"></param>
        /// <returns></returns>
        Task<List<ListMotorcycleDto>> ExecuteAsync(string plate);
    }
}
