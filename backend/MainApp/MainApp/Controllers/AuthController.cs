using Application.Services.Abstract;
using Application.Services.Abstract.Auth;
using Contracts;
using Contracts.Frontend.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MainApp.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IEmailAuthService _emailAuthService;
    private readonly IGithubAuthService _githubAuthService;

    public AuthController(IEmailAuthService emailAuthService, IGithubAuthService githubAuthService)
    {
        _emailAuthService = emailAuthService;
        _githubAuthService = githubAuthService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginModel loginModel)
    {
        return Ok(await _emailAuthService.Login(loginModel.Email, loginModel.Password));
    }

    [HttpPost("refreshToken")]
    public async Task<IActionResult> RefreshToken(RefreshTokenModel refreshTokenModel)
    {
        return Ok(await _emailAuthService.RefreshToken(refreshTokenModel.RefreshToken));
    }

    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterModel registerModel)
    {
        return Ok(await _emailAuthService.Register(registerModel));
    }
    
    [AllowAnonymous]
    [HttpPost("github")]
    public async Task<IActionResult> GithubLogin([FromBody] OAuthTokenModel request)
    {
        return Ok(await _githubAuthService.LoginByGithub(request));
    }
}
