using Business.DTOs.Identity;
using Data;
using Data.Data.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Business.Services.Auth
{
    public interface ITokenService
    {

        AuthResultDTO CreateToken(ApplicationUserEntity user, List<string> roles, IList<Claim> userClaims); 
        string GenerateRefreshToken();
    }
}
