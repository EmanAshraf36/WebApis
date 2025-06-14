using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace WebApi.Authority;
using Microsoft.IdentityModel.Tokens;

public class Authenticator
{
    public static bool Authenticate(string clientId, string secret)
    {
        var app = AppRepository.GetApplicationByClientId(clientId);
        if (app == null) return false;

        return (app.ClientId == clientId && app.Secret == secret);
    }

    public static string CreateToken(string clientId, DateTime expiresAt, string strSecretKey)
    {
        //Algorithm => PayLoad => SigningKey
        var app = AppRepository.GetApplicationByClientId(clientId);

        var claims = new List<Claim>
        {
            new Claim("AppName", app?.ApplicationName ?? string.Empty),
            new Claim("Read", (app?.Scopes ?? string.Empty).Contains("read") ? "true" : "false"),
            new Claim("Write", (app?.Scopes ?? string.Empty).Contains("write") ? "true" : "false"),
        };
        var secretKey = Encoding.ASCII.GetBytes(strSecretKey);
        
        var jwt = new JwtSecurityToken(
            signingCredentials: new SigningCredentials(
                new SymmetricSecurityKey(secretKey),
                SecurityAlgorithms.HmacSha256),
            claims: claims,
            expires: expiresAt,
            notBefore: DateTime.UtcNow);
        return new JwtSecurityTokenHandler().WriteToken(jwt);
    }
}