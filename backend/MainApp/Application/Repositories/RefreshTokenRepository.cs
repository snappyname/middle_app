using Application.Repositories.Abstract;
using DAL;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Application.Repositories;

public class RefreshTokenRepository : IRefreshTokenRepository
{
    private readonly AppDbContext _dbContext;

    public RefreshTokenRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddRefreshTokenAsync(RefreshToken refreshToken, CancellationToken cancellationToken)
    {
        await _dbContext.RefreshTokens.AddAsync(refreshToken, cancellationToken); 
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<RefreshToken?> GetByTokenWithUserAsync(string token, CancellationToken cancellationToken)
    {
        return await _dbContext.RefreshTokens
            .Include(x => x.User)
            .FirstOrDefaultAsync(x => x.Token == token, cancellationToken);
    }
}
