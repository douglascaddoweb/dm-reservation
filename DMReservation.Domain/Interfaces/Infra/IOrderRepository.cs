using DMReservation.Domain.Entities;

namespace DMReservation.Domain.Interfaces.Infra
{
    public interface IOrderRepository : IRepositoryGeneric<Order, int>
    {
    }
}
