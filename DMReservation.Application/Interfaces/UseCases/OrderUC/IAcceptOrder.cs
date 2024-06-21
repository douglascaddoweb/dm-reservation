namespace DMReservation.Application.Interfaces.UseCases.OrderUC
{
    public interface IAcceptOrder
    {

        /// <summary>
        /// Possibilita o entregador realizar uma entrega
        /// </summary>
        /// <param name="idOrder"></param>
        /// <param name="idDeliveryMan"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        Task ExecuteAsync(int idOrder, int idDeliveryMan);
    }
}
