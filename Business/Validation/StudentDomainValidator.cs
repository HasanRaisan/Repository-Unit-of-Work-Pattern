using Business.Domains.Core;
using FluentValidation;

namespace Business.Validation
{
    public class StudentDomainValidator : AbstractValidator<StudentDomain>
    {
        public StudentDomainValidator()
        {

            RuleFor(student => student.Id)
                .GreaterThan(0).When(student => student.Id != 0) 
                .WithMessage("Student ID must be greater than zero for existing records.");

            RuleFor(student => student.Name)
                .NotEmpty().WithMessage("Student name is required.")
                .Length(1, 100).WithMessage("Student name length must be between 1 and 100 characters.");

            RuleFor(student => student.Age)
                .InclusiveBetween(1, 120).WithMessage("Age must be between 1 and 120");


            RuleFor(student => student.Grade)
                .InclusiveBetween(0, 20).WithMessage("Grade must be between 0 and 20.");
        }
    }
}
