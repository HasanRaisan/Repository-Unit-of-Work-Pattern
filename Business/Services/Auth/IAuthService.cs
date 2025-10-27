using Business.Domain.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Auth
{
    public interface IAuthService
    {
        /// <summary>
        /// Attempts to log in a user.
        /// </summary>
        /// <param name="Domain">The login credentials (Email and Password).</param>
        /// <returns>An AuthResultDomain containing success status and JWT/Refresh Token if successful.</returns>
        Task<AuthResultDomain> Login(LoginDomain Domain);

        /// <summary>
        /// Registers a new user account.
        /// </summary>
        /// <param name="Domain">The registration details (FullName, Email, Passwords).</param>
        /// <returns>An AuthResultDomain indicating the result of the registration attempt.</returns>
        Task<AuthResultDomain> Register(RegisterDomain Domain);

        /// <summary>
        /// Assigns a specified role to a user.
        /// </summary>
        /// <param name="Domain">The user ID and the role name to assign.</param>
        /// <returns>An AuthResultDomain indicating the result of the role assignment.</returns>
        Task<AuthResultDomain> AssignRole(AssignRoleDomain Domain);
    }
}
