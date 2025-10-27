using Business.Constants;
using Business.Domain.Auth;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Auth
{
    public class AuthService : IAuthService
    {
        // Dependencies for Identity Management
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        // Dependency for Token Generation (following SRP)
        private readonly ITokenService _tokenService;

        public AuthService(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            ITokenService tokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }

        public async Task<AuthResultDomain> Login(LoginDomain model)
        {
            // 1. Check user credentials using Identity SignInManager
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, isPersistent: false, lockoutOnFailure: false);

            if (!result.Succeeded)
            {
                return new AuthResultDomain
                {
                    Success = false,
                    Message = "Invalid login attempt. Check email or password."
                };
            }

            // 2. Retrieve user object and roles
            var user = await _userManager.FindByEmailAsync(model.Email)
                       ?? throw new InvalidOperationException("User not found after successful sign-in.");
            var roles = await _userManager.GetRolesAsync(user);

            // 3. Generate tokens using the dedicated service
            var jwtToken = _tokenService.GenerateJwtToken(user, roles.ToList());
            var refreshToken = _tokenService.GenerateRefreshToken();

            // NOTE: In a real app, the RefreshToken should be saved to the database here.

            return new AuthResultDomain
            {
                Success = true,
                Message = "Login successful.",
                Token = jwtToken,
                RefreshToken = refreshToken
            };              
        }

        public async Task<AuthResultDomain> Register(RegisterDomain model)
        {
            // 1. Basic validation (can be done better with FluentValidation or Data Annotations)
            if (model.Password != model.ConfirmPassword)
            {
                return new AuthResultDomain { Success = false, Message = "Password and confirmation password do not match." };
            }

            // 2. Create the new user object
            var newUser = new IdentityUser
            {
                UserName = model.Email,
                Email = model.Email,
                // Assuming FullName is needed in ApplicationUser, not IdentityUser.
            };

            // 3. Create user in the database
            var result = await _userManager.CreateAsync(newUser, model.Password);

            if (!result.Succeeded)
            {
                // Join all errors into a single message
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                return new AuthResultDomain { Success = false, Message = $"User creation failed: {errors}" };
            }

            // 4. Assign default role (e.g., 'User')
            await _userManager.AddToRoleAsync(newUser, RoleConstants.User);

            return new AuthResultDomain { Success = true, Message = "User registered successfully. Default role assigned." };
        }

        public async Task<AuthResultDomain> AssignRole(AssignRoleDomain model)
        {
            // 1. Find the target user
            var user = await _userManager.FindByIdAsync(model.UserId);
            if (user == null)
            {
                return new AuthResultDomain { Success = false, Message = "User not found." };
            }

            // 2. Check if the role exists (optional but recommended)
            // This would require injecting RoleManager<IdentityRole>

            // 3. Assign the role
            var result = await _userManager.AddToRoleAsync(user, model.RoleName);

            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                return new AuthResultDomain { Success = false, Message = $"Failed to assign role: {errors}" };
            }

            return new AuthResultDomain { Success = true, Message = $"Role '{model.RoleName}' assigned successfully." };
        }
    }

}