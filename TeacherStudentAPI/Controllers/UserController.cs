using Business.DTOs.Identity;
using Business.Results;
using Business.Services.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TeacherStudentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IAuthService _authService;

        public UserController(IAuthService authService)
        {
            _authService = authService;
        }



        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AuthResult))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Result<>))]
        public async Task<IActionResult> Register([FromBody] RegisterDTO registerDTO)
        {
            var result = await _authService.RegisterAsync(registerDTO);

            if (!result.IsSuccess)
            {
                return BadRequest(new { Errors = result.Errors });
            }

            return Ok(result.Data);
        }




        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AuthResult))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(Result<>))]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
        {
            var result = await _authService.LoginAsync(loginDTO);
       
            if (!result.IsSuccess)
            {
                return Unauthorized(new { Errors = result.Errors });
            }
       
            return Ok(result.Data);
        }



        [HttpPost("assignRole")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AuthResult))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)] // For not admin
        public async Task<IActionResult> AssignRole([FromBody] AssignRoleDTO assignRoleDTO)
        {
            var result = await _authService.AssignRoleAsync(assignRoleDTO);

            if (!result.IsSuccess)
            {
                return BadRequest(new { Errors = result.Errors });
            }

            return Ok(result.Data);
        }
    }
}
