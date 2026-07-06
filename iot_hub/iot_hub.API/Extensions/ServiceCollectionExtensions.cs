using Application.ApiClients.Abstractions;
using Application.Services;
using Application.Services.Abstract;
using iot_hub.BackgroundJobs;
using iot_hub.Configurations;
using Refit;

namespace iot_hub.Extensions;

public static class ServiceCollectionExtensions
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddDIServices(IConfiguration configuration)
        {
            services.AddScoped<GetDoorDataJob>();
            services.AddScoped<GetTemperatureDataJob>();
            services.AddScoped<GetHumidityDataJob>();
            services.AddSingleton<IKafkaService, KafkaService>();
            
            services.Configure<SensorsApiSettings>(
                configuration.GetSection("SensorsApi"));
            
            services.Configure<AppSettings>(configuration);
            return services;
        }

        public IServiceCollection AddRefitServices(IConfiguration configuration)
        {
            services
                .AddRefitClient<IDoorSensorsClient>()
                .ConfigureHttpClient(c => c.BaseAddress = new Uri(configuration["SensorsApi:SmartDoor"]!));

            services
                .AddRefitClient<ITemperatureSensorClient>()
                .ConfigureHttpClient(c => c.BaseAddress = new Uri(configuration["SensorsApi:Temperature"]!));

            services
                .AddRefitClient<IHumiditySensorClient>()
                .ConfigureHttpClient(c => c.BaseAddress = new Uri(configuration["SensorsApi:Humidity"]!));
            return services;
        }
    }
}
