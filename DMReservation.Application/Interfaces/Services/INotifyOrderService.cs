using DMReservation.Domain.DTOs;

namespace DMReservation.Application.Interfaces.Services
{
    public interface INotifyOrderService
    {
        Task<List<NotifyOrderDto>> GetAll(int idorder);
    }
}
