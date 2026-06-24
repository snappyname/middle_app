using Application.Exceptions;
using Application.Extensions;
using Application.Repositories.Abstract;
using Application.Services.Abstract.Auth;
using Contracts;
using Contracts.Frontend.Auth;
using DAL;
using Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Application.Services.Auth;

public class EmailAuthService : IEmailAuthService
{
    private readonly UserManager<Domain.User> _userManager;
    private readonly IConfiguration _configuration;
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly IUserRepository _userRepository;

    public EmailAuthService(UserManager<Domain.User> userManager, IConfiguration configuration, IRefreshTokenRepository refreshTokenRepository, IUserRepository userRepository)
    {
        _userManager = userManager;
        _configuration = configuration;
        _refreshTokenRepository = refreshTokenRepository;
        _userRepository = userRepository;
    }

    public async Task<TokensModel> Login(string email, string password)
    {
        var user = await _userManager.FindByNameAsync(email);

        if (user == null || !await _userManager.CheckPasswordAsync(user, password))
        {
            throw new LoginOrPasswordInvalidException();
        }

        var jwtToken = TokenGenerator.CreateJwtToken(user, _configuration[ConfigurationKeys.JwtKey]!);
        var refreshToken = TokenGenerator.GenerateRefreshToken(user.Id);
        await _refreshTokenRepository.AddRefreshTokenAsync(refreshToken);
      
        return new TokensModel { JWTToken = jwtToken, RefreshToken = refreshToken.Token };
    }

    public async Task<TokensModel> RefreshToken(string token)
    {
        var refreshToken = await _refreshTokenRepository.GetByTokenWithUserAsync(token);
        if (refreshToken == null || refreshToken.IsRevoked)
        {
            throw new RefreshTokenInvalid();
        }

        refreshToken.IsRevoked = true;
        var newJwtToken = TokenGenerator.CreateJwtToken(refreshToken.User, _configuration[ConfigurationKeys.JwtKey]!);
        var newRefreshToken =  TokenGenerator.GenerateRefreshToken(refreshToken.UserId);
        await _refreshTokenRepository.AddRefreshTokenAsync(refreshToken);
        return new TokensModel { JWTToken = newJwtToken, RefreshToken = newRefreshToken.Token };
    }

    public async Task<TokensModel> Register(RegisterModel model)
    {
        if ((await _userRepository.GetAllUsers()).Any(x => x.Email == model.Email))
        {
            throw new UserWithThisEmailExist();
        }

        var newUser = new Domain.User { UserName = model.Email, Email = model.Email };
        var result = await _userManager.CreateAsync(newUser, model.Password);
        if (result.Succeeded) { return await Login(newUser.Email, model.Password); }

        throw new PasswordFormatInvalidException();
    }
}
