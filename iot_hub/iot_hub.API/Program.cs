using Application.ApiClients.Abstractions;
using Application.Services;
using Application.Services.Abstract;
using Coravel;
using iot_hub.BackgroundJobs;
using iot_hub.Extentions;
using Mapster;
using Refit;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddTransient<GetDoorDataJob>();
builder.Services.AddTransient<GetTemperatureDataJob>();
builder.Services.AddTransient<GetHumidityDataJob>();
builder.Services
    .AddRefitClient<ISensorsClient>()
    .ConfigureHttpClient(c =>
        c.BaseAddress = new Uri("http://localhost:8080/api"));

TypeAdapterConfig.GlobalSettings.Scan(typeof(Program).Assembly);

builder.Services.AddSingleton<IKafkaService, KafkaService>();

builder.Services.AddScheduler();
builder.Services.AddHttpClient();

var app = builder.Build();
app.UseApplicationSchedulers();
app.Run();
