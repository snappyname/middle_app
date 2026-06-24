using Application.KafkaHandlers;
using Application.RefitClients;
using Application.Repositories;
using Application.Repositories.Abstract;
using Application.Services;
using Application.Services.Abstract;
using Application.Services.Abstract.Admin;
using Application.Services.Abstract.Auth;
using Application.Services.Abstract.Sensors;
using Application.Services.Abstract.User;
using Application.Services.Admin;
using Application.Services.Auth;
using Application.Services.Sensors;
using Application.Services.User;
using Contracts.Kafka;
using KafkaFlow;
using KafkaFlow.Serializer;
using Mapster;
using Refit;

namespace MainApp.Helpers;

public static class ServiceContainer
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddScopedServices()
        {
            /* SERVICES */
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICurrentUserService, CurrentUserService>();
            services.AddScoped<IGithubAuthService, GithubAuthService>();
            services.AddScoped<IEmailAuthService, EmailAuthService>();
            services.AddScoped<IAdminService, AdminService>();
            services.AddScoped<ISensorsService, SensorsService>();
            
            /*REPOSITORIES*/
            services.AddScoped<ISensorsRepository, SensorsRepository>();
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            
            /*KAFKA HANDLERS*/
            services.AddScoped<TemperatureHandler>();
            services.AddScoped<HumidityHandler>();
            return services;
        }

        public IServiceCollection AddSingletonServices()
        {
            services.AddSingleton(TypeAdapterConfig.GlobalSettings);
            return services;
        }

        public IServiceCollection AddRefitServices(IConfiguration configuration)
        {
            services.AddRefitClient<IGithubApiClient>()
                .ConfigureHttpClient(x =>
                {
                    x.BaseAddress = new Uri("https://api.github.com");
                    x.DefaultRequestHeaders.UserAgent.ParseAdd("MyApp");
                });
            services
                .AddRefitClient<IGithubAuthClient>()
                .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://github.com"));

            return services;
        }

        public IServiceCollection AddKafkaHandlers(IConfiguration configuration)
        {
            services.AddKafka(kafka => kafka
                .AddCluster(cluster => cluster
                    .WithBrokers(new[] { "localhost:29092" })
                    .AddConsumer(consumer => consumer
                        .Topic("Temperature")
                        .WithGroupId("Temperature")
                        .WithAutoOffsetReset(AutoOffsetReset.Earliest)
                        .WithBufferSize(100)
                        .WithWorkersCount(3)
                        .AddMiddlewares(middlewares => middlewares
                            .AddSingleTypeDeserializer<List<TemperatureValueKafkaDTO>, JsonCoreDeserializer>()
                            .AddTypedHandlers(h => h.AddHandler<TemperatureHandler>())
                        )
                    )
                    .AddConsumer(consumer => consumer
                        .Topic("Humidity")
                        .WithGroupId("Humidity")
                        .WithAutoOffsetReset(AutoOffsetReset.Earliest)
                        .WithBufferSize(100)
                        .WithWorkersCount(3)
                        .AddMiddlewares(middlewares => middlewares
                            .AddSingleTypeDeserializer<List<HumidityValueKafkaDTO>, JsonCoreDeserializer>()
                            .AddTypedHandlers(h => h.AddHandler<HumidityHandler>())
                        )
                    )
                ));
            return services;
        }
    }
}
