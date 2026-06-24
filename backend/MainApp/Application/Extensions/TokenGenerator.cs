using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using DAL;
using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Application.Extensions;

public static class TokenGenerator
{
    public static string CreateJwtToken(User user, string JWTKey)
    {
        var claims = new[]
        {
            new Claim(CustomClaims.UserId, user.Id),
            new Claim(CustomClaims.UserName, user.UserName),
            new Claim(CustomClaims.Email, user.Email),
            new Claim(CustomClaims.IsAdmin, user.IsAdmin.ToString()),
        };

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(JWTKey));

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(60),
            signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
    
    public static RefreshToken GenerateRefreshToken(string userId)
    {
        return new RefreshToken
        {
            Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
            Expires = DateTime.UtcNow.AddDays(30),
            UserId = userId
        };
    }
}
