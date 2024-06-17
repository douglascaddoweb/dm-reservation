using DMReservation.API.Model;
using DMReservation.Application.Interfaces.UseCases.DeliveryManUC;
using DMReservation.Domain.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace DMReservation.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DeliveryManController : ControllerBase
    {
        private readonly ICreateDeliveryMan _createDeliveryMan;

        public DeliveryManController(ICreateDeliveryMan createDeliveryMan)
        {
            _createDeliveryMan = createDeliveryMan;
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateDeliveryManModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, ModelState);
                }

                CreateDeliveryManDto deliveryMan = new CreateDeliveryManDto(model.Name, model.Cnpj, model.BirthDate, model.Cnh, model.TypeCnh);
                
                await _createDeliveryMan.ExecuteAsync(deliveryMan);

                return StatusCode(StatusCodes.Status201Created);

            } catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
        }
    }
}
