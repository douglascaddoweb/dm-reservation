﻿using DMReservation.Application.Interfaces.Services;
using DMReservation.Application.Interfaces.UseCases.MotorcycleUC;
using DMReservation.Domain.DTOs;
using DMReservation.Domain.Entities;
using DMReservation.Domain.Exceptions;
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

        /// <summary>
        /// Cria o cadastro de uma motocicleta
        /// </summary>
        /// <param name="motor"></param>
        /// <returns></returns>
        public async Task ExecuteAsync(CreateMotorcycleDto motor)
        {
            try
            {
                if (await _motorcycleService.GetMotorcycleWithPlate(motor.LicensePlate)) throw new ApplicationBaseException(MessageSetting.MotorcycleRegistered, "CRMT");

                Motorcycle motorcycle = new(motor.Year, motor.Model, motor.LicensePlate);

                await _motorcycleRepository.AddAsync(motorcycle);
                await _motorcycleRepository.CommitAsync();

            } 
            catch (ApplicationBaseException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ApplicationBaseException(ex.Message, MessageSetting.ProcessError, "GNCRMT");
            }
        }

        
    }
}
