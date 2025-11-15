using Application.DTOs.Student;
using FluentValidation;

namespace Application.Validation.Student
{
    public class StudentCreateDTOValidator : StudentBaseValidator<StudentCreateDTO>
    {
        public StudentCreateDTOValidator()
        {
            // Any additional rules for creation can go here
        }
    }
}
