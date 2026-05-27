using TemperatureSensor.Services;
using TemperatureSensor.Services.Abstractions;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddScoped<ITemperatureService, TemperatureService>();
var app = builder.Build();


app.UseMiddleware<ChaosMiddleware>();

app.MapControllers();
app.Run();
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.Run();

