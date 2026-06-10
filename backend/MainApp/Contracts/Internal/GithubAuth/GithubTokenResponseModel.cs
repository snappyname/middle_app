using System.Text.Json.Serialization;

namespace Contracts.Internal.GithubAuth;

public class GithubTokenResponseModel
{
    [JsonPropertyName("access_token")] public string AccessToken { get; set; }
}
