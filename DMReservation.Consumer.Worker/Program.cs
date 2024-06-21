using DMReservation.Consumer.Worker;
using DMReservation.Domain.Interfaces.Infra;
using DMReservation.Domain.Settings;
using DMReservation.Infra.Context;
using DMReservation.Infra.Repositories;

var builder = Host.CreateApplicationBuilder(args);

GeneralSetting.ConnectionString = builder.Configuration.GetConnectionString("Default");
GeneralSetting.HostRabbit = builder.Configuration.GetSection("Rabbit:Host").Value;
GeneralSetting.UserName = builder.Configuration.GetSection("Rabbit:UserName").Value;
GeneralSetting.Password = builder.Configuration.GetSection("Rabbit:Password").Value;
GeneralSetting.Port = Convert.ToInt32(builder.Configuration.GetSection("Rabbit:Port").Value);
GeneralSetting.ChannelConsumer = builder.Configuration.GetSection("Rabbit:Channel").Value;

builder.Services.AddDbContext<DataContext>(ServiceLifetime.Transient);
builder.Services.AddTransient<INotifyOrderRepository, NotifyOrderRepository>();
builder.Services.AddTransient<IDeliveryManRepository, DeliveryManRepository>();
builder.Services.AddTransient<IOrderRepository, OrderRepository>();

builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();
