using FluentValidation;
using Application.DTOs.Student;

namespace Application.Validation.Student
{
    public class StudentUpdateDTOValidator : StudentBaseValidator<StudentUpdateDTO>
    {
        public StudentUpdateDTOValidator() 
        {
            RuleFor(s => s.Id)
           .GreaterThan(0).WithMessage("Student ID is required and must be positive.");
        }

    }
}
