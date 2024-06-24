using DMReservation.API.Model;
using DMReservation.API.Model.Attibutes;
using DMReservation.Application.Interfaces.UseCases.MotorcycleUC;
using DMReservation.Domain.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace DMReservation.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MotorcycleController : ControllerBase
    {

        private readonly ISearchMotorcycle _searchMotorcycle;
        private readonly ICreateMotorcycle _createMotorcycle;
        private readonly IUpdateMotorcycle _updateMotorcycle;
        private readonly IRemoveMotorcycle _removeMotorcycle;

        public MotorcycleController(ICreateMotorcycle createMotorcycle, 
            ISearchMotorcycle searchMotorcycle,
            IUpdateMotorcycle updateMotorcycle,
            IRemoveMotorcycle removeMotorcycle)
        {
            _createMotorcycle = createMotorcycle;
            _searchMotorcycle = searchMotorcycle;
            _updateMotorcycle = updateMotorcycle;
            _removeMotorcycle = removeMotorcycle;
        }

        /// <summary>
        /// Retorna lista de motocicletas cadastradas podendo filtrar pela placa da motocicleta
        /// </summary>
        /// <param name="plate"></param>
        /// <returns></returns>
        [HttpGet("search")]
        public async Task<IActionResult> GetMotorcycle(string? plate)
        {
            List<ListMotorcycleDto> motors = await _searchMotorcycle.ExecuteAsync(plate);

            return StatusCode(StatusCodes.Status200OK, motors);
        }

        /// <summary>
        /// Cria o cadastro de uma motocicleta
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("create")]
        public async Task<IActionResult> Post(CreateMotorcycle model)
        {
            ModelState.ValidateModel();

            CreateMotorcycleDto motor = new CreateMotorcycleDto
            {
                LicensePlate = model.LicensePlate,
                Model = model.Model,
                Year = model.Year
            };

            MotorcycleDto motorcycle = await _createMotorcycle.ExecuteAsync(motor);

            return StatusCode(StatusCodes.Status201Created, motorcycle);            
        }

        /// <summary>
        /// Atualiza o cadastro da motocicleta apenas a Placa
        /// </summary>
        /// <param name="model"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPatch("update")]
        public async Task<IActionResult> Patch(UpdateMotorcycleModel model, int id)
        {
            ModelState.ValidateModel();

            UpdateMotorcycleDto motor = new UpdateMotorcycleDto(id, model.LicensePlate);

            MotorcycleDto motorcycle = await _updateMotorcycle.ExecuteAsync(motor);
            return StatusCode(StatusCodes.Status204NoContent, motorcycle);
        }

        
        /// <summary>
        /// Exclui uma motocicleta
        /// </summary>
        /// <param name="model"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(int id)
        {
            await _removeMotorcycle.ExecuteAsync(id);

            return StatusCode(StatusCodes.Status204NoContent);

        }
    }
}
