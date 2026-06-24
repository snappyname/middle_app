namespace Application.Services.Abstract.Auth;

public interface ICurrentUserService
{
    public Guid UserId { get; }
    public string Email { get; }
    public bool IsAdmin { get; }
}
