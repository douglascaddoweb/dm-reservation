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
    public class MotorcycleUpdateTest
    {
        private readonly IMapper _mapper;

        public MotorcycleUpdateTest()
        {
            _mapper = InfraExtension.Mapper();
        }

        /// <summary>
        /// Dada a entrada valida de dados para a atualização de uma motocicleta o
        /// metodo deverá atualizar e retornar os dados da motocicleta.
        /// </summary>
        [Fact]
        public void ValidMotorcycle_ExecuteUpdate_ReturnMotorcycle()
        {
            //Arrange
            UpdateMotorcycleDto model = new UpdateMotorcycleDto(1, "ASD-1234");

            Mock<IMotorcycleRepository> motorcycleRepositoryMock = new Mock<IMotorcycleRepository>();
            Mock<IMotorcycleService> motorcycleServiceMock = new Mock<IMotorcycleService>();

            Motorcycle motorcycle = new Motorcycle(2020, "cg titan ks", "asd-6325");

            motorcycleRepositoryMock.Setup(mr => mr.FindIdAsync(It.IsAny<int>())).ReturnsAsync(motorcycle);

            UpdateMotorcycle updateMotorcycle = new UpdateMotorcycle(motorcycleRepositoryMock.Object, motorcycleServiceMock.Object, _mapper);

            // Act
            var result = updateMotorcycle.ExecuteAsync(model).Result;

            // Asserts
            result.Year.ShouldBe(motorcycle.Year);
            result.LicensePlate.Value.ShouldBe(model.LicensePlate);
            result.Model.ShouldBe(motorcycle.Model);

        }

        /// <summary>
        /// Dado os dados da motocicleta com a placa inválida deverá retornar exception
        /// </summary>
        [Fact]
        public void InvalidMotorcycle_ExecuteUpdateLicensePlate_ReturnApplicationBaseException()
        {
            //Arrange
            UpdateMotorcycleDto model = new UpdateMotorcycleDto(1, "321456sdf");

            Mock<IMotorcycleRepository> motorcycleRepositoryMock = new Mock<IMotorcycleRepository>();
            Mock<IMotorcycleService> motorcycleServiceMock = new Mock<IMotorcycleService>();

            Motorcycle motorcycle = new Motorcycle(2020, "cg titan ks", "asd-6325");

            motorcycleRepositoryMock.Setup(mr => mr.FindIdAsync(It.IsAny<int>())).ReturnsAsync(motorcycle);

            UpdateMotorcycle updateMotorcycle = new UpdateMotorcycle(motorcycleRepositoryMock.Object, motorcycleServiceMock.Object, _mapper);

            // ACT + Asserts
            Should.ThrowAsync<ApplicationBaseException>(() =>
                updateMotorcycle.ExecuteAsync(model))
                .Result.Message.ShouldBe(MessageSetting.LincensePlateInvalid);

            motorcycleRepositoryMock.Verify(mr => mr.Update(It.IsAny<Motorcycle>()), Times.Never);
        }

        /// <summary>
        /// Dado os dados da motocicleta válida deverá exception caso já tenha uma motocicleta cadastrada 
        /// com a mesma placa informada.
        /// </summary>
        [Fact]
        public void ValidMotorcycle_ExecuteConsultLicensePlate_ReturnApplicationBaseException()
        {
            //Arrange
            UpdateMotorcycleDto model = new UpdateMotorcycleDto(1, "ASD-1234");

            Mock<IMotorcycleRepository> motorcycleRepositoryMock = new Mock<IMotorcycleRepository>();
            Mock<IMotorcycleService> motorcycleServiceMock = new Mock<IMotorcycleService>();

            motorcycleServiceMock.Setup(mr => mr.GetMotorcycleWithPlate(It.IsAny<string>())).ReturnsAsync(true);

            UpdateMotorcycle updateMotorcycle = new UpdateMotorcycle(motorcycleRepositoryMock.Object, motorcycleServiceMock.Object, _mapper);

            // ACT + Asserts
            Should.ThrowAsync<ApplicationBaseException>(() =>
                updateMotorcycle.ExecuteAsync(model))
                .Result.Message.ShouldBe(MessageSetting.MotorcycleRegistered);

            motorcycleRepositoryMock.Verify(mr => mr.Update(It.IsAny<Motorcycle>()), Times.Never);
        }

        /// <summary>
        /// Dado os dados da motocicleta válida deverá exception caso já tenha uma motocicleta cadastrada 
        /// com a mesma placa informada.
        /// </summary>
        [Fact]
        public void ValidMotorcycle_ExecuteConsultExistMotorcycle_ReturnApplicationBaseException()
        {
            //Arrange
            UpdateMotorcycleDto model = new UpdateMotorcycleDto(1, "ASD-1234");

            Mock<IMotorcycleRepository> motorcycleRepositoryMock = new Mock<IMotorcycleRepository>();
            Mock<IMotorcycleService> motorcycleServiceMock = new Mock<IMotorcycleService>();

            motorcycleRepositoryMock.Setup(mr => mr.FindIdAsync(It.IsAny<int>()));

            UpdateMotorcycle updateMotorcycle = new UpdateMotorcycle(motorcycleRepositoryMock.Object, motorcycleServiceMock.Object, _mapper);

            // ACT + Asserts
            Should.ThrowAsync<ApplicationBaseException>(() =>
                updateMotorcycle.ExecuteAsync(model))
                .Result.Message.ShouldBe(MessageSetting.RegistryNotFound);

            motorcycleRepositoryMock.Verify(mr => mr.Update(It.IsAny<Motorcycle>()), Times.Never);
        }
    }
}
