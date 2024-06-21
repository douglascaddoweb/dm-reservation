using DMReservation.Domain.Entities;
using DMReservation.Domain.Interfaces.Infra;
using DMReservation.Infra.Context;

namespace DMReservation.Infra.Repositories
{
    public class RentalRepository : RepositoryGeneric<Rental, int>, IRentalRepository
    {
        public RentalRepository(DataContext dataContext) : base(dataContext)
        {
        }
    }
}
