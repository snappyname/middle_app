namespace Application.Services.Abstract;

public interface ICurrentUserService
{
    public string UserId { get; }
    public string Email { get; }
}
