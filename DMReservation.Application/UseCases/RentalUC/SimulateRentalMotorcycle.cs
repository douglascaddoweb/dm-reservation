using DMReservation.Application.Interfaces.UseCases.RentalUC;
using DMReservation.Domain.DTOs;
using DMReservation.Domain.Entities;
using DMReservation.Domain.Interfaces.Infra;
using DMReservation.Domain.Settings;

namespace DMReservation.Application.UseCases.RentalUC
{
    public class SimulateRentalMotorcycle : ISimulateRentalMotorcycle
    {
        private readonly IRentalPlanRepository _planRepository;

        public SimulateRentalMotorcycle(
            IRentalPlanRepository planRepository)
        {
            _planRepository = planRepository;
        }


        public async Task<DetailSimulateRentalDto> ExecuteAsync(RentalMotorcycleDto rentalMotorcycle)
        {
            if (rentalMotorcycle.DateFinish < DateTime.Now) throw new Exception(MessageSetting.DateFinishInvalid);


            TimeSpan days = rentalMotorcycle.DateFinish - DateTime.Now;

            RentalPlan rentalPlan = await _planRepository.GetRentalPlanAsync(days.Days);

            if (rentalPlan is not RentalPlan) throw new Exception("Impossible generate the value to the rental.");

            Rental rental = new Rental(rentalPlan, DateTime.Now.AddDays(1), rentalMotorcycle.DateFinish);
            rental.CalculatePrice(rentalMotorcycle.DateFinish);

            return new DetailSimulateRentalDto(rental.DateForecastFinish, rental.DateStart, rental.Total);
        }
    }
}
