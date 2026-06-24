using Application.Extensions;
using Application.RefitClients;
using Application.Repositories.Abstract;
using Application.Services.Abstract;
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

    public async Task<TokensModel> LoginByGithub(OAuthTokenModel request)
    {
        var token = await GetGithubToken(request.IdToken);
        var userLogin = await GetUserLogin(token);
        var email = await GetUserEmail(token);
        Domain.User user = await FindOrCreateGithubUser(userLogin.Id, userLogin.Login, email);
        string accessToken = TokenGenerator.CreateJwtToken(user, _configuration[ConfigurationKeys.JwtKey]!);
        RefreshToken refreshToken = TokenGenerator.GenerateRefreshToken(user.Id);
        await _refreshTokenRepository.AddRefreshTokenAsync(refreshToken);
        return new TokensModel { JWTToken = accessToken, RefreshToken = refreshToken.Token };
    }

    private async Task<string> GetGithubToken(string code)
    {
        var tokenResponse = await _githubAuthClient.GetAccessTokenAsync(
            new GithubTokenRequestModel
            {
                ClientId = _configuration[ConfigurationKeys.GithubClientId]!,
                ClientSecret = _configuration[ConfigurationKeys.GithubClientSecret]!,
                Code = code,
                RedirectUri = _configuration[ConfigurationKeys.GithubRedirectUrl]!
            });

        return tokenResponse.AccessToken;
    }

    private async Task<GithubUserResponse> GetUserLogin(string token)
    {
        return await _githubApiClient.GetUserAsync($"Bearer {token}", _configuration[ConfigurationKeys.GithubAppName]!);
    }

    private async Task<string> GetUserEmail(string token)
    {
        var emails = await _githubApiClient.GetEmailsAsync($"Bearer {token}");
        return emails.First(x => x.Primary).Email;
    }

    private async Task<Domain.User> FindOrCreateGithubUser(long githubId, string username, string email)
    {
        Domain.User? login = await _userRepository.GetByGithubId(githubId);
        if (login != null)
        {
            return login;
        }

        Domain.User? user = await _userRepository.GetByEmail(email);
        if (user != null)
        {
            await _userRepository.SetUserGithubId(user, githubId);
            return user;
        }

        Domain.User newUser = new() { UserName = username, Email = email, GithubId = githubId, EmailConfirmed = true };
        await _userManager.CreateAsync(newUser);
        return newUser;
    }
}
