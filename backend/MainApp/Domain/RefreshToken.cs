using Microsoft.EntityFrameworkCore;

namespace Domain;

[Index(nameof(Token), IsUnique = true)]
public class RefreshToken
{
    public long Id { get; set; }
    public User User { get; set; }
    public string UserId { get; set; }
    public string Token { get; set; }
    public DateTime Expires { get; set; }
    public bool IsRevoked { get; set; }
    public bool IsActive => !IsRevoked && DateTime.UtcNow < Expires;
}
