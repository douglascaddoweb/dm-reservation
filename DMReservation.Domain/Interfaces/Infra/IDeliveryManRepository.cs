using DMReservation.Domain.Entities;
using DMReservation.Domain.ValueObjects;

namespace DMReservation.Domain.Interfaces.Infra
{
    public interface IDeliveryManRepository : IRepositoryGeneric<DeliveryMan, int>
    {
        /// <summary>
        /// Consulta um entregador por um cnpj informado
        /// </summary>
        /// <param name="cnpj"></param>
        /// <returns></returns>
        Task<DeliveryMan> GetDeliveryManWithCnpjAsync(Cnpj cnpj);

        /// <summary>
        /// Consulta um entregado por uma CNH informada
        /// </summary>
        /// <param name="cnh"></param>
        /// <returns></returns>
        Task<DeliveryMan> GetDeliveryManWithCnhAsync(Cnh cnh);

        /// <summary>
        /// Consulta entregadores aptos a realizar entregas
        /// </summary>
        /// <returns></returns>
        Task<List<DeliveryMan>> GetDeliveryManAvailableAsync();
    }
}
