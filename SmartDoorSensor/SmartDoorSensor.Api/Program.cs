using Coravel;
using FluentValidation;
using SmartDoorSensor.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);
builder.Services.AddHttpClient();
builder.Services.AddScheduler();

builder.Services.AddDIServices();
builder.Services.AddValidators();

builder.Services.AddControllers();

var app = builder.Build();

app.UseApplicationSchedulers();

app.MapControllers();
app.UseHttpsRedirection();

app.Run();
