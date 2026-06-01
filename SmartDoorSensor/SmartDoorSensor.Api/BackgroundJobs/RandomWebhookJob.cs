using Coravel.Invocable;

namespace SmartDoorSensor.BackgroundJobs;

public class RandomWebhookJob : IInvocable
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<RandomWebhookJob> _logger;
    private readonly IConfiguration _configuration;

    public RandomWebhookJob(IHttpClientFactory factory, ILogger<RandomWebhookJob> logger, IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
        _httpClient = factory.CreateClient();
    }

    public async Task Invoke()
    {
        try
        {
            if (Random.Shared.Next(0, 10) < 2)
            {
                var url = _configuration["HubApiUrl"];
                var response = await _httpClient.GetAsync(url);
                _logger.LogInformation($"Webhook sent: {DateTime.Now} | {response.Content} ");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
        }
    }
}
