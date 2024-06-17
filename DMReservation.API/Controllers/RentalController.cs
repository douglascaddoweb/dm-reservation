﻿using DMReservation.API.Model;
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

        public RentalController(ISimulateRentalMotorcycle simulateRentalMotorcycle)
        {
            _simulateRentalMotorcycle = simulateRentalMotorcycle;
        }

        [HttpPost("simulate")]
        public async Task<IActionResult> Simulate(SimulateRentalModel model)
        {
            try
            {
                if (!ModelState.IsValid) return StatusCode(StatusCodes.Status400BadRequest, ModelState);


                DetailSimulateRentalDto detail =  await _simulateRentalMotorcycle.ExecuteAsync(new RentalMotorcycleDto(model.IdDeliveryMan, model.DateFinish));

                return StatusCode(StatusCodes.Status200OK, detail);

            } catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }

        }
    }
}