using DMReservation.Application.Interfaces.UseCases.OrderUC;
using DMReservation.Domain.Entities;
using DMReservation.Domain.Exceptions;
using DMReservation.Domain.Interfaces.Infra;
using DMReservation.Domain.Settings;

namespace DMReservation.Application.UseCases.OrderUC
{
    public class DeliveredOrder : IDeliveredOrder
    {
        private readonly IOrderRepository _orderRepository;

        public DeliveredOrder(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        /// <summary>
        /// Possibilita o entregado informar a entrega do pedido
        /// </summary>
        /// <param name="idorder"></param>
        /// <returns></returns>
        public async Task ExecuteAsync(int idorder)
        {
            try
            {
                Order order = await _orderRepository.FindIdAsync(idorder);

                if (order is not Order)
                    throw new ApplicationBaseException(MessageSetting.RegistryNotFound, "DEOR");

                order.Delivered();
                _orderRepository.Update(order);

                await _orderRepository.CommitAsync();
            }
            catch (ApplicationBaseException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ApplicationBaseException(ex.Message, MessageSetting.ProcessError, "GNDEOR");
            }
        }
    }
}
