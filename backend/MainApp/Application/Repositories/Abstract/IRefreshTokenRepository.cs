using Domain;

namespace Application.Repositories.Abstract
{
    public interface IRefreshTokenRepository
    {
        Task AddRefreshTokenAsync(RefreshToken refreshToken, CancellationToken cancellationToken = default);
        Task<RefreshToken?> GetByTokenWithUserAsync(string token, CancellationToken cancellationToken = default);
    }
}
