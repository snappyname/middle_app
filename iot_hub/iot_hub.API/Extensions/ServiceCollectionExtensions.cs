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

        public IServiceCollection AddRefitServices()
        {
            services
                .AddRefitClient<IDoorSensorsClient>()
                .ConfigureHttpClient(c => c.BaseAddress = new Uri("http://localhost:8080/api"));

            services
                .AddRefitClient<ITemperatureSensorClient>()
                .ConfigureHttpClient(c => c.BaseAddress = new Uri("http://localhost:9080/api"));

            services
                .AddRefitClient<IHumiditySensorClient>()
                .ConfigureHttpClient(c => c.BaseAddress = new Uri("http://localhost:7080/api"));
            return services;
        }
    }
}
