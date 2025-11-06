using AutoMapper;
using Business.Constants;
using Business.Domain.Auth;
using Business.Domains.Core;
using Business.DTOs.Identity;
using Business.DTOs.Student;
using Business.Result;
using Data.Data.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Business.Services.Auth
{
    public class AuthService : IAuthService
    {
        // Dependencies for Identity Management
        private readonly UserManager<ApplicationUserEntity> _userManager;
        private readonly SignInManager<ApplicationUserEntity> _signInManager;
        private readonly RoleManager<ApplicationRoleEntity> _roleManager;
        private readonly IMapper _mapper;
        private readonly IValidator<RegisterDomain> _validator;

        // Dependency for Token Generation (following SRP)
        private readonly ITokenService _tokenService;

        public AuthService(
            UserManager<ApplicationUserEntity> userManager,
            SignInManager<ApplicationUserEntity> signInManager,
            RoleManager<ApplicationRoleEntity> roleManager,
            IMapper mapper,
            IValidator<RegisterDomain> validator,
            ITokenService tokenService)
        {
            _userManager = userManager;
            this._mapper = mapper;
            this._validator = validator;
            _tokenService = tokenService;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }



        public async Task<Result<AuthResult>> LoginAsync(LoginDTO loginDTO)
        {
            var userDomain = _mapper.Map<LoginDomain>(loginDTO);

            var user = await _userManager.FindByEmailAsync(userDomain.Email!);

            if (user == null || !await _userManager.CheckPasswordAsync(user, userDomain.Password!))
            {
                return new Result<AuthResult>
                {
                    IsSuccess = false,
                    Errors = new List<string> { "Invalid email or password." } 
                };
            }
            
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);

            var authResult = _tokenService.CreateToken((ApplicationUserEntity)user, roles.ToList(), userClaims.ToList());

            var authDTO = new AuthResult
            {
                IsAuthenticated = true,
                UserName = user.UserName ?? string.Empty,
                Email = user.Email ?? string.Empty,
                Role = [.. roles],
                Token = authResult.Token, 
                Expiration = authResult.Expiration,
                Message = "Login successful"
            };

            return new Result<AuthResult>
            {
                IsSuccess = true,
                Data = authDTO
            };
        }
        public async Task<Result<AuthResult>> RegisterAsync(RegisterDTO registerDTO)
        {
            var registerDomain = _mapper.Map<RegisterDomain>(registerDTO);

            var validationResult = await _validator.ValidateAsync(registerDomain);

            if (!validationResult.IsValid)
            {
                return new Result<AuthResult>()
                {
                    IsSuccess = false,
                    Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }

            var userEntity = _mapper.Map<ApplicationUserEntity>(registerDomain);
            userEntity.EmailConfirmed = true;

            var result = await _userManager.CreateAsync(user: userEntity, registerDomain.Password!);

            if (!result.Succeeded)
            {
                return new Result<AuthResult>
                {
                    IsSuccess = false,
                    Errors = result.Errors.Select(e => e.Description).ToList()
                };
            }

            await _userManager.AddToRoleAsync(userEntity, RoleConstants.Student );

            var jwtTokenResult = _tokenService.CreateToken(
            userEntity,
            new List<string> { "Student" },
            new List<Claim>() );

            var authResultDTO = new AuthResult
            {
                IsAuthenticated = true,
                UserName = userEntity.UserName ?? string.Empty,
                Email = userEntity.Email ?? string.Empty,
                Role = ["Student"],
                Token = jwtTokenResult.Token,
                Expiration = jwtTokenResult.Expiration,
                Message = "Registration successful"
            };

            return new Result<AuthResult>
            {
                IsSuccess = true,
                Data = authResultDTO
            };
        }
        public async Task<Result<AuthResult>> AssignRoleAsync(AssignRoleDTO assignRoleDTO)
        {
            // A. Search for the user and role
            var user = await _userManager.FindByIdAsync(assignRoleDTO.UserId!);
            var roleExists = await _roleManager.RoleExistsAsync(assignRoleDTO.Role!);

            if (user == null || !roleExists)
            {
                return new Result<AuthResult>
                {
                    IsSuccess = false,
                    Errors = new List<string> { "User ID or Role not found." }
                };
            }
            // B. Check if the user already has this role
            if (await _userManager.IsInRoleAsync(user, assignRoleDTO.Role!))
            {
                return new Result<AuthResult>
                {
                    IsSuccess = true,
                    Data = new AuthResult
                    {
                        Message = $"User already has the '{assignRoleDTO.Role}' role.",
                        UserName = user.UserName!
                    }
                };
            }
            // C. Add the role
            var result = await _userManager.AddToRoleAsync(user, assignRoleDTO.Role!);

            if (!result.Succeeded)
            {
                return new Result<AuthResult>
                {
                    IsSuccess = false,
                    Errors = result.Errors.Select(e => e.Description).ToList()
                };
            }
            // D. Success
            var authModel = new AuthResult
            {
                IsAuthenticated = false,
                UserName = user.UserName!,
                Email = user.Email!,
                Role = [assignRoleDTO.Role!], 
                Message = $"Role '{assignRoleDTO.Role}' assigned successfully to {user.UserName}."
            };

            return new Result<AuthResult>
            {
                IsSuccess = true,
                Data = authModel
            };

        }


    }

} 