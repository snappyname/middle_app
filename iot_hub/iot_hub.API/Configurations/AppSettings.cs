namespace iot_hub.Configurations;

public class AppSettings
{
    public SensorsApiSettings SensorsApi { get; set; } = new();
    public string KafkaUrl { get; set; } = string.Empty;
    public int RequestsRateInSeconds { get; set; }
}
