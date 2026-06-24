using Domain;

namespace Application.Repositories.Abstract
{
    public interface IRefreshTokenRepository
    {
        Task AddRefreshTokenAsync(RefreshToken refreshToken);
        Task<RefreshToken?> GetByTokenWithUserAsync(string token);
    }
}
