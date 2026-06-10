using System.Net.Http.Headers;
using System.Net.Http.Json;
using Application.Extensions;
using Application.RefitClients;
using Application.Services.Abstract;
using Contracts;
using Contracts.Frontend.Auth;
using Contracts.Internal.GithubAuth;
using DAL;
using Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Application.Services;

public class GithubAuthService : IGithubAuthService
{
    private readonly AppDbContext _dbContext;
    private readonly IGithubApiClient _githubApiClient;
    private readonly IGithubAuthClient _githubAuthClient;
    private readonly UserManager<User> _userManager;
    private readonly IConfiguration _configuration;

    public GithubAuthService(UserManager<User> userManager, IConfiguration configuration, AppDbContext context, IGithubApiClient githubApiClient, IGithubAuthClient githubAuthClient)
    {
        _userManager = userManager;
        _configuration = configuration;
        _dbContext = context;
        _githubApiClient = githubApiClient;
        _githubAuthClient = githubAuthClient;
    }

    public async Task<TokensModel> LoginByGithub(OAuthTokenModel request)
    {
        var token = await GetGithubToken(request.IdToken);
        var userLogin = await GetUserLogin(token);
        var email = await GetUserEmail(token);
        User user = await FindOrCreateGithubUser(userLogin.Id, userLogin.Login, email);
        string accessToken = TokenGenerator.CreateJwtToken(user, _configuration[ConfigurationKeys.JwtKey]!);
        RefreshToken refreshToken = await TokenGenerator.GenerateRefreshToken(user.Id, _dbContext);
        await _dbContext.RefreshTokens.AddAsync(refreshToken);
        await _dbContext.SaveChangesAsync();
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

    private async Task<User> FindOrCreateGithubUser(long githubId, string username, string email)
    {
        User? login = await _dbContext.Users.AsNoTracking().FirstOrDefaultAsync(x => x.GithubId == githubId);
        if (login != null)
        {
            return login;
        }

        User? user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Email == email);
        if (user != null)
        {
            user.GithubId = githubId;
            await _dbContext.SaveChangesAsync();
            return user;
        }

        User newUser = new() { UserName = username, Email = email, GithubId = githubId, EmailConfirmed = true };
        await _userManager.CreateAsync(newUser);
        return newUser;
    }
}
