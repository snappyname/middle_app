using Contracts;
using Contracts.Internal.GithubAuth;
using Refit;

namespace Application.RefitClients;

public interface IGithubApiClient
{
    [Get("/user")]
    Task<GithubUserResponse> GetUserAsync([Header("Authorization")] string authorization, [Header("User-Agent")] string userAgent);

    [Get("/user/emails")]
    Task<List<GithubEmailModel>> GetEmailsAsync([Header("Authorization")] string authorization);
}
