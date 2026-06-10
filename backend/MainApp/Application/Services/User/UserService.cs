using Application.Services.Abstract;
using DAL;
using Microsoft.EntityFrameworkCore;

namespace Application.Services;

public class UserService : IUserService
{
    private readonly AppDbContext _dbContext;

    public UserService(AppDbContext context)
    {
        _dbContext = context;
    }

    public async Task<Domain.User> GetMe(string userId)
    {
        return (await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == userId))!;
    }
}
