using Business.Configruration;
using Business.Results;
using Data.Data.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http.Headers;
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

        public AuthResult  CreateToken(ApplicationUserEntity user, List<string> roles, IList<Claim> userClaims)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier,user.Id?? ""),
                new Claim(ClaimTypes.Email,user.Email?? ""),
                new Claim(ClaimTypes.Name,user.UserName?? "")
            };

            claims.AddRange(userClaims);
            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));

            var token = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience, 
            expires: DateTime.Now.AddMinutes(_jwtSettings.Lifetime), 
            claims: claims,
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            return new AuthResult
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = token.ValidTo,
                UserName = user.UserName ?? ""
            };

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
