using DMReservation.Domain.Entities;
using DMReservation.Domain.Interfaces.Infra;
using DMReservation.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace DMReservation.Infra.Repositories
{
    public class NotifyOrderRepository : RepositoryGeneric<NotifyOrder, int>, INotifyOrderRepository
    {
        public NotifyOrderRepository(DataContext dataContext) : base(dataContext)
        {
        }

        /// <summary>
        /// Retorna uma lista com as notificações enviadas para os entregados de um pedido
        /// </summary>
        /// <param name="idorder"></param>
        /// <returns></returns>
        public async Task<List<NotifyOrder>> GetAllWithRelationshipsToOrderAsync(int idorder)
        {
            return await _entities.Include(i => i.DeliveryMan).Include(i => i.Order).Where(w=>w.IdOrder == idorder).ToListAsync();
        }

        /// <summary>
        /// Verifica se existe notificação para o entregador para um determinado pedido
        /// </summary>
        /// <param name="iddeliveryMan"></param>
        /// <param name="idOrder"></param>
        /// <returns></returns>
        public async Task<NotifyOrder> GetNotifyAsync(int iddeliveryMan, int idOrder)
        {
            return await _entities
                .Include(i => i.DeliveryMan)
                .Include(i => i.Order)
                    .ThenInclude(i => i.OrderDeliveryMan)
                .Where(w => w.IdDeliveryMan == iddeliveryMan && w .IdOrder == idOrder).FirstOrDefaultAsync();
        }
    }
}
