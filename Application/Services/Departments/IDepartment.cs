using Application.DTOs.Department;
using Application.Services.Generic;

namespace Application.Services.Departments
{
    public interface IDepartment : IGenericService<DepartmentDTO,DepartmentCreateDTO ,DepartmentUpdateDTO>
    {

    }
}
