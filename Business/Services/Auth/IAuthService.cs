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

        Task<Result<AuthResultDTO>> LoginAsync(LoginDTO loginDTO);


        Task<Result<AuthResultDTO>> RegisterAsync(RegisterDTO registerDTO);


        //Task<Result<AuthResultDTO>> AssignRoleAsync(AssignRoleDomain Domain);
    }
}
