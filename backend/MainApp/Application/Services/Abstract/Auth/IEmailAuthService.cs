using Contracts;
using Contracts.Frontend.Auth;

namespace Application.Services.Abstract.Auth;

public interface IEmailAuthService
{
    Task<TokensModel> Login(string email, string password, CancellationToken cancellationToken = default);
    Task<TokensModel> RefreshToken(string refreshToken, CancellationToken cancellationToken = default);
    Task<TokensModel> Register(RegisterModel model, CancellationToken cancellationToken = default);
}
