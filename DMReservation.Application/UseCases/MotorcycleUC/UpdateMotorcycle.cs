using AutoMapper;
using DMReservation.Application.Interfaces.Services;
using DMReservation.Application.Interfaces.UseCases.MotorcycleUC;
using DMReservation.Domain.DTOs;
using DMReservation.Domain.Entities;
using DMReservation.Domain.Exceptions;
using DMReservation.Domain.Interfaces.Infra;
using DMReservation.Domain.Settings;
using DMReservation.Domain.ValueObjects;

namespace DMReservation.Application.UseCases.MotorcycleUC
{
    public class UpdateMotorcycle : IUpdateMotorcycle
    {
        private readonly IMotorcycleRepository _motorcycleRepository;
        private readonly IMotorcycleService _motorcycleService;
        private readonly IMapper _mapper;

        public UpdateMotorcycle(IMotorcycleRepository motorcycleRepository, 
            IMotorcycleService motorcycleService,
            IMapper mapper)
        {
            _motorcycleRepository = motorcycleRepository; 
            _motorcycleService = motorcycleService;
            _mapper = mapper;
        }

        /// <summary>
        /// Atualiza a motocicleta somente a placa
        /// </summary>
        /// <param name="motor"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<MotorcycleDto> ExecuteAsync(UpdateMotorcycleDto motor)
        {
            try
            {
                if (await _motorcycleService.GetMotorcycleWithPlate(motor.LicensePlate)) throw new ApplicationBaseException(MessageSetting.MotorcycleRegistered, "UPMT01");

                Motorcycle motorcycle = await _motorcycleRepository.FindIdAsync(motor.Id);

                if (motorcycle is not Motorcycle) throw new ApplicationBaseException(MessageSetting.RegistryNotFound, "UPMT02");

                motorcycle.ChangeLicensePlate(motor.LicensePlate);

                _motorcycleRepository.Update(motorcycle);

                await _motorcycleRepository.CommitAsync();
                return _mapper.Map<MotorcycleDto>(motorcycle);
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
