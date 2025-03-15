using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GCDomain.Helpers
{
    public class TokenHelper(IConfiguration config)
    {
        private readonly JwtSecurityTokenHandler _tokenHandler = new();
        private readonly SymmetricSecurityKey _signingKey = new(Encoding.UTF8.GetBytes(config["Jwt:Key"]!));

        public string GetJwtToken(int userId)
        {
            var token = GenerateJwtToken(userId);
            return IsTokenValid(token) ? token : "";
        }

        private string GenerateJwtToken(int userId)
        {
            var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, userId.ToString()),
            new(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64)
        };

            var credentials = new SigningCredentials(_signingKey, SecurityAlgorithms.HmacSha256);

            int expirationInMinutes = int.Parse(config["Jwt:ExpirationInMinutes"] ?? "10");

            var description = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Issuer = config["Jwt:Issuer"],
                Audience = config["Jwt:Audience"],
                IssuedAt = DateTime.UtcNow,
                Expires = DateTime.UtcNow.AddMinutes(expirationInMinutes),
                SigningCredentials = credentials
            };

            var token = _tokenHandler.CreateToken(description);
            return _tokenHandler.WriteToken(token);
        }

        private bool IsTokenValid(string token)
        {
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = _signingKey,
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidIssuer = config["Jwt:Issuer"],
                ValidAudience = config["Jwt:Audience"],
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

            try
            {
                _tokenHandler.ValidateToken(token, validationParameters, out _);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
