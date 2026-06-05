using Application.ApiClients.Abstractions;
using Application.Services;
using Application.Services.Abstract;
using iot_hub.BackgroundJobs;
using Refit;

namespace iot_hub.Extensions;

public static class ServiceCollectionExtensions
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddDIServices()
        {
            services.AddTransient<GetDoorDataJob>();
            services.AddTransient<GetTemperatureDataJob>();
            services.AddTransient<GetHumidityDataJob>();
            services.AddTransient<IKafkaService, KafkaService>();
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
