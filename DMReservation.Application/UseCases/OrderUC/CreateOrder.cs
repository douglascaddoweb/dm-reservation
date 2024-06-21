using DMReservation.Application.Interfaces.UseCases.OrderUC;
using DMReservation.Domain.DTOs;
using DMReservation.Domain.Entities;
using DMReservation.Domain.Enums;
using DMReservation.Domain.Interfaces.Infra;

namespace DMReservation.Application.UseCases.OrderUC
{
    public class CreateOrder : ICreateOrder
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IDeliveryManRepository _deliveryManRepository;
        private readonly IMessageRabbit _messageRabbit;

        public CreateOrder(IDeliveryManRepository deliveryManRepository, 
            IOrderRepository orderRepository,
            IMessageRabbit messageRabbit)
        {
            _deliveryManRepository = deliveryManRepository;
            _orderRepository = orderRepository;            
            _messageRabbit = messageRabbit;
        }

        /// <summary>
        /// Cria um pedido para realizar a entrega
        /// </summary>
        /// <param name="orderDto"></param>
        /// <returns></returns>
        public async Task ExecuteAsync(CreateOrderDto orderDto)
        {
            try
            {
                Order order = new Order(DateTime.Now, orderDto.price, StatusOrder.Available);

                await _orderRepository.AddAsync(order);
                await _orderRepository.CommitAsync();

                List<DeliveryMan> deliveryMen = await _deliveryManRepository.GetDeliveryManAvailableAsync();

                foreach(DeliveryMan deliveryMan in deliveryMen)
                {
                    await _messageRabbit.ExecuteAsync(order.Id, deliveryMan.Id);
                }

            } catch (Exception ex)
            {
                throw;
            }
        }

    }
}
