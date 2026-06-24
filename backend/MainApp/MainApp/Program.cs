using Application.MappingProfiles;
using KafkaFlow;
using MainApp.Helpers;
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

builder.Services.AddDatabase(builder.Configuration);
builder.Services.AddIdentityServices();

builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddAuthorization();

builder.Services.AddFrontendCors(builder.Configuration);
builder.Services.AddForwardedHeadersSupport();

TypeAdapterConfig.GlobalSettings.Scan(AppDomain.CurrentDomain.GetAssemblies());

WebApplication app = builder.Build();
app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedProto | ForwardedHeaders.XForwardedFor
});

app.UseForwardedHeaders();

app.UseCors("Frontend");

app.ApplyMigrations();
var bus = app.Services.CreateKafkaBus();
await bus.StartAsync();
app.ConfigurePipeline();
app.Run();
