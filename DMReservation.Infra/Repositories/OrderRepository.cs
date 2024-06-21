using DMReservation.Domain.Entities;
using DMReservation.Domain.Interfaces.Infra;
using DMReservation.Infra.Context;

namespace DMReservation.Infra.Repositories
{
    public class OrderRepository : RepositoryGeneric<Order, int>, IOrderRepository
    {
        public OrderRepository(DataContext dataContext) : base(dataContext)
        {
        }
    }
}
