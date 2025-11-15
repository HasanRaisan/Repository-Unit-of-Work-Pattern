using Application.DTOs.Student;
using Application.DTOs.Teaher;
using Application.Services.Generic;

namespace Application.Services.Students
{
    public interface IStudent : IGenericService<StudentDTO,StudentCreateDTO,StudentUpdateDTO>
    {

    }
}