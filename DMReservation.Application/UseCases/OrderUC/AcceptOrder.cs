using DMReservation.Application.Interfaces.UseCases.OrderUC;
using DMReservation.Domain.Entities;
using DMReservation.Domain.Enums;
using DMReservation.Domain.Exceptions;
using DMReservation.Domain.Interfaces.Infra;
using DMReservation.Domain.Settings;

namespace DMReservation.Application.UseCases.OrderUC
{
    public class AcceptOrder : IAcceptOrder
    {
        private readonly INotifyOrderRepository _notifyOrderRepository;
        private readonly IOrderRepository _orderRepository;


        public AcceptOrder(INotifyOrderRepository notifyOrderRepository, IOrderRepository orderRepository)
        {
            _notifyOrderRepository = notifyOrderRepository;
            _orderRepository = orderRepository;

        }

        /// <summary>
        /// Possibilita o entregador realizar uma entrega
        /// </summary>
        /// <param name="idOrder"></param>
        /// <param name="idDeliveryMan"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task ExecuteAsync(int idOrder, int idDeliveryMan)
        {
            try
            {
                NotifyOrder notify = await _notifyOrderRepository.GetNotifyAsync(idDeliveryMan, idOrder);

                if (notify is not NotifyOrder)
                    throw new ApplicationBaseException(MessageSetting.PermissionDelivery, "ATOR01");

                Order order = await _orderRepository.FindIdAsync(idOrder);

                if (order.Status != StatusOrder.Available)
                    throw new ApplicationBaseException(MessageSetting.AcceptNotPossible, "ATOR02");

                order.AcceptDelivery();

                _orderRepository.Update(order);

                order.CreateOrderDelivery(notify.DeliveryMan);

                await _orderRepository.CommitAsync();
            }
            catch (ApplicationBaseException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ApplicationBaseException(ex.Message, MessageSetting.ProcessError, "GNATOR");
            }
        }
    }
}
