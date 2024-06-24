using AutoMapper;
using DMReservation.Application.UseCases.DeliveryManUC;
using DMReservation.Domain.DTOs;
using DMReservation.Domain.Entities;
using DMReservation.Domain.Exceptions;
using DMReservation.Domain.Interfaces.Infra;
using DMReservation.Domain.Settings;
using DMReservation.Domain.ValueObjects;
using DMReservation.Infra.Extensions;
using Moq;
using Shouldly;

namespace RMReservation.Test.Applications.UseCases.DeliveryManTest
{
    public class DeliveryManCreateTest
    {
        private readonly IMapper _mapper;

        public DeliveryManCreateTest()
        {
            _mapper = InfraExtension.Mapper();
        }

        /// <summary>
        /// Dado os dados do entregador valido o metodo deverá cadastar e retornar os dados criados.
        /// </summary>
        [Fact]
        public void ValidDeliveryMan_ExecuteCreate_ReturnDeliveryManDto()
        {
            //Arrange
            CreateDeliveryManDto deliveryMan = new CreateDeliveryManDto("Jhon Doe", 
                "09.164.776/0001-11",
                DateTime.Today.AddYears(-25), 
                "11307947147",
                "A");
            
            Mock<IDeliveryManRepository> deliveryManRepositoryMock = new Mock<IDeliveryManRepository>();

            CreateDeliveryMan createDeliveryMan = new CreateDeliveryMan(deliveryManRepositoryMock.Object, _mapper);

            //Act
            var result = createDeliveryMan.ExecuteAsync(deliveryMan).Result;

            //Assert
            result.Cnh.Value.ShouldBe(deliveryMan.cnh);
            result.Cnpj.Value.ShouldBe(deliveryMan.cnpj);
            result.BirthDate.ShouldBe(deliveryMan.birthdate);
            result.Name.ShouldBe(deliveryMan.name);
        }

        /// <summary>
        /// Dado os dados do entregador valido o metodo deverá Validar o tipo de CNH e retornar erro .
        /// </summary>
        [Fact]
        public void ValidDeliveryMan_ExecuteValidateTypeCnh_ReturnApplicationBaseException()
        {
            //Arrange
            CreateDeliveryManDto deliveryMan = new CreateDeliveryManDto("Jhon Doe", 
                "09.164.776/0001-11",
                DateTime.Today.AddYears(-25), 
                "11307947147",
                "C");
            
            Mock<IDeliveryManRepository> deliveryManRepositoryMock = new Mock<IDeliveryManRepository>();

            CreateDeliveryMan createDeliveryMan = new CreateDeliveryMan(deliveryManRepositoryMock.Object, _mapper);

            //Act + Assert
            Should.ThrowAsync<ApplicationBaseException>(() =>
                createDeliveryMan.ExecuteAsync(deliveryMan))
                .Result.Message.ShouldBe(MessageSetting.TypeCnhInvalid);

        }

        /// <summary>
        /// Dado os dados do entregador valido o metodo deverá Validar o CNPJ valido e retornar erro .
        /// </summary>
        [Fact]
        public void ValidDeliveryMan_ExecuteValidateCnpj_ReturnApplicationBaseException()
        {
            //Arrange
            CreateDeliveryManDto deliveryMan = new CreateDeliveryManDto("Jhon Doe", 
                "05.164.776/0001-11",
                DateTime.Today.AddYears(-25), 
                "11307947147",
                "A");
            
            Mock<IDeliveryManRepository> deliveryManRepositoryMock = new Mock<IDeliveryManRepository>();

            CreateDeliveryMan createDeliveryMan = new CreateDeliveryMan(deliveryManRepositoryMock.Object, _mapper);

            //Act + Assert
            Should.ThrowAsync<ApplicationBaseException>(() =>
                createDeliveryMan.ExecuteAsync(deliveryMan))
                .Result.Message.ShouldBe(MessageSetting.CnpjInvalid);
        }
        
