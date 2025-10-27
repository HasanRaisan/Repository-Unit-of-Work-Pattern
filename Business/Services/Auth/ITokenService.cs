using Data;
using Data.Data.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Business.Services.Auth
{
    public interface ITokenService
    {
        /// <summary>
        /// Generates a signed JWT for the specified user and their roles.
        /// </summary>
        /// <param name="user">The user entity object (e.g., IdentityUser).</param>
        /// <param name="roles">List of user's roles for claims inclusion.</param>
        /// <returns>The generated JWT string.</returns>
        string GenerateJwtToken(object user, List<string> roles); // Note: 'object user' will be replaced by your actual Identity User type

        /// <summary>
        /// Generates a cryptographic secure random refresh token string.
        /// </summary>
        /// <returns>The generated Refresh Token string.</returns>
        string GenerateRefreshToken();
    }
}
