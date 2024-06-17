using DMReservation.Application.Extensions;
using DMReservation.Domain.Settings;
using DMReservation.Infra.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
GeneralSetting.ConnectionString = builder.Configuration.GetConnectionString("Default");

builder.Services.AddInfraExtensions();
builder.Services.AddApplicationExtensions();

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

app.MapControllers();

app.Run();
