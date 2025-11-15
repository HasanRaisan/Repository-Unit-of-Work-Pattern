using Application.DTOs.Student;
using Application.DTOs.Teaher;
using Application.Services.Generic;

namespace Application.Services.Students
{
    public interface IStudentService : IGenericService<StudentDTO,StudentCreateDTO,StudentUpdateDTO>
    {

    }
}