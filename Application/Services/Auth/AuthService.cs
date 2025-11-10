using AutoMapper;
using Application.Constants;
using Application.DTOs.Identity;
using Application.DTOs.Student;
using Application.DTOs.Teaher;
using Application.Results;
using Infrastructure.Data.Entities;
using Domain.Entities.Auth;
using Domain.Entities.Core;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Application.Services.Auth
{
    public class AuthService : IAuthService
    {
        // Dependencies
        private readonly UserManager<ApplicationUserEntity> _userManager;
        private readonly SignInManager<ApplicationUserEntity> _signInManager;
        private readonly RoleManager<ApplicationRoleEntity> _roleManager;
        private readonly IMapper _mapper;
        private readonly IValidator<RegisterDTO> _registerValidator;
        private readonly IValidator<LoginDTO> _loginValidator;
        private readonly ITokenService _tokenService;

        public AuthService(
            UserManager<ApplicationUserEntity> userManager,
            SignInManager<ApplicationUserEntity> signInManager,
            RoleManager<ApplicationRoleEntity> roleManager,
            IMapper mapper,
            IValidator<RegisterDTO> registerValidator,
            IValidator<LoginDTO> LoginValidator,
            ITokenService tokenService)
        {
            _userManager = userManager;
            this._mapper = mapper;
            this._registerValidator = registerValidator;
            this._loginValidator = LoginValidator;
            _tokenService = tokenService;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }



        public async Task<Result<AuthResult>> LoginAsync(LoginDTO loginDTO)
        {
            // 1️ DTO validation using FluentValidation
            var dtoValidationResult = await _loginValidator.ValidateAsync(loginDTO);
            if (!dtoValidationResult.IsValid)
            {
                var errors = dtoValidationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return ResultFactory.Fail<AuthResult>(errors);
            }

            // 2️ Map to Domain and validate business rules
            var userDomain = _mapper.Map<LoginDomain>(loginDTO);
            var domainValidationResult = userDomain.ValidateBusinessRules();
            if (!domainValidationResult.IsSuccess)
            {
                return ResultFactory.Fail<AuthResult>(domainValidationResult.Errors);
            }

            // 3️ Find user by email
            var user = await _userManager.FindByEmailAsync(userDomain.Email!);
            if (user == null || !await _userManager.CheckPasswordAsync(user, userDomain.Password!))
            {
                return ResultFactory.Fail<AuthResult>(new List<string> { "Invalid email or password." });
            }

            // 4️ Retrieve user claims and roles
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);

            // 5️ Generate JWT token
            var authResultToken = _tokenService.CreateToken((ApplicationUserEntity)user, roles.ToList(), userClaims.ToList());

            // 6️ Map to AuthResult DTO
            var authDTO = new AuthResult
            {
                IsAuthenticated = true,
                UserName = user.UserName ?? string.Empty,
                Email = user.Email ?? string.Empty,
                Role = roles.ToList(), // fix: assign roles list
                Token = authResultToken.Token,
                Expiration = authResultToken.Expiration,
                Message = "Login successful"
            };

            return ResultFactory.Success(authDTO);
        }
        public async Task<Result<AuthResult>> RegisterAsync(RegisterDTO registerDTO)
        {


            // 1 DTO validation using FluentValidation
            var dtoValidationResult = await _registerValidator.ValidateAsync(registerDTO);
            if (!dtoValidationResult.IsValid)
            {
                var errors = dtoValidationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return ResultFactory.Fail<AuthResult>(errors);
            }

            // 2 Map DTO to Domain
            var registerDomain = _mapper.Map<RegisterDomain>(registerDTO);

            // 3️ Domain validation / Application rules
            var domainValidationResult = registerDomain.ValidateBusinessRules();
            if (!domainValidationResult.IsSuccess)
            {
                return ResultFactory.Fail<AuthResult>(domainValidationResult.Errors);
            }

            // 4️ Map Domain to Identity entity
            var userEntity = _mapper.Map<ApplicationUserEntity>(registerDomain);
            userEntity.EmailConfirmed = true; // optional: auto-confirm email

            // 5️ Create user in Identity WITHOUT assigning any role
            var createResult = await _userManager.CreateAsync(userEntity, registerDomain.Password!);
            if (!createResult.Succeeded)
            {
                var errors = createResult.Errors.Select(e => e.Description).ToList();
                return ResultFactory.Fail<AuthResult>(errors);
            }

            // 6 Generate JWT token (optional: empty roles for now)
            var jwtTokenResult = _tokenService.CreateToken(
                userEntity,
                new List<string>(), // empty roles
                new List<Claim>()   // no extra claims
            );

            // 7 Map to AuthResult
            var authResultDTO = new AuthResult
            {
                IsAuthenticated = true,
                UserName = userEntity.UserName ?? string.Empty,
                Email = userEntity.Email ?? string.Empty,
                Role = new List<string>(), // empty roles initially
                Token = jwtTokenResult.Token,
                Expiration = jwtTokenResult.Expiration,
                Message = "Registration successful. Assign roles later."
            };

            return ResultFactory.Success(authResultDTO);
        }


        public async Task<Result<AuthResult>> AssignRoleAsync(AssignRoleDTO assignRoleDTO)
        {
            // 1️ Search for the user by ID and check if the role exists
            var user = await _userManager.FindByIdAsync(assignRoleDTO.UserId!);
            var roleExists = await _roleManager.RoleExistsAsync(assignRoleDTO.Role!);

            if (user == null || !roleExists)
            {
                return ResultFactory.Fail<AuthResult>(new List<string> { "User ID or Role not found." });
            }

            // 2️ Check if the user already has this role
            if (await _userManager.IsInRoleAsync(user, assignRoleDTO.Role!))
            {
                var alreadyAssigned = new AuthResult
                {
                    Message = $"User already has the '{assignRoleDTO.Role}' role.",
                    UserName = user.UserName!
                };
                return ResultFactory.Success(alreadyAssigned);
            }

            // 3️ Add the role to the user
            var result = await _userManager.AddToRoleAsync(user, assignRoleDTO.Role!);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description).ToList();
                return ResultFactory.Fail<AuthResult>(errors);
            }

            // 4️ Prepare success result
            var authModel = new AuthResult
            {
                IsAuthenticated = false, // just role assignment, not login
                UserName = user.UserName!,
                Email = user.Email!,
                Role = new List<string> { assignRoleDTO.Role! }, 
                Message = $"Role '{assignRoleDTO.Role}' assigned successfully to {user.UserName}."
            };

            return ResultFactory.Success(authModel);
        }
    }
} 