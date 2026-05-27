using SmartDoorSensor.BackgroundJobs;
using SmartDoorSensor.DTO;
using SmartDoorSensor.Repository;
using SmartDoorSensor.Repository.Abstract;
using SmartDoorSensor.Services;
using SmartDoorSensor.Services.Abstractions;
using SmartDoorSensor.Validators;

namespace SmartDoorSensor.Extensions;

public static class ServiceCollectionExtensions
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddDIServices()
        {
            services.AddTransient<RandomWebhookJob>();
            services.AddScoped<IDoorService, DoorService>();
            services.AddSingleton<IDoorRepository, DoorRepository>();
            return services;
        }

        public IServiceCollection AddValidators()
        {
            services.AddControllers(options =>
            {
                options.Filters.Add<ValidationFilter<SetDoorStatusDTO>>();
            });
            return services;
        }
    }
}
