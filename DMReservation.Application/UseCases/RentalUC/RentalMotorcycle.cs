using DMReservation.Application.Interfaces.UseCases.RentalUC;
using DMReservation.Domain.DTOs;
using DMReservation.Domain.Entities;
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


        public async Task<DetailSimulateRentalDto> ExecuteAsync(RentalMotorcycleDto rentalMotorcycle)
        {
            try
            {

                Motorcycle motorcycle = await _motorcycleRepository.GetMotorcycleAvailableAsync();

                if (motorcycle is not Motorcycle) throw new Exception("Don't have any motorcycle to rent.");

                DeliveryMan deliveryMan = await _deliveryManRepository.FindIdAsync(rentalMotorcycle.IdDeliveryMan);

                if (deliveryMan is not DeliveryMan) throw new Exception("Not found any delivery man");

                if (deliveryMan.TypeCnh.Value == "B") throw new Exception("Delivery man can't drive motorcycle.");
            
                if (rentalMotorcycle.DateFinish < DateTime.Now) throw new Exception(MessageSetting.DateFinishInvalid);
            
                TimeSpan days = rentalMotorcycle.DateFinish - DateTime.Now;

                RentalPlan rentalPlan = await _planRepository.GetMaxDaysPlanAsync();

                if (rentalPlan.Days > days.Days)
                {
                    rentalPlan = await _planRepository.GetRentalPlanAsync(days.Days);
                }

                if (rentalPlan is not RentalPlan) throw new Exception("Impossible generate the value to the rental.");

                Rental rental = new Rental(motorcycle, deliveryMan, rentalPlan, DateTime.Now.AddDays(1), rentalMotorcycle.DateFinish);
                rental.CalculatePrice(rentalMotorcycle.DateFinish);

                await _rentalRepository.AddAsync(rental);
                await _rentalRepository.CommitAsync();

                return new DetailSimulateRentalDto(rental.DateForecastFinish, rental.DateStart, rental.Total);
            } catch (Exception ex)
            {
                throw;
            }
        }
    }
}
