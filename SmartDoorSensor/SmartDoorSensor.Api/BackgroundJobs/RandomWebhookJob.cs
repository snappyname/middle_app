using Coravel.Invocable;

namespace SmartDoorSensor.BackgroundJobs;

public class RandomWebhookJob : IInvocable
{
    private HttpClient _httpClient;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILogger<RandomWebhookJob> _logger;
    private readonly IConfiguration _configuration;
    private const int EXPECTED_WEBHOOK_REQUEST_TIME = 5;

    public RandomWebhookJob(ILogger<RandomWebhookJob> logger, IConfiguration configuration,
        IHttpClientFactory httpClientFactory)
    {
        _logger = logger;
        _configuration = configuration;
        _httpClientFactory = httpClientFactory;
    }

    public async Task Invoke()
    {
        try
        {
            _httpClient = _httpClientFactory.CreateClient();
            if (Random.Shared.Next(0, EXPECTED_WEBHOOK_REQUEST_TIME) < 1)
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
