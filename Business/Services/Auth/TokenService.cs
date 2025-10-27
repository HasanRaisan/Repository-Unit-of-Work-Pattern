using Business.Configruration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Auth
{
    public class TokenService : ITokenService
    {
        private readonly JwtSettings _jwtSettings;

        // Dependency Injection of JwtSettings configuration
        public TokenService(IOptions<JwtSettings> jwtSettings)
        {
            _jwtSettings = jwtSettings.Value;
        }

        public string GenerateJwtToken(object user, List<string> roles)
        {
            // --- 1. Prepare Key and Credentials ---
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // --- 2. Prepare Claims (User Identity Data) ---
            var claims = new List<Claim>
            {
                // Assuming 'user' has properties like Id and Email (from IdentityUser)
                // We cast 'user' to dynamic or the actual Identity type for property access
                new Claim(JwtRegisteredClaimNames.Sub, (user as dynamic)?.Id.ToString() ?? throw new InvalidOperationException("User ID is required.")),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, (user as dynamic)?.Email ?? string.Empty),
            };

            // Add user roles as claims
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            // --- 3. Create Token Descriptor and Token ---
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(_jwtSettings.TokenValidityInMinutes),
                Issuer = _jwtSettings.Issuer,
                Audience = _jwtSettings.Audience,
                SigningCredentials = credentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(securityToken);
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            // Convert byte array to a base64 string for storage/transmission
            return Convert.ToBase64String(randomNumber);
        }
    }
}
