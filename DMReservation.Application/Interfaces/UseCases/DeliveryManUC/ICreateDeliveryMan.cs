using DMReservation.Domain.DTOs;

namespace DMReservation.Application.Interfaces.UseCases.DeliveryManUC
{
    public interface ICreateDeliveryMan
    {

        /// <summary>
        /// Cria um cadastro de entregador
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<DeliveryManDto> ExecuteAsync(CreateDeliveryManDto model);
    }
}
