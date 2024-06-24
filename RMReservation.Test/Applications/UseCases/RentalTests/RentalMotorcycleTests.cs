using DMReservation.Application.UseCases.RentalUC;
using DMReservation.Domain.DTOs;
using DMReservation.Domain.Entities;
using DMReservation.Domain.Exceptions;
using DMReservation.Domain.Interfaces.Infra;
using DMReservation.Domain.Settings;
using DMReservation.Domain.ValueObjects;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMReservation.Test.Applications.UseCases.RentalTests
{
    public class RentalMotorcycleTests
    {

        /// <summary>
        /// Dado os dados validos para a confirmação de uma locação o sistema realiza a locação para o entregador
        /// selecionando a motocicleta disponível e o plano com base na data de entrega.
        /// </summary>
        [Fact]
        public void ValidRental_Execute_ReturnsRentalDto()
        {
            //Arrange
            RentalMotorcycleDto rental = new RentalMotorcycleDto(1, DateTime.Now.AddDays(7));

            Mock<IMotorcycleRepository> motorcycleRepositorioMock = new Mock<IMotorcycleRepository>();
            Mock<IRentalPlanRepository> rentalPlanRepositoryMock = new Mock<IRentalPlanRepository>();
            Mock<IDeliveryManRepository> deliveryManRepositoryMock = new Mock<IDeliveryManRepository>();
            Mock<IRentalRepository> rentalRepositoryMock = new Mock<IRentalRepository>();

            motorcycleRepositorioMock
                .Setup(mr => mr.GetMotorcycleAvailableAsync())
                .ReturnsAsync(new Motorcycle(2019, "CG Titan ks", "ASD-1234"));

            deliveryManRepositoryMock
                .Setup(dr => dr.FindIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new DeliveryMan("Jhon Doe",
                    new Cnpj("09.164.776/0001-11"),
                    DateTime.Today.AddYears(-25),
                    new Cnh("11307947147"),
                    new TypeCnh("A")));

            rentalPlanRepositoryMock
                .Setup(rp => rp.GetMaxDaysPlanAsync())
                .ReturnsAsync(new RentalPlan(30, 22, 20, 50));
            
            rentalPlanRepositoryMock
                .Setup(rp => rp.GetRentalPlanAsync(It.IsAny<int>()))
                .ReturnsAsync(new RentalPlan(7, 30, 20, 50));

            RentalMotorcycle rentalMotorcycle = new RentalMotorcycle(deliveryManRepositoryMock.Object,
                rentalPlanRepositoryMock.Object, 
                rentalRepositoryMock.Object,
                motorcycleRepositorioMock.Object);


            //act
            var result = rentalMotorcycle.ExecuteAsync(rental).Result;

            //Assert
            result.datefinish.ShouldBe(rental.DateFinish);
            result.Price.ShouldBe(222);

            rentalRepositoryMock.Verify(rm => rm.AddAsync(It.IsAny<Rental>()), Times.Once());
        }

        /// <summary>
        /// Dado os dados validos para a confirmação de uma locação o sistema verifica se há uma motocicleta disponível 
        /// retornando erro quando não encontra.
        /// </summary>
        [Fact]
        public void ValidRental_ExecuteValidateNMotorcycleExist_ReturnsApplicationBaseException()
        {
            //Arrange
            RentalMotorcycleDto rental = new RentalMotorcycleDto(1, DateTime.Now.AddDays(7));

            Mock<IMotorcycleRepository> motorcycleRepositorioMock = new Mock<IMotorcycleRepository>();
            Mock<IRentalPlanRepository> rentalPlanRepositoryMock = new Mock<IRentalPlanRepository>();
            Mock<IDeliveryManRepository> deliveryManRepositoryMock = new Mock<IDeliveryManRepository>();
            Mock<IRentalRepository> rentalRepositoryMock = new Mock<IRentalRepository>();

            motorcycleRepositorioMock
                .Setup(mr => mr.GetMotorcycleAvailableAsync());

            RentalMotorcycle rentalMotorcycle = new RentalMotorcycle(deliveryManRepositoryMock.Object,
                rentalPlanRepositoryMock.Object, 
                rentalRepositoryMock.Object,
                motorcycleRepositorioMock.Object);

            //act * Assert
            Should.ThrowAsync<ApplicationBaseException>(() =>
                rentalMotorcycle.ExecuteAsync(rental))
                .Result.Message.ShouldBe(MessageSetting.AnythingMotorcyleToRent);

        }

        /// <summary>
        /// Dado os dados validos para a confirmação de uma locação o sistema verifica se o id do entregador
        /// existe na base de dados.
        /// </summary>
        [Fact]
        public void ValidRental_ExecuteValidateDeliveryExist_ReturnsApplicationBaseException()
        {
            //Arrange
            RentalMotorcycleDto rental = new RentalMotorcycleDto(1, DateTime.Now.AddDays(7));

            Mock<IMotorcycleRepository> motorcycleRepositorioMock = new Mock<IMotorcycleRepository>();
            Mock<IRentalPlanRepository> rentalPlanRepositoryMock = new Mock<IRentalPlanRepository>();
            Mock<IDeliveryManRepository> deliveryManRepositoryMock = new Mock<IDeliveryManRepository>();
            Mock<IRentalRepository> rentalRepositoryMock = new Mock<IRentalRepository>();

            motorcycleRepositorioMock
                .Setup(mr => mr.GetMotorcycleAvailableAsync())
                .ReturnsAsync(new Motorcycle(2019, "CG Titan ks", "ASD-1234"));

            deliveryManRepositoryMock
                .Setup(dr => dr.FindIdAsync(It.IsAny<int>()));

            RentalMotorcycle rentalMotorcycle = new RentalMotorcycle(deliveryManRepositoryMock.Object,
                rentalPlanRepositoryMock.Object,
                rentalRepositoryMock.Object,
                motorcycleRepositorioMock.Object);


            //act * Assert
            Should.ThrowAsync<ApplicationBaseException>(() =>
                rentalMotorcycle.ExecuteAsync(rental))
                .Result.Message.ShouldBe(MessageSetting.DeliveryManNotFound);
        }

        /// <summary>
        /// Dado os dados validos para a confirmação de uma locação o sistema verifica se o tipo da 
        /// CNH pode pilotar motocicleta.
        /// </summary>
        [Fact]
        public void ValidRental_ExecuteValidateTypeCnh_ReturnsApplicationBaseException()
        {
            //Arrange
            RentalMotorcycleDto rental = new RentalMotorcycleDto(1, DateTime.Now.AddDays(7));

            Mock<IMotorcycleRepository> motorcycleRepositorioMock = new Mock<IMotorcycleRepository>();
            Mock<IRentalPlanRepository> rentalPlanRepositoryMock = new Mock<IRentalPlanRepository>();
            Mock<IDeliveryManRepository> deliveryManRepositoryMock = new Mock<IDeliveryManRepository>();
            Mock<IRentalRepository> rentalRepositoryMock = new Mock<IRentalRepository>();

            motorcycleRepositorioMock
                .Setup(mr => mr.GetMotorcycleAvailableAsync())
                .ReturnsAsync(new Motorcycle(2019, "CG Titan ks", "ASD-1234"));

            deliveryManRepositoryMock
                .Setup(dr => dr.FindIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new DeliveryMan("Jhon Doe",
                    new Cnpj("09.164.776/0001-11"),
                    DateTime.Today.AddYears(-25),
                    new Cnh("11307947147"),
                    new TypeCnh("C")));

            RentalMotorcycle rentalMotorcycle = new RentalMotorcycle(deliveryManRepositoryMock.Object,
                rentalPlanRepositoryMock.Object,
                rentalRepositoryMock.Object,
                motorcycleRepositorioMock.Object);

            //act * Assert
            Should.ThrowAsync<ApplicationBaseException>(() =>
                rentalMotorcycle.ExecuteAsync(rental))
                .Result.Message.ShouldBe(MessageSetting.DeliveryManNotHabilit);
        }

        /// <summary>
        /// Dado os dados validos para a confirmação de uma locação o sistema verifica se a data de entrega
        /// é maior que o dia atual.
        /// </summary>
        [Fact]
        public void ValidRental_ExecuteValidateDateFinish_ReturnsApplicationBaseException()
        {
            //Arrange
            RentalMotorcycleDto rental = new RentalMotorcycleDto(1, DateTime.Now.AddDays(-7));

            Mock<IMotorcycleRepository> motorcycleRepositorioMock = new Mock<IMotorcycleRepository>();
            Mock<IRentalPlanRepository> rentalPlanRepositoryMock = new Mock<IRentalPlanRepository>();
            Mock<IDeliveryManRepository> deliveryManRepositoryMock = new Mock<IDeliveryManRepository>();
            Mock<IRentalRepository> rentalRepositoryMock = new Mock<IRentalRepository>();

            motorcycleRepositorioMock
                .Setup(mr => mr.GetMotorcycleAvailableAsync())
                .ReturnsAsync(new Motorcycle(2019, "CG Titan ks", "ASD-1234"));

            deliveryManRepositoryMock
                .Setup(dr => dr.FindIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new DeliveryMan("Jhon Doe",
                    new Cnpj("09.164.776/0001-11"),
                    DateTime.Today.AddYears(-25),
                    new Cnh("11307947147"),
                    new TypeCnh("A")));

            rentalPlanRepositoryMock
                .Setup(rp => rp.GetMaxDaysPlanAsync())
                .ReturnsAsync(new RentalPlan(30, 22, 20, 50));

            rentalPlanRepositoryMock
                .Setup(rp => rp.GetRentalPlanAsync(It.IsAny<int>()))
                .ReturnsAsync(new RentalPlan(7, 30, 20, 50));

            RentalMotorcycle rentalMotorcycle = new RentalMotorcycle(deliveryManRepositoryMock.Object,
                rentalPlanRepositoryMock.Object,
                rentalRepositoryMock.Object,
                motorcycleRepositorioMock.Object);

            //act * Assert
            Should.ThrowAsync<ApplicationBaseException>(() =>
                rentalMotorcycle.ExecuteAsync(rental))
                .Result.Message.ShouldBe(MessageSetting.DateFinishInvalid);
        }

        /// <summary>
        /// Dado os dados validos para a confirmação de uma locação o sistema realiza a locação para o entregador
        /// selecionando a motocicleta disponível e o plano com base na data de entrega.
        /// </summary>
        [Fact]
        public void ValidRental_ExecuteRentalPlanExist_ReturnsApplicationBaseException()
        {
            //Arrange
            RentalMotorcycleDto rental = new RentalMotorcycleDto(1, DateTime.Now.AddDays(7));

            Mock<IMotorcycleRepository> motorcycleRepositorioMock = new Mock<IMotorcycleRepository>();
            Mock<IRentalPlanRepository> rentalPlanRepositoryMock = new Mock<IRentalPlanRepository>();
            Mock<IDeliveryManRepository> deliveryManRepositoryMock = new Mock<IDeliveryManRepository>();
            Mock<IRentalRepository> rentalRepositoryMock = new Mock<IRentalRepository>();

            motorcycleRepositorioMock
                .Setup(mr => mr.GetMotorcycleAvailableAsync())
                .ReturnsAsync(new Motorcycle(2019, "CG Titan ks", "ASD-1234"));

            deliveryManRepositoryMock
                .Setup(dr => dr.FindIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new DeliveryMan("Jhon Doe",
                    new Cnpj("09.164.776/0001-11"),
                    DateTime.Today.AddYears(-25),
                    new Cnh("11307947147"),
                    new TypeCnh("A")));

            rentalPlanRepositoryMock
                .Setup(rp => rp.GetMaxDaysPlanAsync());

            rentalPlanRepositoryMock
                .Setup(rp => rp.GetRentalPlanAsync(It.IsAny<int>()));

            RentalMotorcycle rentalMotorcycle = new RentalMotorcycle(deliveryManRepositoryMock.Object,
                rentalPlanRepositoryMock.Object,
                rentalRepositoryMock.Object,
                motorcycleRepositorioMock.Object);

            //act * Assert
            Should.ThrowAsync<ApplicationBaseException>(() =>
                rentalMotorcycle.ExecuteAsync(rental))
                .Result.Message.ShouldBe(MessageSetting.ErrorGenereateValueRental);
        }

    }
}
