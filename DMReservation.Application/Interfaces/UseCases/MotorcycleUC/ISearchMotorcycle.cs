using DMReservation.Domain.DTOs;
using DMReservation.Domain.Entities;

namespace DMReservation.Application.Interfaces.UseCases.MotorcycleUC
{
    public interface ISearchMotorcycle
    {
        Task<List<ListMotorcycleDto>> ExecuteAsync(string plate);
    }
}
