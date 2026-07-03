using Contracts.Frontend.Auth;

namespace Application.Services.Abstract.Auth;

public interface IGithubAuthService
{
    Task<TokensModel> LoginByGithub(OAuthTokenModel request, CancellationToken cancellationToken = default);
}
