using Contracts;
using Contracts.Frontend.Auth;

namespace Application.Services.Abstract.Auth;

public interface IEmailAuthService
{
    Task<TokensModel> Login(string email, string password);
    Task<TokensModel> RefreshToken(string refreshToken);
    Task<TokensModel> Register(RegisterModel model);
}
