using DMReservation.Application.Interfaces.UseCases.MotorcycleUC;
using DMReservation.Domain.Entities;
using DMReservation.Domain.Exceptions;
using DMReservation.Domain.Interfaces.Infra;
using DMReservation.Domain.Settings;

namespace DMReservation.Application.UseCases.MotorcycleUC
{
    public class RemoveMotorcycle : IRemoveMotorcycle
    {
        private readonly IMotorcycleRepository _motorcycleRepository;

        public RemoveMotorcycle(IMotorcycleRepository motorcycleRepository)
        {
            _motorcycleRepository = motorcycleRepository;
        }

        /// <summary>
        /// Exclui uma moto validando se não existe locação para a mesma
        /// </summary>
        /// <param name="idmotorcycle"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task ExecuteAsync(int idmotorcycle)
        {
            try
            {
                Motorcycle motorcycle = await _motorcycleRepository.GetMotorcycleWithRentalsAsync(idmotorcycle);

                if (motorcycle is not Motorcycle)
                    throw new ApplicationBaseException(MessageSetting.RegistryNotFound, "RMMT01");

                if (motorcycle.Rentals is IReadOnlyCollection<Rental> && motorcycle.Rentals.Count >= 1)
                    throw new ApplicationBaseException(MessageSetting.RemoveMotorcycle, "RMMT01");

                _motorcycleRepository.Remove(motorcycle);

                await _motorcycleRepository.CommitAsync();
            }
            catch (ApplicationBaseException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ApplicationBaseException(ex.Message, MessageSetting.ProcessError, "GNRMMT");
            }
        }
    }
}
