using Contracts;
using Contracts.Frontend.Auth;

namespace Application.Services.Abstract;

public interface IGithubAuthService
{
    Task<TokensModel> LoginByGithub(OAuthTokenModel request);
}
