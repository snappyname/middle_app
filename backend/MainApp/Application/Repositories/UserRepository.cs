using Application.Repositories.Abstract;
using DAL;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Application.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _dbContext;

    public UserRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<User?> GetById(Guid id)
    {
        return await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == id.ToString()); 
    }

    public async Task<User?> GetByGithubId(long id)
    {
        return await _dbContext.Users.FirstOrDefaultAsync(x => x.GithubId == id);
    }

    public async Task<User?> GetByEmail(string email)
    {
        return await _dbContext.Users.FirstOrDefaultAsync(x => x.NormalizedEmail == email.ToUpper());
    }

    public async Task<User> SetUserGithubId(User user, long githubId)
    {
       user.GithubId = githubId;
       await _dbContext.SaveChangesAsync();
       return user;
    }

    public async Task<List<User>> GetAllUsers()
    {
        var users = await _dbContext.Users.ToListAsync();
        return users;
    }

    public async Task UpdateUser(User user)
    {
        await _dbContext.SaveChangesAsync();
    }
}
