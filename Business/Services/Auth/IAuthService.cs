using Business.Domain.Auth;
using Business.DTOs.Identity;
using Business.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Auth
{
    public interface IAuthService
    {

        Task<Result<AuthResult>> LoginAsync(LoginDTO loginDTO);


        Task<Result<AuthResult>> RegisterAsync(RegisterDTO registerDTO);


        Task<Result<AuthResult>> AssignRoleAsync(AssignRoleDTO roleDTO);
    }
}
 