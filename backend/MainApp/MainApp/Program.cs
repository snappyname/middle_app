using MainApp.Helpers;
using Microsoft.AspNetCore.HttpOverrides;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();

builder.Services.AddHttpClient();
builder.Services.AddRefitServices(builder.Configuration);
builder.Services.AddHttpContextAccessor();

builder.Services.AddScopedServices();
builder.Services.AddSingletonServices();

builder.Services.AddDatabase(builder.Configuration);
builder.Services.AddIdentityServices();

builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddAuthorization();

builder.Services.AddFrontendCors(builder.Configuration);
builder.Services.AddForwardedHeadersSupport();

WebApplication app = builder.Build();
app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedProto | ForwardedHeaders.XForwardedFor
});

app.UseForwardedHeaders();

app.UseCors("Frontend");

app.ApplyMigrations();

app.ConfigurePipeline();
app.Run();
