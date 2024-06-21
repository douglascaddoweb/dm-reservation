using DMReservation.Domain.DTOs;

namespace DMReservation.Application.Interfaces.UseCases.OrderUC
{
    public interface ICreateOrder
    {
        /// <summary>
        /// Cria um pedido para realizar a entrega
        /// </summary>
        /// <param name="orderDto"></param>
        /// <returns></returns>
        Task ExecuteAsync(CreateOrderDto orderDto);
    }
}
