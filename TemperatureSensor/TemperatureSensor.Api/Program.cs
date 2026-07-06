using Application.Repositories;
using Application.Repositories.Abstractions;
using Application.Services;
using Application.Services.Abstractions;
using TemperatureSensor.Middleware;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddScoped<ITemperatureService, TemperatureService>();
builder.Services.AddScoped<ISensorRepository, SensorRepository>();

var app = builder.Build();
app.UseMiddleware<ChaosMiddleware>();
app.MapControllers();
app.Run();
app.UseHttpsRedirection();

app.Run();
