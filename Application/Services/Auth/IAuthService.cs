using Application.DTOs.Identity;
using Application.Results;

namespace Application.Services.Auth
{
    public interface IAuthService
    {

        Task<Result<AuthResult>> LoginAsync(LoginDTO loginDTO);


        Task<Result<AuthResult>> RegisterAsync(RegisterDTO registerDTO);


        Task<Result<AuthResult>> AssignRoleAsync(AssignRoleDTO roleDTO);
    }
}
 