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

        /// <summary>
        /// Consulta se já existe uma moto com a placa informada
        /// </summary>
        /// <param name="licenseplate"></param>
        /// <returns></returns>
        Task<bool> VerifyMotorcycleExistAsync(string licenseplate);

        /// <summary>
        /// Verifica se existe motos disponiveis para realizar locação
        /// </summary>
        /// <returns></returns>
        Task<Motorcycle> GetMotorcycleAvailableAsync();


        /// <summary>
        /// Retora uma motocicleta com suas locações 
        /// </summary>
        /// <param name="idMotorcycle"></param>
        /// <returns></returns>
        Task<Motorcycle> GetMotorcycleWithRentalsAsync(int idMotorcycle);
    }
}
