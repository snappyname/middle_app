using Coravel.Invocable;

namespace iot_hub.BackgroundJobs;

public class GetTemperatureDataJob : IInvocable
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<GetTemperatureDataJob> _logger;

    public GetTemperatureDataJob(IHttpClientFactory factory, ILogger<GetTemperatureDataJob> logger)
    {
        _httpClient = factory.CreateClient();
        _logger = logger;
    }

    public async Task Invoke()
    {
        try
        {
            //var response = await _httpClient.GetAsync("http://localhost:9080/api/Temperature");
            //_logger.LogInformation($"GetTemperatureDataJob response: {await response.Content.ReadAsStringAsync()}");
        }
        catch
        {
            // ignored
        }
    }
}
