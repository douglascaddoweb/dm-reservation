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

        /// <summary>
        /// Retorna um plano de locação de acordo com a quantidade de dias
        /// </summary>
        /// <param name="days"></param>
        /// <returns></returns>
        public async Task<RentalPlan> GetRentalPlanAsync(int days)
        {
            return await _entities.OrderBy(o => o.Days).FirstOrDefaultAsync(w => w.Days > days || w.Days < days);
        }

        /// <summary>
        /// Retora um plano de locação com a maior quantidade de dias disponivel
        /// </summary>
        /// <returns></returns>
        public async Task<RentalPlan> GetMaxDaysPlanAsync()
        {
            return (await _entities.ToListAsync()).MaxBy(w => w.Days);
        }
    }
}
