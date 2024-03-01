using Auth.Api.Abstractions;
using Auth.Api.Model;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Auth.Api.Infrastructure;

public class FakeTokenService : ITokenService
{
    public string CreateAccessToken(UserIdentity identity) => "abc";
}

// dotnet add package System.IdentityModel.Tokens.Jwt
public class JwtTokenService : ITokenService
{
    public string CreateAccessToken(UserIdentity userIdentity)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        var identity = new ClaimsIdentity();
        identity.AddClaim(new Claim("license", "B"));
        identity.AddClaim(new Claim("fn", userIdentity.FirstName));

        var secretKey = "your-256-bit-secret";
        var key = Encoding.ASCII.GetBytes(secretKey);
        var credentials = new SymmetricSecurityKey(key);
        var signingCredentials = new SigningCredentials(credentials, SecurityAlgorithms.HmacSha256Signature);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = identity,
            Expires = DateTime.Now.AddMinutes(15),
            SigningCredentials = signingCredentials
        };

        var securityToken = tokenHandler.CreateToken(tokenDescriptor);

        var token = tokenHandler.WriteToken(securityToken);

        return token;
    }
}
