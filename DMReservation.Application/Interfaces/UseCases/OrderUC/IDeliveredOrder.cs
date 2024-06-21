namespace DMReservation.Application.Interfaces.UseCases.OrderUC
{
    public interface IDeliveredOrder
    {

        /// <summary>
        /// Possibilita o entregado informar a entrega do pedido
        /// </summary>
        /// <param name="idorder"></param>
        /// <returns></returns>
        Task ExecuteAsync(int idorder);
    }
}
