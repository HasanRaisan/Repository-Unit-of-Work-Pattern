using Business.Results;
using Business.Services.Generic;
using Business.DTOs.Teaher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Teachers
{
    public interface ITeacher : IGenericService<TeacherDTO>
    {
        Task<Result<IEnumerable<TeacherDTO>>> GetTeachersByDepartmentAsync(int Id);

    }
}
