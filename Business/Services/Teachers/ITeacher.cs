using Business.Domains.Core;
using Business.Result;
using Business.Services.Generic;
using Clean_Three_Tier_First.DTOs.Teaher;
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
