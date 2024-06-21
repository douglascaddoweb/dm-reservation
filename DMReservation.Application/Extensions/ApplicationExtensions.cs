using DMReservation.Application.Interfaces.Services;
using DMReservation.Application.Interfaces.UseCases.DeliveryManUC;
using DMReservation.Application.Interfaces.UseCases.MotorcycleUC;
using DMReservation.Application.Interfaces.UseCases.OrderUC;
using DMReservation.Application.Interfaces.UseCases.RentalUC;
using DMReservation.Application.Services;
using DMReservation.Application.UseCases.DeliveryManUC;
using DMReservation.Application.UseCases.MotorcycleUC;
using DMReservation.Application.UseCases.OrderUC;
using DMReservation.Application.UseCases.RentalUC;
using Microsoft.Extensions.DependencyInjection;

namespace DMReservation.Application.Extensions
{
    public static class ApplicationExtensions
    {
        public static IServiceCollection AddApplicationExtensions(this IServiceCollection services)
        {
            services.AddTransient<IAcceptOrder, AcceptOrder>();
            services.AddTransient<ICreateDeliveryMan, CreateDeliveryMan>();
            services.AddTransient<ICreateMotorcycle, CreateMotorcycle>();
            services.AddTransient<ICreateOrder, CreateOrder>();
            services.AddTransient<IDeliveredOrder, DeliveredOrder>();
            services.AddTransient<IRentalMotorcycle, RentalMotorcycle>();
            services.AddTransient<IRemoveMotorcycle, RemoveMotorcycle>();
            services.AddTransient<ISearchMotorcycle, SearchMotorcycle>();
            services.AddTransient<ISimulateRentalMotorcycle, SimulateRentalMotorcycle>();
            services.AddTransient<IUpdateMotorcycle, UpdateMotorcycle>();
            services.AddTransient<IUploadImages, UploadImages>();

            services.AddTransient<IMotorcycleService, MotorcycleService>();
            services.AddTransient<INotifyOrderService, NotifyOrderService>();

            return services;
        }
    }
}
