using DMReservation.Domain.Entities;
using DMReservation.Domain.Interfaces.Infra;
using DMReservation.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace DMReservation.Infra.Repositories
{
    public class RentalPlanRepository : RepositoryGeneric<RentalPlan, short>, IRentalPlanRepository
    {
        public RentalPlanRepository(DataContext dataContext) : base(dataContext)
        {
        }

        public async Task<RentalPlan> GetRentalPlanAsync(int days)
        {
            return await _entities.OrderBy(o => o.Days).FirstOrDefaultAsync(w => w.Days > days || w.Days < days);
        }

        public async Task<RentalPlan> GetMaxDaysPlanAsync()
        {
            return (await _entities.ToListAsync()).MaxBy(w => w.Days);
        }
    }
}
