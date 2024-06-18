using System.Security.Claims;
using System.Text;
using DriveSafe.Domain.Publishing.Models.Entities;
using DriveSafe.Domain.Security.Services;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

namespace DriveSafe.Application.Security.CommandServices;

public class TokenService : ITokenService
{
    public string GenerateToken(User user, int Id)
    {
        var secret = "drive-safe-no-como-heisenwolf-gogogogogogogogogogogog";
        var key = Encoding.ASCII.GetBytes(secret);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Sid, Id.ToString()),
                new Claim(ClaimTypes.Name, user.Gmail)
            }),
            Expires = DateTime.UtcNow.AddHours(4),
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var tokenHandler = new JsonWebTokenHandler();

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return token;
    }

    public Task<int?> ValidateToken(string token)
    {
        throw new NotImplementedException();
    }
}