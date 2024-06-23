using DMReservation.Application.Interfaces.Services;
using DMReservation.Domain.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace DMReservation.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NotifyOrderController : ControllerBase
    {
        private readonly INotifyOrderService _notificationService;

        public NotifyOrderController(INotifyOrderService notificationService)
        {
            _notificationService = notificationService;
        }

        /// <summary>
        /// Retorna uma lista de entregadores que foram notificados para um pedido
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get(int idorder)
        {
            List<NotifyOrderDto> notifyOrders = await _notificationService.GetAll(idorder);

            return StatusCode(StatusCodes.Status200OK, notifyOrders);
        }
    }
}
