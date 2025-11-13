using Application.DTOs.Identity;
using Application.Results;

namespace Application.Services.Auth
{
    public interface IAuthService
    {

        Task<Result<AuthResultDTO>> LoginAsync(LoginDTO loginDTO);


        Task<Result<AuthResultDTO>> RegisterAsync(RegisterDTO registerDTO);


        Task<Result<AuthResultDTO>> AssignRoleAsync(AssignRoleDTO roleDTO);
    }
}
 