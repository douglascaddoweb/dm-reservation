using DMReservation.API.Model;
using DMReservation.Application.Interfaces.UseCases.OrderUC;
using DMReservation.Domain.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace DMReservation.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly ICreateOrder _createOrder;
        private readonly IAcceptOrder _acceptOrder;
        private readonly IDeliveredOrder _deliveredOrder;

        public OrderController(ICreateOrder createOrder, 
            IAcceptOrder acceptOrder,
            IDeliveredOrder deliveredOrder)
        {
            _createOrder = createOrder;
            _acceptOrder = acceptOrder;
            _deliveredOrder = deliveredOrder;

        }

        /// <summary>
        /// Cria um pedido para realizar a entrega
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("create")]
        public async Task<IActionResult> Post(CreateOrderModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, model);
                }

                await _createOrder.ExecuteAsync(new CreateOrderDto(model.Price));

                return StatusCode(StatusCodes.Status201Created);

            } catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
        }

        /// <summary>
        /// Entregar aceita o pedido para realizar a entrega
        /// </summary>
        /// <param name="iddeliveryMan"></param>
        /// <param name="idorder"></param>
        /// <returns></returns>
        [HttpPatch("accept")]
        public async Task<IActionResult> Patch(int iddeliveryMan, int idorder)
        {
            try
            {
                await _acceptOrder.ExecuteAsync(idorder, iddeliveryMan);

                return StatusCode(StatusCodes.Status204NoContent);

            } catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
        }

        /// <summary>
        /// Entregado informa que foi feita a entrega do pedido
        /// </summary>
        /// <param name="idorder"></param>
        /// <returns></returns>
        [HttpPatch("delivered")]
        public async Task<IActionResult> DeliveredOrder(int idorder)
        {
            try
            {
                await _deliveredOrder.ExecuteAsync(idorder);

                return StatusCode(StatusCodes.Status204NoContent);

            } catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
        }

    }
}
