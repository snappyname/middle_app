using Coravel;
using iot_hub.Extensions;
using Mapster;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDIServices();
builder.Services.AddRefitServices();
TypeAdapterConfig.GlobalSettings.Scan(typeof(Program).Assembly);
builder.Services.AddScheduler();
builder.Services.AddHttpClient();

var app = builder.Build();
app.UseApplicationSchedulers();
app.Run();
