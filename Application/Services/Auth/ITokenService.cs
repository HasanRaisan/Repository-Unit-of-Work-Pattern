using Application.Results;
using Infrastructure;
using Infrastructure.Data.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Application.Services.Auth
{
    public interface ITokenService
    {

        AuthResult CreateToken(ApplicationUserEntity user, List<string> roles, IList<Claim> userClaims); 
        string GenerateRefreshToken();
    }
}
