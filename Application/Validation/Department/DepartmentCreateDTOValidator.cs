using Application.DTOs.Department;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validation.Department
{
    public class DepartmentCreateDTOValidator : DepartmentBaseValidator<DepartmentCreateDTO>
    {
        public DepartmentCreateDTOValidator()
        {
            // Additional rules? → Not required now
        }
    }
}
