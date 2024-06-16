using DMReservation.Application.Interfaces.Services;
using DMReservation.Application.Interfaces.UseCases.MotorcycleUC;
using DMReservation.Application.Services;
using DMReservation.Application.UseCases.MotorcycleUC;
using Microsoft.Extensions.DependencyInjection;

namespace DMReservation.Application.Extensions
{
    public static class ApplicationExtensions
    {
        public static IServiceCollection AddApplicationExtensions(this IServiceCollection services)
        {
            services.AddTransient<ICreateMotorcycle, CreateMotorcycle>();
            services.AddTransient<ISearchMotorcycle, SearchMotorcycle>();
            services.AddTransient<IUpdateMotorcycle, UpdateMotorcycle>();

            services.AddTransient<IMotorcycleService, MotorcycleService>();

            return services;
        }
    }
}
