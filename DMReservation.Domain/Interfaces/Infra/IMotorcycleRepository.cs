using DMReservation.Domain.Entities;

namespace DMReservation.Domain.Interfaces.Infra
{
    public interface IMotorcycleRepository : IRepositoryGeneric<Motorcycle, int>
    {
        /// <summary>
        /// Get All motorcycles with license plate
        /// </summary>
        /// <param name="licensePlate"></param>
        /// <returns></returns>
        Task<List<Motorcycle>> GetAllWithLicensePlateAsync(string licensePlate);

        Task<bool> VerifyMotorcycleExistAsync(string licenseplate);
    }
}
