using DMReservation.API.Middleware;
using DMReservation.Application.Extensions;
using DMReservation.Domain.Settings;
using DMReservation.Infra.Extensions;
using log4net.Config;
using log4net;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
GeneralSetting.ConnectionString = builder.Configuration.GetConnectionString("Default");
GeneralSetting.HostRabbit = builder.Configuration.GetSection("Rabbit:Host").Value;
GeneralSetting.UserName = builder.Configuration.GetSection("Rabbit:UserName").Value;
GeneralSetting.Password = builder.Configuration.GetSection("Rabbit:Password").Value;
GeneralSetting.Port = Convert.ToInt32(builder.Configuration.GetSection("Rabbit:Port").Value);

builder.Services.AddInfraExtensions();
builder.Services.AddApplicationExtensions();

var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));

builder.Services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseErroHandler();

app.UseCors(
    x =>
    {
        x.AllowAnyHeader();
        x.AllowAnyMethod();
        x.AllowAnyOrigin();
    });

app.MapControllers();

app.Run();
