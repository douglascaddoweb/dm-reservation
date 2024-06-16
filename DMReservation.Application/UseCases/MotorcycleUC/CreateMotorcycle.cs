using DMReservation.Application.Interfaces.Services;
using DMReservation.Application.Interfaces.UseCases.MotorcycleUC;
using DMReservation.Domain.DTOs;
using DMReservation.Domain.Entities;
using DMReservation.Domain.Interfaces.Infra;
using DMReservation.Domain.Settings;

namespace DMReservation.Application.UseCases.MotorcycleUC
{
    public class CreateMotorcycle : ICreateMotorcycle
    {
        private readonly IMotorcycleRepository _motorcycleRepository;
        private readonly IMotorcycleService _motorcycleService;

        public CreateMotorcycle(IMotorcycleRepository motorcycleRepository, IMotorcycleService motorcycleService)
        {
            _motorcycleRepository = motorcycleRepository;
            _motorcycleService = motorcycleService;
        }

        public async Task ExecuteAsync(CreateMotorcycleDto motor)
        {
            try
            {
                if (await _motorcycleService.GetMotorcycleWithPlate(motor.LicensePlate)) throw new Exception(MessageSetting.MotorcycleRegistered);

                Motorcycle motorcycle = new(motor.Year, motor.Model, motor.LicensePlate);

                await _motorcycleRepository.AddAsync(motorcycle);
                await _motorcycleRepository.CommitAsync();

            } 
            catch (Exception ex)
            {
                throw ex;
            }
        }

        
    }
}
