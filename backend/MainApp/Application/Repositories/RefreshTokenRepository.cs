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

    public async Task AddRefreshTokenAsync(RefreshToken refreshToken)
    {
        await _dbContext.RefreshTokens.AddAsync(refreshToken); 
        await _dbContext.SaveChangesAsync();
    }

    public async Task<RefreshToken?> GetByTokenWithUserAsync(string token)
    {
        return await _dbContext.RefreshTokens
            .Include(x => x.User)
            .FirstOrDefaultAsync(x => x.Token == token);
    }
}
