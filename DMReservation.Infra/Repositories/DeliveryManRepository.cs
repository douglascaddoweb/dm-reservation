using DMReservation.Domain.Entities;
using DMReservation.Domain.Interfaces.Infra;
using DMReservation.Domain.ValueObjects;
using DMReservation.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace DMReservation.Infra.Repositories
{
    public class DeliveryManRepository : RepositoryGeneric<DeliveryMan, int>, IDeliveryManRepository
    {
        public DeliveryManRepository(DataContext dataContext) : base(dataContext)
        {
        }

        public async Task<DeliveryMan> GetDeliveryManWithCnpjAsync(Cnpj cnpj)
        {
            return await _entities.FirstOrDefaultAsync(f => f.Cnpj.Value == cnpj.Value);
        }

        public async Task<DeliveryMan> GetDeliveryManWithCnhAsync(Cnh cnh)
        {
            return await _entities.FirstOrDefaultAsync(f => f.Cnh.Value == cnh.Value);
        }
    }
}
