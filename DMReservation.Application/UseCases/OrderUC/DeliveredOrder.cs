using DMReservation.Application.Interfaces.UseCases.OrderUC;
using DMReservation.Domain.Entities;
using DMReservation.Domain.Interfaces.Infra;

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
            Order order = await _orderRepository.FindIdAsync(idorder);

            order.Delivered();

            _orderRepository.Update(order);
            await _orderRepository.CommitAsync();
        }
    }
}
