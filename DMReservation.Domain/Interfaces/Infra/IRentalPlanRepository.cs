using DMReservation.Domain.Entities;

namespace DMReservation.Domain.Interfaces.Infra
{
    public interface IRentalPlanRepository : IRepositoryGeneric<RentalPlan, short>
    {
        Task<RentalPlan> GetRentalPlanAsync(int days);
        Task<RentalPlan> GetMaxDaysPlanAsync();
    }
}
