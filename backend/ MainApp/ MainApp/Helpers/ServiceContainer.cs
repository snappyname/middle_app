using Application.RefitClients;
using Application.Services;
using Application.Services.Abstract;
using Application.Services.Abstract.Auth;
using Application.Services.Auth;
using Refit;

namespace MainApp.Helpers;

public static class ServiceContainer
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddScopedServices()
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICurrentUserService, CurrentUserService>();
            services.AddScoped<IGithubAuthService, GithubAuthService>();
            services.AddScoped<IEmailAuthService, EmailAuthService>();
            return services;
        }

        public IServiceCollection AddSingletonServices()
        {
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
    }
}
