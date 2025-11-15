using Application.Services.Generic;
using Application.DTOs.Teaher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Results;

namespace Application.Services.Teachers
{
    public interface ITeacher : IGenericService<TeacherDTO, TeacherCreateDTO, TeacherUpdateDTO>
    {
        Task<Result<IEnumerable<TeacherDTO>>> GetTeachersByDepartmentAsync(int Id);

    }
}