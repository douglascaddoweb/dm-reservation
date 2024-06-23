using DMReservation.Application.Interfaces.UseCases.RentalUC;
using DMReservation.Domain.DTOs;
using DMReservation.Domain.Entities;
using DMReservation.Domain.Exceptions;
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

        /// <summary>
        /// Realiza uma simulação para os valores de locação aproximada
        /// </summary>
        /// <param name="rentalMotorcycle"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<DetailSimulateRentalDto> ExecuteAsync(RentalMotorcycleDto rentalMotorcycle)
        {
            try
            {
                if (rentalMotorcycle.DateFinish < DateTime.Now)
                    throw new ApplicationBaseException(MessageSetting.DateFinishInvalid, "SERLMT01");

                TimeSpan days = rentalMotorcycle.DateFinish - DateTime.Now;

                RentalPlan rentalPlan = await _planRepository.GetRentalPlanAsync(days.Days);

                if (rentalPlan is not RentalPlan)
                    throw new ApplicationBaseException(MessageSetting.ErrorGenereateValueRental, "SERLMT02");

                Rental rental = new Rental(rentalPlan, DateTime.Now.AddDays(1), rentalMotorcycle.DateFinish);
                rental.CalculatePrice(rentalMotorcycle.DateFinish);

                return new DetailSimulateRentalDto(rental.DateForecastFinish, rental.DateStart, rental.Total);
            }
            catch (ApplicationBaseException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ApplicationBaseException(ex.Message, MessageSetting.ProcessError, "GNSERLMT");
            }
        }
    }
}
