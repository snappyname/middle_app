using Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DAL;

public class AppDbContext : IdentityDbContext<User>
{
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    public DbSet<SensorsMap> SensorsMap { get; set; }
    public DbSet<SensorValue> SensorValues { get; set; }
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
}
