using Application.Repositories.Abstract;
using DAL;
using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Application.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _dbContext;
    private readonly IMemoryCache _cache;
    private const string CacheKey = "user_sensors_";

    public UserRepository(AppDbContext dbContext, IMemoryCache cache)
    {
        _dbContext = dbContext;
        _cache = cache;
    }

    public async Task<User?> GetById(Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == id.ToString(), cancellationToken); 
    }

    public async Task<User?> GetUserWithSensorsById(string id, CancellationToken cancellationToken)
    {
        return await _dbContext.Users
            .Include(x=> x.Sensors)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken); 
    }

    public async Task<User?> GetByGithubId(long id, CancellationToken cancellationToken)
    {
        return await _dbContext.Users.FirstOrDefaultAsync(x => x.GithubId == id, cancellationToken);
    }

    public async Task<User?> GetByEmail(string email, CancellationToken cancellationToken)
    {
        return await _dbContext.Users.FirstOrDefaultAsync(x => x.NormalizedEmail == email.ToUpper(), cancellationToken);
    }

    public async Task<User> SetUserGithubId(User user, long githubId, CancellationToken cancellationToken)
    {
       user.GithubId = githubId;
       await _dbContext.SaveChangesAsync(cancellationToken);
       return user;
    }

    public async Task<List<User>> GetAllUsers(CancellationToken cancellationToken)
    {
        var users = await _dbContext.Users.ToListAsync(cancellationToken);
        return users;
    }

    public async Task<List<User>> GetAllUsersWithSensors(CancellationToken cancellationToken)
    {
       return await _dbContext.Users.Include(x=> x.Sensors).ToListAsync(cancellationToken: cancellationToken);
    }

    public async Task UpdateUser(User user, CancellationToken cancellationToken)
    {
        _cache.Remove($"{CacheKey}{user.Id}");
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<List<SensorsMap>> GetUserSensors(string userId, CancellationToken cancellationToken)
    {
        var cacheKey = $"{CacheKey}{userId}";

        return await _cache.GetOrCreateAsync(cacheKey, async entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10);
            var dbUser = await _dbContext.Users
                .Include(u => u.Sensors)
                .FirstOrDefaultAsync(u => u.Id == userId, cancellationToken);

            return dbUser?.Sensors.ToList() ?? [];
        }) ?? [];
    }
}
