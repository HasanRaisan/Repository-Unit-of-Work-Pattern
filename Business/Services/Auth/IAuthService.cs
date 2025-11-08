using Business.DTOs.Identity;
using Business.Results;

namespace Business.Services.Auth
{
    public interface IAuthService
    {

        Task<Result<AuthResult>> LoginAsync(LoginDTO loginDTO);


        Task<Result<AuthResult>> RegisterAsync(RegisterDTO registerDTO);


        Task<Result<AuthResult>> AssignRoleAsync(AssignRoleDTO roleDTO);
    }
}
 