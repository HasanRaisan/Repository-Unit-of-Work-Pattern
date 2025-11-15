using Application.DTOs.Student;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validation.Student
{
    public class StudentBaseValidator<T> : AbstractValidator<T> where T : StudentBaseDTO
    {
        protected StudentBaseValidator()
        {
            RuleFor(s => s.Name)
                .NotEmpty().WithMessage("Student name is required.")
                .Length(1, 100).WithMessage("Student name must be between 1 and 100 characters.");

            RuleFor(s => s.Age)
                .InclusiveBetween(1, 120).WithMessage("Age must be between 1 nnd 120.");

            RuleFor(s => s.Grade)
                .InclusiveBetween(1, 100).WithMessage("Grade must be between 1 and 12.");
        }
    }
}
