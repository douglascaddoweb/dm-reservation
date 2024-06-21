using DMReservation.Application.Interfaces.Services;
using DMReservation.Application.Interfaces.UseCases.MotorcycleUC;
using DMReservation.Domain.DTOs;
using DMReservation.Domain.Entities;
using DMReservation.Domain.Interfaces.Infra;
using DMReservation.Domain.Settings;

namespace DMReservation.Application.UseCases.MotorcycleUC
{
    public class UpdateMotorcycle : IUpdateMotorcycle
    {
        private readonly IMotorcycleRepository _motorcycleRepository;
        private readonly IMotorcycleService _motorcycleService;

        public UpdateMotorcycle(IMotorcycleRepository motorcycleRepository, IMotorcycleService motorcycleService)
        {
            _motorcycleRepository = motorcycleRepository; 
            _motorcycleService = motorcycleService;
        }

        /// <summary>
        /// Atualiza a motocicleta somente a placa
        /// </summary>
        /// <param name="motor"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task ExecuteAsync(UpdateMotorcycleDto motor)
        {
            if (await _motorcycleService.GetMotorcycleWithPlate(motor.LicensePlate)) throw new Exception(MessageSetting.MotorcycleRegistered);

            Motorcycle motorcycle = await _motorcycleRepository.FindIdAsync(motor.Id);

            if (motorcycle == null) throw new Exception(MessageSetting.RegistryNotFound);

            motorcycle.ChangeLicensePlate(motor.LicensePlate);

            _motorcycleRepository.Update(motorcycle);

            await _motorcycleRepository.CommitAsync();
        }
    }
}
