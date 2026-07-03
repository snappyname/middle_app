using Contracts;
using Contracts.Internal.GithubAuth;
using Refit;

namespace Application.RefitClients;

public interface IGithubAuthClient
{
    [Post("/login/oauth/access_token")]
    [Headers("Accept: application/json")]
    Task<GithubTokenResponseModel> GetAccessTokenAsync(
        [Body(BodySerializationMethod.UrlEncoded)] GithubTokenRequestModel requestModel, CancellationToken cancellationToken = default);
}
