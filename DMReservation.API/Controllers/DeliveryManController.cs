using DMReservation.API.Model;
using DMReservation.API.Model.Attibutes;
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
        private readonly IUploadImages _uploadImages;

        public DeliveryManController(ICreateDeliveryMan createDeliveryMan, IUploadImages uploadImages)
        {
            _createDeliveryMan = createDeliveryMan;
            _uploadImages = uploadImages;

        }

        /// <summary>
        /// Cria um cadastro de entregador
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("create")]
        public async Task<IActionResult> Post(CreateDeliveryManModel model)
        {
            ModelState.ValidateModel();

            CreateDeliveryManDto deliveryMan = new CreateDeliveryManDto(model.Name, model.Cnpj, model.BirthDate, model.Cnh, model.TypeCnh);
                
            await _createDeliveryMan.ExecuteAsync(deliveryMan);

            return StatusCode(StatusCodes.Status201Created);
        }

        /// <summary>
        /// Envia imagem da CNH do entregador
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("upload")]
        public async Task<IActionResult> Upload(UploadCnhModel model)
        {
            ModelState.ValidateModel();

            await _uploadImages.ExecuteAsync(model.File, model.Id);

            return StatusCode(StatusCodes.Status201Created);
        }
    }
}
