using DMReservation.API.Model;
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
            try
            {
                List<ListMotorcycleDto> motors = await _searchMotorcycle.ExecuteAsync(plate);

                return StatusCode(StatusCodes.Status200OK, motors);

            } catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
        }

        /// <summary>
        /// Cria o cadastro de uma motocicleta
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("create")]
        public async Task<IActionResult> Post(CreateMotorcycle model)
        {
            try
            {
                if (!ModelState.IsValid) {
                    return StatusCode(StatusCodes.Status400BadRequest, ModelState);
                }

                CreateMotorcycleDto motor = new CreateMotorcycleDto
                {
                    LicensePlate = model.LicensePlate,
                    Model = model.Model,
                    Year = model.Year
                };

                await _createMotorcycle.ExecuteAsync(motor);

                return StatusCode(StatusCodes.Status201Created);

            } catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
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
            try
            {
                if (!ModelState.IsValid) {
                    return StatusCode(StatusCodes.Status400BadRequest, ModelState);
                }

                UpdateMotorcycleDto motor = new UpdateMotorcycleDto
                {
                    LicensePlate = model.LicensePlate,
                    Id = id
                };

                await _updateMotorcycle.ExecuteAsync(motor);

                return StatusCode(StatusCodes.Status204NoContent);

            } catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
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
            try
            {
                await _removeMotorcycle.ExecuteAsync(id);

                return StatusCode(StatusCodes.Status204NoContent);

            } catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
        }



    }
}
