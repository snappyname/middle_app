using Application.SignalR;
using KafkaFlow;
using MainApp.Controllers;
using MainApp.Graphql;
using MainApp.Helpers;
using MainApp.SignalR;
using Mapster;
using Microsoft.AspNetCore.HttpOverrides;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();

builder.Services.AddHttpClient();
builder.Services.AddRefitServices(builder.Configuration);
builder.Services.AddKafkaHandlers(builder.Configuration);
builder.Services.AddHttpContextAccessor();

builder.Services.AddScopedServices();
builder.Services.AddSingletonServices();
builder.Services.AddMemoryCache();

builder.Services.AddSignalR();
builder.Services.AddSingleton<IRealtimeNotifier, SignalRNotifier>();

builder.Services.AddDatabase(builder.Configuration);
builder.Services.AddIdentityServices();

builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddAuthorization();

builder.Services.AddFrontendCors(builder.Configuration);
builder.Services.AddForwardedHeadersSupport();

builder.Services
    .AddGraphQLServer()
    .AddAuthorization()
    .AddQueryType<Query>();

TypeAdapterConfig.GlobalSettings.Scan(AppDomain.CurrentDomain.GetAssemblies());

WebApplication app = builder.Build();
app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedProto | ForwardedHeaders.XForwardedFor
});

app.UseForwardedHeaders();

app.UseCors("Frontend");
app.MapGraphQL();
app.ApplyMigrations();
var bus = app.Services.CreateKafkaBus();
await bus.StartAsync();
app.MapHub<SensorHub>("/hub/sensors");
app.ConfigurePipeline();
app.Run();
