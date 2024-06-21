using DMReservation.Domain.Entities;

namespace DMReservation.Domain.Interfaces.Infra
{
    public interface IRentalPlanRepository : IRepositoryGeneric<RentalPlan, short>
    {

        /// <summary>
        /// Retorna um plano de locação de acordo com a quantidade de dias
        /// </summary>
        /// <param name="days"></param>
        /// <returns></returns>
        Task<RentalPlan> GetRentalPlanAsync(int days);

        /// <summary>
        /// Retora um plano de locação com a maior quantidade de dias disponivel
        /// </summary>
        /// <returns></returns>
        Task<RentalPlan> GetMaxDaysPlanAsync();
    }
}
