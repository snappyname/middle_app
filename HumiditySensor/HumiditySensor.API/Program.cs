using Application.Repositories;
using Application.Repositories.Abstract;
using Application.Services;
using Application.Services.Abstractions;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddScoped<IHumidityService, HumidityService>();
builder.Services.AddScoped<ISensorRepository, SensorRepository>();
var app = builder.Build();

app.MapControllers();
app.Run();
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.Run();
