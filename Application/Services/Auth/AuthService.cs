using AutoMapper;
using Application.Constants;
using Application.DTOs.Identity;
using Application.DTOs.Student;
using Application.DTOs.Teaher;
using Application.Results;
using Infrastructure.Data.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Domain.Entities.Auth.Login;
using Domain.Entities.Auth.Register;
using Domain.Entities.Auth.AssignRole;

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
        private readonly IValidator<AssignRoleDTO> _assignRoleValidator;
        private readonly ITokenService _tokenService;

        public AuthService(
            UserManager<ApplicationUserEntity> userManager,
            SignInManager<ApplicationUserEntity> signInManager,
            RoleManager<ApplicationRoleEntity> roleManager,
            IMapper mapper,
            IValidator<RegisterDTO> registerValidator,
            IValidator<LoginDTO> LoginValidator,
            IValidator<AssignRoleDTO> assignRoleValidator,
            ITokenService tokenService)
        {
            _userManager = userManager;
            this._mapper = mapper;
            this._registerValidator = registerValidator;
            this._loginValidator = LoginValidator;
            this._assignRoleValidator = assignRoleValidator;
            _tokenService = tokenService;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }



        public async Task<Result<AuthResultDTO>> LoginAsync(LoginDTO loginDTO)
        {
            // 1️ DTO validation using FluentValidation
            var dtoValidationResult = await _loginValidator.ValidateAsync(loginDTO);
            if (!dtoValidationResult.IsValid)
            {
                var errors = dtoValidationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return ResultFactory.Fail<AuthResultDTO>( ErrorType.ValidationError, errors);
            }

            // 2️ Map to Domain and validate business rules
            var domainValidationResult = LoginDomain.Create(loginDTO.Email,loginDTO.Password);
            if (!domainValidationResult.IsSuccess)            
                return ResultFactory.Fail<AuthResultDTO>( ErrorType.ValidationError, domainValidationResult.Errors);
            
            var userDomain = domainValidationResult.Data;

            try
            {
                // 3️ Find user by email
                var user = await _userManager.FindByEmailAsync(userDomain.Email!);
                if (user == null || !await _userManager.CheckPasswordAsync(user, userDomain.Password!))
                {
                    return ResultFactory.Fail<AuthResultDTO>(ErrorType.ValidationError, "Invalid email or password.");
                }

                // 4️ Retrieve user claims and roles
                var userClaims = await _userManager.GetClaimsAsync(user);
                var roles = await _userManager.GetRolesAsync(user);

                // 5️ Generate JWT token
                var authResultToken = _tokenService.CreateToken((ApplicationUserEntity)user, roles.ToList(), userClaims.ToList());

                // 6️ Map to AuthResult DTO
                var authDTO = new AuthResultDTO
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
            catch (Exception ex)
            {
                return ResultFactory.Fail<AuthResultDTO>(ErrorType.InternalError, "An unexpected error occurred during the login process.");
            }

        }
        public async Task<Result<AuthResultDTO>> RegisterAsync(RegisterDTO DTO)
        {


            // 1 DTO validation using FluentValidation
            var dtoValidationResult = await _registerValidator.ValidateAsync(DTO);
            if (!dtoValidationResult.IsValid)
            {
                var errors = dtoValidationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return ResultFactory.Fail<AuthResultDTO>(ErrorType.ValidationError, errors);
            }


            // 3️ Map, Domain validation / Application rules
            var domainValidationResult = RegisterDomain.Create(DTO.FullName, DTO.UserName, DTO.Email, DTO.Password, DTO.ConfirmPassword);
            if (!domainValidationResult.IsSuccess)            
                return ResultFactory.Fail<AuthResultDTO>(ErrorType.ValidationError, domainValidationResult.Errors);

            var registerDomain = domainValidationResult.Data;

            // 4️ Map Domain to Identity entity
            var userEntity = _mapper.Map<ApplicationUserEntity>(registerDomain);
            userEntity.EmailConfirmed = true; // optional: auto-confirm email
            try
            {
                // 5️ Create user in Identity WITHOUT assigning any role
                var createResult = await _userManager.CreateAsync(userEntity, registerDomain.Password!);
                if (!createResult.Succeeded)
                {
                    var errors = createResult.Errors.Select(e => e.Description).ToList();
                    return ResultFactory.Fail<AuthResultDTO>(ErrorType.Conflict, errors);
                }

                // 6 Generate JWT token (optional: empty roles for now)
                var jwtTokenResult = _tokenService.CreateToken(
                    userEntity,
                    new List<string>(), // empty roles
                    new List<Claim>()   // no extra claims
                );

                // 7 Map to AuthResult
                var authResultDTO = new AuthResultDTO
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
            catch (Exception ex)
            {
                return ResultFactory.Fail<AuthResultDTO>(ErrorType.InternalError, "An unexpected error occurred during user registration.");
            }

        }


        public async Task<Result<AuthResultDTO>> AssignRoleAsync(AssignRoleDTO DTO)
        {
            var dtoValidationResult = await _assignRoleValidator.ValidateAsync(DTO);
            if (!dtoValidationResult.IsValid)
            {
                var errors = dtoValidationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return ResultFactory.Fail<AuthResultDTO>(ErrorType.ValidationError, errors);
            }
            var checkDomain = AssignRoleDomain.Create(DTO.UserId, DTO.Role, new List<string> { "Admin", "Teacher", "Student" });
            if(!checkDomain.IsSuccess)
                return ResultFactory.Fail<AuthResultDTO>(ErrorType.ValidationError, checkDomain.Errors);

            try
            {
                // 1️ Search for the user by ID and check if the role exists
                var user = await _userManager.FindByIdAsync(DTO.UserId!);
                var roleExists = await _roleManager.RoleExistsAsync(DTO.Role!);

                if (user == null || !roleExists)
                {
                    return ResultFactory.Fail<AuthResultDTO>(ErrorType.NotFound, "User ID or Role not found." );
                }

                // 2️ Check if the user already has this role
                if (await _userManager.IsInRoleAsync(user, DTO.Role!))
                {
                    var alreadyAssigned = new AuthResultDTO
                    {
                        Message = $"User already has the '{DTO.Role}' role.",
                        UserName = user.UserName!
                    };
                    return ResultFactory.Success(alreadyAssigned);
                }

                // 3️ Add the role to the user
                var result = await _userManager.AddToRoleAsync(user, DTO.Role!);
                if (!result.Succeeded)
                {
                    var errors = result.Errors.Select(e => e.Description).ToList();
                    return ResultFactory.Fail<AuthResultDTO>(ErrorType.Conflict, errors);
                }

                // 4️ Prepare success result
                var authModel = new AuthResultDTO
                {
                    IsAuthenticated = false, // just role assignment, not login
                    UserName = user.UserName!,
                    Email = user.Email!,
                    Role = new List<string> { DTO.Role! },
                    Message = $"Role '{DTO.Role}' assigned successfully to {user.UserName}."
                };

                return ResultFactory.Success(authModel);
            }
            catch(Exception ex)
            {
                return ResultFactory.Fail<AuthResultDTO>(ErrorType.InternalError, "An unexpected error occurred during role assignment.");
            }
        }
    }
} 