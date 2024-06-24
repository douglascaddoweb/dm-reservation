using AutoFixture;
using DMReservation.Application.UseCases.RentalUC;
using DMReservation.Domain.DTOs;
using DMReservation.Domain.Entities;
using DMReservation.Domain.Exceptions;
using DMReservation.Domain.Interfaces.Infra;
using DMReservation.Domain.Settings;
using Moq;
using Shouldly;

namespace RMReservation.Test.Applications.UseCases.RentalTests
{
    public class RentalSimulateTest
    {

        /// <summary>
        /// Dado os dados de locação válidos deverá simular o valor da locação retornando a simulação.
        /// </summary>
        [Fact]
        public void ValidRental_ExecuteSimulate_ReturnDetailRental()
        {
            //Arrange
            RentalMotorcycleDto model = new RentalMotorcycleDto( 1, DateTime.Now.AddDays(8));

            Mock<IRentalPlanRepository> rentalPlanRepositorioMock = new Mock<IRentalPlanRepository>();

            RentalPlan rental = new RentalPlan(7, 30, 20, 50);

            rentalPlanRepositorioMock.Setup(rp => rp.GetRentalPlanAsync(It.IsAny<int>())).ReturnsAsync(rental);

            SimulateRentalMotorcycle simulate = new SimulateRentalMotorcycle(rentalPlanRepositorioMock.Object);
            //Act
            var result = simulate.ExecuteAsync(model).Result;

            //Assert
            result.datefinish.ShouldBe(model.DateFinish);
            result.Price.ShouldBe(216);
        }

        /// <summary>
        /// Dado os dados de locação válidos deverá simular o valor da locação retornando a simulação.
        /// </summary>
        [Fact]
        public void ValidRental_ExecuteValidateDateFinish_ReturnApplicationBaseException()
        {
            //Arrange
            RentalMotorcycleDto model = new RentalMotorcycleDto( 1, DateTime.Now.AddDays(-8));

            Mock<IRentalPlanRepository> rentalPlanRepositorioMock = new Mock<IRentalPlanRepository>();

            SimulateRentalMotorcycle simulate = new SimulateRentalMotorcycle(rentalPlanRepositorioMock.Object);

            //Act + Assert
            Should.ThrowAsync<ApplicationBaseException>(() =>
                simulate.ExecuteAsync(model)
            ).Result.Message.ShouldBe(MessageSetting.DateFinishInvalid);

        }

        /// <summary>
        /// Dado os dados de locação válidos deverá simular o valor da locação retornando a simulação.
        /// </summary>
        [Fact]
        public void ValidRental_ExecuteRentalPlanExist_ReturnApplicationBaseException()
        {
            //Arrange
            RentalMotorcycleDto model = new RentalMotorcycleDto( 1, DateTime.Now.AddDays(7));

            Mock<IRentalPlanRepository> rentalPlanRepositorioMock = new Mock<IRentalPlanRepository>();

            rentalPlanRepositorioMock.Setup(rm => rm.GetRentalPlanAsync(It.IsAny<int>()));

            SimulateRentalMotorcycle simulate = new SimulateRentalMotorcycle(rentalPlanRepositorioMock.Object);

            //Act + Assert
            Should.ThrowAsync<ApplicationBaseException>(() =>
                simulate.ExecuteAsync(model)
            ).Result.Message.ShouldBe(MessageSetting.ErrorGenereateValueRental);

        }
    }
}
