using DMReservation.Domain.Entities;
using DMReservation.Domain.ValueObjects;

namespace DMReservation.Domain.Interfaces.Infra
{
    public interface IDeliveryManRepository : IRepositoryGeneric<DeliveryMan, int>
    {
        Task<DeliveryMan> GetDeliveryManWithCnpjAsync(Cnpj cnpj);
        Task<DeliveryMan> GetDeliveryManWithCnhAsync(Cnh cnh);
    }
}
