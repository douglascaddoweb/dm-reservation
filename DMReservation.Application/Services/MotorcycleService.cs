using DMReservation.Application.Interfaces.Services;
using DMReservation.Domain.Interfaces.Infra;

namespace DMReservation.Application.Services
{
    public class MotorcycleService : IMotorcycleService
    {
        public readonly IMotorcycleRepository _motorcycleRepository;

        public MotorcycleService(IMotorcycleRepository motorcycleRepository)
        {
            _motorcycleRepository = motorcycleRepository;
        }
        public async Task<bool> GetMotorcycleWithPlate(string licenseplate)
        {
            return await _motorcycleRepository.VerifyMotorcycleExistAsync(licenseplate);
        }
    }
}
