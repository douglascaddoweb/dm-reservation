using DMReservation.Application.Interfaces.Services;
using DMReservation.Application.Interfaces.UseCases.MotorcycleUC;
using DMReservation.Domain.DTOs;
using DMReservation.Domain.Entities;
using DMReservation.Domain.Exceptions;
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
            try
            {
                if (await _motorcycleService.GetMotorcycleWithPlate(motor.LicensePlate)) throw new ApplicationBaseException(MessageSetting.MotorcycleRegistered, "UPMT01");

                Motorcycle motorcycle = await _motorcycleRepository.FindIdAsync(motor.Id);

                if (motorcycle == null) throw new ApplicationBaseException(MessageSetting.RegistryNotFound, "UPMT02");

                motorcycle.ChangeLicensePlate(motor.LicensePlate);

                _motorcycleRepository.Update(motorcycle);

                await _motorcycleRepository.CommitAsync();
            } catch (ApplicationBaseException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ApplicationBaseException(ex.Message, MessageSetting.ProcessError, "GNUPMT");
            }
        }
    }
}
