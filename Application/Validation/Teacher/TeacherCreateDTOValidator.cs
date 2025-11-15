using Application.DTOs.Student;
using Application.DTOs.Teaher;
using FluentValidation;


namespace Application.Validation.Teacher
{
    public class TeacherCreateDTOValidator : TeacherBaseValidator<TeacherCreateDTO>
    {
        public TeacherCreateDTOValidator()
        {

        }
    }
}
