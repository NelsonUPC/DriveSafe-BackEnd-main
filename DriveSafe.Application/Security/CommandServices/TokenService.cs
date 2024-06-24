using System.Security.Claims;
using System.Text;
using DriveSafe.Domain.Publishing.Models.Entities;
using DriveSafe.Domain.Security.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

namespace DriveSafe.Application.Security.CommandServices;

public class TokenService : ITokenService
{
    private readonly IConfiguration _configuration;
    //string secret = "drive-safe-no-como-heisenwolf-gogogogogogogogogogogog";
    
    public TokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public string GenerateToken(User user, int Id)
    {
        
        var key = Encoding.ASCII.GetBytes(_configuration["AppSettings:Secret"]); //Debe ser igual que: var key = Encoding.ASCII.GetBytes(secret); (linea 41) 
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

