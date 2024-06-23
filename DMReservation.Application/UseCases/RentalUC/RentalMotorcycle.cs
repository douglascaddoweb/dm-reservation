using DMReservation.Application.Interfaces.UseCases.RentalUC;
using DMReservation.Domain.DTOs;
using DMReservation.Domain.Entities;
using DMReservation.Domain.Exceptions;
using DMReservation.Domain.Interfaces.Infra;
using DMReservation.Domain.Settings;

namespace DMReservation.Application.UseCases.RentalUC
{
    public class RentalMotorcycle : IRentalMotorcycle
    {
        private readonly IRentalPlanRepository _planRepository;
        private readonly IDeliveryManRepository _deliveryManRepository;
        private readonly IMotorcycleRepository _motorcycleRepository;
        private readonly IRentalRepository _rentalRepository;

        public RentalMotorcycle(
            IDeliveryManRepository deliveryManRepository,
            IRentalPlanRepository planRepository,
            IRentalRepository rentalRepository,
            IMotorcycleRepository motorcycleRepository)
        {
            _deliveryManRepository = deliveryManRepository;
            _planRepository = planRepository;
            _motorcycleRepository = motorcycleRepository;
            _rentalRepository = rentalRepository;
        }

        /// <summary>
        /// Realiza a locação da motocicleta para um entregador
        /// </summary>
        /// <param name="rentalMotorcycle"></param>
        /// <returns></returns>
        public async Task<DetailSimulateRentalDto> ExecuteAsync(RentalMotorcycleDto rentalMotorcycle)
        {
            try
            {
                Motorcycle motorcycle = await _motorcycleRepository.GetMotorcycleAvailableAsync();

                if (motorcycle is not Motorcycle)
                    throw new ApplicationBaseException(MessageSetting.AnythingMotorcyleToRent, "RLMT01");

                DeliveryMan deliveryMan = await _deliveryManRepository.FindIdAsync(rentalMotorcycle.IdDeliveryMan);

                if (deliveryMan is not DeliveryMan)
                    throw new ApplicationBaseException(MessageSetting.DeliveryManNotFound, "RLMT02");

                if (deliveryMan.TypeCnh.Value == "B")
                    throw new ApplicationBaseException(MessageSetting.DeliveryManNotHabilit, "RLMT03");

                if (rentalMotorcycle.DateFinish < DateTime.Now)
                    throw new ApplicationBaseException(MessageSetting.DateFinishInvalid, "RLMT04");

                TimeSpan days = rentalMotorcycle.DateFinish - DateTime.Now;

                RentalPlan rentalPlan = await _planRepository.GetMaxDaysPlanAsync();

                if (rentalPlan.Days > days.Days)
                    rentalPlan = await _planRepository.GetRentalPlanAsync(days.Days);

                if (rentalPlan is not RentalPlan)
                    throw new ApplicationBaseException(MessageSetting.ErrorGenereateValueRental, "RLMT05");

                Rental rental = new Rental(motorcycle, deliveryMan, rentalPlan, DateTime.Now.AddDays(1), rentalMotorcycle.DateFinish);
                rental.CalculatePrice(rentalMotorcycle.DateFinish);

                await _rentalRepository.AddAsync(rental);
                await _rentalRepository.CommitAsync();

                return new DetailSimulateRentalDto(rental.DateForecastFinish, rental.DateStart, rental.Total);
            }
            catch (ApplicationBaseException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ApplicationBaseException(ex.Message, MessageSetting.ProcessError, "GNRLMT");
            }
        }
    }
}