        /// <summary>
        /// Dado os dados do entregador valido o metodo deverá Validar se o CNPJ já está cadastrado.
        /// </summary>
        [Fact]
        public void ValidDeliveryMan_ExecuteValidateCnpjExist_ReturnApplicationBaseException()
        {
            //Arrange
            CreateDeliveryManDto deliveryMan = new CreateDeliveryManDto("Jhon Doe", 
                "09.164.776/0001-11",
                DateTime.Today.AddYears(-25), 
                "11307947147",
                "A");
            
            Mock<IDeliveryManRepository> deliveryManRepositoryMock = new Mock<IDeliveryManRepository>();

            DeliveryMan delivery = new DeliveryMan("Jhon Doe",
                new Cnpj("09.164.776/0001-11"),
                DateTime.Today.AddYears(-25),
                new Cnh("11307947147"),
                new TypeCnh("A"));

            deliveryManRepositoryMock
                .Setup(dr => dr.GetDeliveryManWithCnpjAsync(It.IsAny<Cnpj>()))
                .ReturnsAsync(delivery);

            CreateDeliveryMan createDeliveryMan = new CreateDeliveryMan(deliveryManRepositoryMock.Object, _mapper);

            //Act + Assert
            Should.ThrowAsync<ApplicationBaseException>(() =>
                createDeliveryMan.ExecuteAsync(deliveryMan))
                .Result.Message.ShouldBe(MessageSetting.CnpjExist);
        }
        
        /// <summary>
        /// Dado os dados do entregador valido o metodo deverá Validar se A CNH é valida.
        /// </summary>
        [Fact]
        public void ValidDeliveryMan_ExecuteValidateCnh_ReturnApplicationBaseException()
        {
            //Arrange
            CreateDeliveryManDto deliveryMan = new CreateDeliveryManDto("Jhon Doe", 
                "09.164.776/0001-11",
                DateTime.Today.AddYears(-25), 
                "11307947157",
                "A");
            
            Mock<IDeliveryManRepository> deliveryManRepositoryMock = new Mock<IDeliveryManRepository>();

            CreateDeliveryMan createDeliveryMan = new CreateDeliveryMan(deliveryManRepositoryMock.Object, _mapper);

            //Act + Assert
            Should.ThrowAsync<ApplicationBaseException>(() =>
                createDeliveryMan.ExecuteAsync(deliveryMan))
                .Result.Message.ShouldBe(MessageSetting.CnhInvalid);
        }

        /// <summary>
        /// Dado os dados do entregador valido o metodo deverá Validar se a CNH já está cadastrada.
        /// </summary>
        [Fact]
        public void ValidDeliveryMan_ExecuteValidateCnhExist_ReturnApplicationBaseException()
        {
            //Arrange
            CreateDeliveryManDto deliveryMan = new CreateDeliveryManDto("Jhon Doe",
                "09.164.776/0001-11",
                DateTime.Today.AddYears(-25),
                "11307947147",
                "A");

            Mock<IDeliveryManRepository> deliveryManRepositoryMock = new Mock<IDeliveryManRepository>();

            DeliveryMan delivery = new DeliveryMan("Jhon Doe 1",
                new Cnpj("09.772.602/0001-31"),
                DateTime.Today.AddYears(-25),
                new Cnh("11307947147"),
                new TypeCnh("A"));

            deliveryManRepositoryMock
                .Setup(dr => dr.GetDeliveryManWithCnhAsync(It.IsAny<Cnh>()))
                .ReturnsAsync(delivery);

            CreateDeliveryMan createDeliveryMan = new CreateDeliveryMan(deliveryManRepositoryMock.Object, _mapper);

            //Act + Assert
            Should.ThrowAsync<ApplicationBaseException>(() =>
                createDeliveryMan.ExecuteAsync(deliveryMan))
                .Result.Message.ShouldBe(MessageSetting.CnhExist);
        }

    }
}
