using HumiditySensor.Services;
using HumiditySensor.Services.Abstractions;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddScoped<IHumidityService, HumidityService>();
var app = builder.Build();

app.MapControllers();
app.Run();
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.Run();
