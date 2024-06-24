using DMReservation.API.Model;
using DMReservation.API.Model.Attibutes;
using DMReservation.Application.Interfaces.UseCases.RentalUC;
using DMReservation.Domain.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace DMReservation.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RentalController : ControllerBase
    {
        private readonly ISimulateRentalMotorcycle _simulateRentalMotorcycle;
        private readonly IRentalMotorcycle _rentalMotorcycle;

        public RentalController(IRentalMotorcycle rentalMotorcycle, ISimulateRentalMotorcycle simulateRentalMotorcycle)
        {
            _rentalMotorcycle = rentalMotorcycle;
            _simulateRentalMotorcycle = simulateRentalMotorcycle;
        }

        /// <summary>
        /// Simula o valor aproximado da locação da motocicleta.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("simulate")]
        public async Task<IActionResult> Simulate(SimulateRentalModel model)
        {
            ModelState.ValidateModel();

            DetailSimulateRentalDto detail = await _simulateRentalMotorcycle.ExecuteAsync(new RentalMotorcycleDto(model.IdDeliveryMan, model.DateFinish));

            return StatusCode(StatusCodes.Status200OK, detail);
        }

        /// <summary>
        /// Realiza a locação de uma motocicleta.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("rental")]
        public async Task<IActionResult> Rental(SimulateRentalModel model)
        {
            ModelState.ValidateModel();

            DetailSimulateRentalDto detail = await _rentalMotorcycle.ExecuteAsync(new RentalMotorcycleDto(model.IdDeliveryMan, model.DateFinish));

            return StatusCode(StatusCodes.Status200OK, detail);
        }
    }
}
