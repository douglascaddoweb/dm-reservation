using DMReservation.Domain.Entities;
using DMReservation.Domain.Interfaces.Infra;
using DMReservation.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace DMReservation.Infra.Repositories
{
    public class MotorcycleRepository : RepositoryGeneric<Motorcycle, int>, IMotorcycleRepository
    {
        public MotorcycleRepository(DataContext dataContext) : base(dataContext)
        {
        }

        public async Task<List<Motorcycle>> GetAllWithLicensePlateAsync(string licensePlate)
        {
            if (string.IsNullOrWhiteSpace(licensePlate))
            {
                return await _entities.ToListAsync();
            }

            return await _entities.Where(w => w.LicensePlate == licensePlate).ToListAsync();
        }

        public async Task<bool> VerifyMotorcycleExistAsync(string licenseplate)
        {
            return await _entities.AnyAsync(x => x.LicensePlate == licenseplate);
        }
    }
}
