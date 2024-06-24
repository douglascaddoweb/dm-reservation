using AutoMapper;
using DMReservation.Domain.DTOs;
using DMReservation.Domain.Entities;
using DMReservation.Domain.Interfaces.Infra;
using DMReservation.Domain.ValueObjects;
using DMReservation.Infra.Context;
using DMReservation.Infra.Messages;
using DMReservation.Infra.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace DMReservation.Infra.Extensions
{
    public static class InfraExtension
    {

        public static IMapper Mapper()
        {
            var config = new AutoMapper.MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ListMotorcycleDto, Motorcycle>().ReverseMap();
                cfg.CreateMap<CreateMotorcycleDto, Motorcycle>().ReverseMap();
                cfg.CreateMap<OrderDto, Order>().ReverseMap();
                cfg.CreateMap<DeliveryManDto, DeliveryMan>().ReverseMap();
                cfg.CreateMap<MotorcycleDto, Motorcycle>().ReverseMap();
                cfg.CreateMap<NotifyOrderDto, NotifyOrder>().ReverseMap();
                
                cfg.CreateMap<CnhDto, Cnh>().ReverseMap();
                cfg.CreateMap<CnpjDto, Cnpj>().ReverseMap();
                cfg.CreateMap<LicensePlateDto, LicensePlate>().ReverseMap();
            });

            IMapper mapper = config.CreateMapper();

            return mapper;
        }


        public static IServiceCollection AddInfraExtensions(this IServiceCollection services)
        {
            services.AddDbContext<DataContext>(ServiceLifetime.Transient);

            services.AddTransient<IDeliveryManRepository, DeliveryManRepository>();
            services.AddTransient<IMotorcycleRepository, MotorcycleRepository>();
            services.AddTransient<INotifyOrderRepository, NotifyOrderRepository>();
            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddTransient<IRentalPlanRepository, RentalPlanRepository>();
            services.AddTransient<IRentalRepository, RentalRepository>();

            services.AddTransient<IMessageRabbit, MessageRabbit>();

            services.AddSingleton(Mapper());

            return services;
        }
    }
}
