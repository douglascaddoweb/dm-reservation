using DMReservation.Domain.Entities;

namespace DMReservation.Domain.Interfaces.Infra
{
    public interface INotifyOrderRepository : IRepositoryGeneric<NotifyOrder, int>
    {

        /// <summary>
        /// Retorna uma lista com as notificações enviadas para os entregados de um pedido
        /// </summary>
        /// <param name="idorder"></param>
        /// <returns></returns>
        Task<List<NotifyOrder>> GetAllWithRelationshipsToOrderAsync(int idorder);


        /// <summary>
        /// Verifica se existe notificação para o entregador para um determinado pedido
        /// </summary>
        /// <param name="iddeliveryMan"></param>
        /// <param name="idOrder"></param>
        /// <returns></returns>
        Task<NotifyOrder> GetNotifyAsync(int iddeliveryMan, int idOrder);
    }
}
