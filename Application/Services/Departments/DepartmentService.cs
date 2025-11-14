using Application.DTOs.Department;
using Application.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Departments
{
    public class DepartmentService : IDepartment
    {
        public Task<Result<DepartmentDTO>> AddAsync(CreateDepartmentDTO DTO)
        {
            throw new NotImplementedException();
        }

        public Task<Result<bool>> DeleteAsync(int ID)
        {
            throw new NotImplementedException();
        }

        public Task<Result<IEnumerable<DepartmentDTO>>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Result<DepartmentDTO>> GetByIDAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Result<DepartmentDTO>> UpdateAsync(UpdateDepartmentDTO DTO)
        {
            throw new NotImplementedException();
        }
    }
}
