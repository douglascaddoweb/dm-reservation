using AutoFixture;
using AutoFixture.Kernel;
using AutoMapper;
using DMReservation.Application.Interfaces.Services;
using DMReservation.Application.UseCases.MotorcycleUC;
using DMReservation.Domain.DTOs;
using DMReservation.Domain.Entities;
using DMReservation.Domain.Exceptions;
using DMReservation.Domain.Interfaces.Infra;
using DMReservation.Domain.Settings;
using DMReservation.Domain.ValueObjects;
using DMReservation.Infra.Extensions;
using Moq;
using Shouldly;

namespace RMReservation.Test.Applications.UseCases.MotorcycleTests
{
    public class MotorcycleRemoveTest
    {
        /// <summary>
        /// Recebido o Id da motocicleta valido para remoção da mesma o
        /// metodo deverá excluir a motocicleta.
        /// </summary>
        [Fact]
        public void ValidMotorcycle_ExecuteRemoveMotorcycle_ReturnNone()
        {
            //Arrange
            
            Mock<IMotorcycleRepository> motorcycleRepositoryMock = new Mock<IMotorcycleRepository>();

            Motorcycle motor = new Motorcycle(2020, "ks cg titan", "asd-1234");
                        
            motorcycleRepositoryMock.Setup(mr => mr.GetMotorcycleWithRentalsAsync(It.IsAny<int>())).ReturnsAsync(motor);

            RemoveMotorcycle removeMotorcycle = new RemoveMotorcycle(motorcycleRepositoryMock.Object);

            // Act
            var result = removeMotorcycle.ExecuteAsync(1);

            // Asserts
            motorcycleRepositoryMock.Verify(mr => mr.Remove(It.IsAny<Motorcycle>()), Times.Once);
            motorcycleRepositoryMock.Verify(mr => mr.CommitAsync(), Times.Once);
        }

        /// <summary>
        /// Dado os dados da motocicleta com a placa inválida deverá retornar exception
        /// </summary>
        [Fact]
        public void InvalidMotorcycle_ExecuteRemoveWithoutMotorcycle_ReturnApplicationBaseException()
        {
            //Arrange

            Mock<IMotorcycleRepository> motorcycleRepositoryMock = new Mock<IMotorcycleRepository>();
                        
            motorcycleRepositoryMock.Setup(mr => mr.GetMotorcycleWithRentalsAsync(It.IsAny<int>()));

            RemoveMotorcycle removeMotorcycle = new RemoveMotorcycle(motorcycleRepositoryMock.Object);

            // Act + Asserts
            Should.ThrowAsync<ApplicationBaseException>(() =>
                removeMotorcycle.ExecuteAsync(1))
                .Result.Message.ShouldBe(MessageSetting.RegistryNotFound);
            
            motorcycleRepositoryMock.Verify(mr => mr.Remove(It.IsAny<Motorcycle>()), Times.Never);
        }

        /// <summary>
        /// Dado os dados da motocicleta válida deverá exception caso já tenha uma motocicleta cadastrada 
        /// com a mesma placa informada.
        /// </summary>
        [Fact]
        public void ValidMotorcycle_ExecuteConsultLicensePlate_ReturnApplicationBaseException()
        {
            //Arrange

            Mock<IMotorcycleRepository> motorcycleRepositoryMock = new Mock<IMotorcycleRepository>();

            Motorcycle motor = new Motorcycle(2020, "ks cg titan", "asd-1234");
            RentalPlan rentalPlan = new Fixture().Create<RentalPlan>();
            motor.AddRentals(new Rental(rentalPlan, DateTime.Now.AddDays(-7), DateTime.Now));

            motorcycleRepositoryMock.Setup(mr => mr.GetMotorcycleWithRentalsAsync(It.IsAny<int>())).ReturnsAsync(motor);

            RemoveMotorcycle removeMotorcycle = new RemoveMotorcycle(motorcycleRepositoryMock.Object);

            // Act + Asserts
            Should.ThrowAsync<ApplicationBaseException>(() =>
                removeMotorcycle.ExecuteAsync(1))
                .Result.Message.ShouldBe(MessageSetting.RemoveMotorcycle);

            motorcycleRepositoryMock.Verify(mr => mr.Remove(It.IsAny<Motorcycle>()), Times.Never);
        }
    }
}
