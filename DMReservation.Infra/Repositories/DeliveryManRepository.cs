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

        /// <summary>
        /// Consulta um entregador por um cnpj informado
        /// </summary>
        /// <param name="cnpj"></param>
        /// <returns></returns>
        public async Task<DeliveryMan> GetDeliveryManWithCnpjAsync(Cnpj cnpj)
        {
            return await _entities.FirstOrDefaultAsync(f => f.Cnpj.Value == cnpj.Value);
        }

        /// <summary>
        /// Consulta um entregado por uma CNH informada
        /// </summary>
        /// <param name="cnh"></param>
        /// <returns></returns>
        public async Task<DeliveryMan> GetDeliveryManWithCnhAsync(Cnh cnh)
        {
            return await _entities.FirstOrDefaultAsync(f => f.Cnh.Value == cnh.Value);
        }

        /// <summary>
        /// Consulta entregadores aptos a realizar entregas
        /// </summary>
        /// <returns></returns>
        public async Task<List<DeliveryMan>> GetDeliveryManAvailableAsync()
        {
            return await _context.DeliveryMen.FromSqlRaw("select t.id Id, t.name, t.cnpj Cnpj_Value, t.birthdate, t.typecnh TypeCnh_Value, t.cnh Cnh_Value, t.image Image from deliveryman t left join rental r on t.id = r.iddeliveryman or r.id is null left join orderdeliveryman o on t.id = o.iddeliveryman or o.id is null left join \"order\" o2 on o.idorder = o2.id where t.typecnh != 'B' and r.status = 1 and (o2.status != 2 or o2.status is null)").ToListAsync();
        }
    }
}
