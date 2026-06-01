using Application.Services;
using Application.Services.Abstractions;
using Application.Validators;
using Application.Validators.DTOs;
using Contracts.DTO;
using FluentValidation;
using Infrastructure.Repository;
using Infrastructure.Repository.Abstract;
using SmartDoorSensor.BackgroundJobs;

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
            services.AddScoped<IValidator<SetDoorStatusDTO>, SetDoorStatusRequestValidator>();
            services.AddControllers(options =>
            {
                options.Filters.Add<ValidationFilter<SetDoorStatusDTO>>();
            });
            return services;
        }
    }
}
