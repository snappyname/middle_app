namespace Contracts.Frontend.Auth;

public class TokensModel
{
    public string JWTToken { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
}
