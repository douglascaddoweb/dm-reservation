using AutoFixture;
using AutoMapper;
using DMReservation.Application.Interfaces.Services;
using DMReservation.Application.UseCases.MotorcycleUC;
using DMReservation.Domain.DTOs;
using DMReservation.Domain.Entities;
using DMReservation.Domain.Exceptions;
using DMReservation.Domain.Interfaces.Infra;
using DMReservation.Domain.Settings;
using DMReservation.Infra.Extensions;
using Moq;
using Shouldly;

namespace RMReservation.Test.Applications.UseCases.MotorcycleTests
{
    public class MotorcycleCreateTest
    {
        private readonly IMapper _mapper;

        public MotorcycleCreateTest()
        {
            _mapper = InfraExtension.Mapper();
        }

        /// <summary>
        /// Dada a entrada valida de dados para o cadastro de uma motocicleta o
        /// metodo deverá cadastrar e retornar os dados da motocicleta.
        /// </summary>
        [Fact]
        public void ValidMotorcycle_Execute_ReturnMotorcycle()
        {
            //Arrange
            CreateMotorcycleDto model = new Fixture().Create<CreateMotorcycleDto>();
            model.LicensePlate = "ASD-1234";

            Mock<IMotorcycleRepository> motorcycleRepositoryMock = new Mock<IMotorcycleRepository>();
            Mock<IMotorcycleService> motorcycleServiceMock = new Mock<IMotorcycleService>();

            CreateMotorcycle createMotorcycle = new CreateMotorcycle(motorcycleRepositoryMock.Object, motorcycleServiceMock.Object, _mapper);

            // Act
            var result = createMotorcycle.ExecuteAsync(model).Result;

            // Asserts
            result.Year.ShouldBe(model.Year);
            result.LicensePlate.Value.ShouldBe(model.LicensePlate);
            result.Model.ShouldBe(model.Model);
        }

        /// <summary>
        /// Dado os dados da motocicleta com a placa inválida deverá retornar exception
        /// </summary>
        [Fact]
        public void InvalidMotorcycle_Execute_ReturnApplicationBaseException()
        {
            //Arrange
            CreateMotorcycleDto model = new Fixture().Create<CreateMotorcycleDto>();
            model.LicensePlate = "321456sdf";

            Mock<IMotorcycleRepository> motorcycleRepositoryMock = new Mock<IMotorcycleRepository>();
            Mock<IMotorcycleService> motorcycleServiceMock = new Mock<IMotorcycleService>();

            CreateMotorcycle createMotorcycle = new CreateMotorcycle(motorcycleRepositoryMock.Object, motorcycleServiceMock.Object, _mapper);

            // ACT + Asserts
            Should.ThrowAsync<ApplicationBaseException>(() =>
                createMotorcycle.ExecuteAsync(model))
                .Result.Message.ShouldBe(MessageSetting.LincensePlateInvalid);

            motorcycleRepositoryMock.Verify(mr => mr.AddAsync(It.IsAny<Motorcycle>()), Times.Never);
        }

        /// <summary>
        /// Dado os dados da motocicleta válida deverá exception caso já tenha uma motocicleta cadastrada 
        /// com a mesma placa informada.
        /// </summary>
        [Fact]
        public void ValidMotorcycle_ExecuteConsultLicensePlate_ReturnApplicationBaseException()
        {
            //Arrange
            CreateMotorcycleDto model = new Fixture().Create<CreateMotorcycleDto>();
            model.LicensePlate = "ASD-123";

            Mock<IMotorcycleRepository> motorcycleRepositoryMock = new Mock<IMotorcycleRepository>();
            Mock<IMotorcycleService> motorcycleServiceMock = new Mock<IMotorcycleService>();

            motorcycleServiceMock.Setup(ms => ms.GetMotorcycleWithPlate(It.IsAny<string>())).ReturnsAsync(true);

            CreateMotorcycle createMotorcycle = new CreateMotorcycle(motorcycleRepositoryMock.Object, motorcycleServiceMock.Object, _mapper);

            // ACT + Asserts
            Should.ThrowAsync<ApplicationBaseException>(() =>
                createMotorcycle.ExecuteAsync(model))
                .Result.Message.ShouldBe(MessageSetting.MotorcycleRegistered);

            motorcycleRepositoryMock.Verify(mr => mr.AddAsync(It.IsAny<Motorcycle>()), Times.Never);
        }
    }
}
