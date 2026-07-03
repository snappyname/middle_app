using Application.Extensions;
using Application.RefitClients;
using Application.Repositories.Abstract;
using Application.Services.Abstract;
using Application.Services.Abstract.Auth;
using Contracts.Frontend.Auth;
using Contracts.Internal.GithubAuth;
using Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace Application.Services;

public class GithubAuthService : IGithubAuthService
{
    private readonly IGithubApiClient _githubApiClient;
    private readonly IGithubAuthClient _githubAuthClient;
    private readonly UserManager<Domain.User> _userManager;
    private readonly IConfiguration _configuration;
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly IUserRepository _userRepository;

    public GithubAuthService(UserManager<Domain.User> userManager, IConfiguration configuration, IGithubApiClient githubApiClient, IGithubAuthClient githubAuthClient, IRefreshTokenRepository refreshTokenRepository, IUserRepository userRepository)
    {
        _userManager = userManager;
        _configuration = configuration;
        _githubApiClient = githubApiClient;
        _githubAuthClient = githubAuthClient;
        _refreshTokenRepository = refreshTokenRepository;
        _userRepository = userRepository;
    }

    public async Task<TokensModel> LoginByGithub(OAuthTokenModel request, CancellationToken cancellationToken)
    {
        var token = await GetGithubToken(request.IdToken, cancellationToken);
        var userLogin = await GetUserLogin(token, cancellationToken);
        var email = await GetUserEmail(token, cancellationToken);
        Domain.User user = await FindOrCreateGithubUser(userLogin.Id, userLogin.Login, email, cancellationToken);
        string accessToken = TokenGenerator.CreateJwtToken(user, _configuration[ConfigurationKeys.JwtKey]!);
        RefreshToken refreshToken = TokenGenerator.GenerateRefreshToken(user.Id);
        await _refreshTokenRepository.AddRefreshTokenAsync(refreshToken, cancellationToken);
        return new TokensModel { JWTToken = accessToken, RefreshToken = refreshToken.Token };
    }

    private async Task<string> GetGithubToken(string code, CancellationToken cancellationToken)
    {
        var tokenResponse = await _githubAuthClient.GetAccessTokenAsync(
            new GithubTokenRequestModel
            {
                ClientId = _configuration[ConfigurationKeys.GithubClientId]!,
                ClientSecret = _configuration[ConfigurationKeys.GithubClientSecret]!,
                Code = code,
                RedirectUri = _configuration[ConfigurationKeys.GithubRedirectUrl]!
            }, cancellationToken);

        return tokenResponse.AccessToken;
    }

    private async Task<GithubUserResponse> GetUserLogin(string token, CancellationToken cancellationToken)
    {
        return await _githubApiClient.GetUserAsync($"Bearer {token}", _configuration[ConfigurationKeys.GithubAppName]!, cancellationToken);
    }

    private async Task<string> GetUserEmail(string token, CancellationToken cancellationToken)
    {
        var emails = await _githubApiClient.GetEmailsAsync($"Bearer {token}", cancellationToken);
        return emails.First(x => x.Primary).Email;
    }

    private async Task<Domain.User> FindOrCreateGithubUser(long githubId, string username, string email, CancellationToken cancellationToken)
    {
        Domain.User? login = await _userRepository.GetByGithubId(githubId, cancellationToken);
        if (login != null)
        {
            return login;
        }

        Domain.User? user = await _userRepository.GetByEmail(email, cancellationToken);
        if (user != null)
        {
            await _userRepository.SetUserGithubId(user, githubId, cancellationToken);
            return user;
        }

        Domain.User newUser = new() { UserName = username, Email = email, GithubId = githubId, EmailConfirmed = true };
        await _userManager.CreateAsync(newUser);
        return newUser;
    }
}
