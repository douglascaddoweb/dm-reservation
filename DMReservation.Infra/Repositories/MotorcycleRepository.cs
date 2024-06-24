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

        /// <summary>
        /// Retorna uma lista de motos com a placa informada ou não
        /// </summary>
        /// <param name="licensePlate"></param>
        /// <returns></returns>
        public async Task<List<Motorcycle>> GetAllWithLicensePlateAsync(string licensePlate)
        {
            if (string.IsNullOrWhiteSpace(licensePlate))
            {
                return await _entities.ToListAsync();
            }

            return await _entities.Where(w => w.LicensePlate.Value == licensePlate).ToListAsync();
        }

        /// <summary>
        /// Consulta se já existe uma moto com a placa informada
        /// </summary>
        /// <param name="licenseplate"></param>
        /// <returns></returns>
        public async Task<bool> VerifyMotorcycleExistAsync(string licenseplate)
        {
            return await _entities.AnyAsync(x => x.LicensePlate.Value == licenseplate);
        }

        /// <summary>
        /// Verifica se existe motos disponiveis para realizar locação
        /// </summary>
        /// <returns></returns>
        public async Task<Motorcycle> GetMotorcycleAvailableAsync()
        {

            return await _context.Motorcycles.FromSqlRaw("select m.* from motorcycle m left join rental r on m.id = r.id where r.id is null or r.status = 2 order by m.id asc").FirstOrDefaultAsync();
        }

        /// <summary>
        /// Retora uma motocicleta com suas locações 
        /// </summary>
        /// <param name="idMotorcycle"></param>
        /// <returns></returns>
        public async Task<Motorcycle> GetMotorcycleWithRentalsAsync(int idMotorcycle)
        {
            return await _entities.Include(i => i.Rentals).Where(w => w.Id == idMotorcycle).FirstOrDefaultAsync();  
        }
    }
}
