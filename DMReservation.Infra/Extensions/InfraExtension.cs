using AutoMapper;
using DMReservation.Domain.DTOs;
using DMReservation.Domain.Entities;
using DMReservation.Domain.Interfaces.Infra;
using DMReservation.Infra.Context;
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
            });

            IMapper mapper = config.CreateMapper();

            return mapper;
        }


        public static IServiceCollection AddInfraExtensions(this IServiceCollection services)
        {
            services.AddDbContext<DataContext>(ServiceLifetime.Transient);

            services.AddTransient<IMotorcycleRepository, MotorcycleRepository>();

            services.AddSingleton(Mapper());

            return services;
        }
    }
}
