using Application.DTOs.Teaher;
using FluentValidation;

namespace Application.Validation.Teacher
{
    // This abstract base validator provides common validation rules for all teacher DTOs.
    // It cannot be instantiated directly; it is meant to be inherited by specific validators
    // such as TeacherCreateDTOValidator and TeacherUpdateDTOValidator.
    //
    // The generic constraint (where T : TeacherBaseDTO) ensures that only DTOs derived from
    // TeacherBaseDTO can use this validator. This prevents invalid types from being passed
    // and keeps validation logic strongly typed.
    //
    // Using an abstract base class avoids code duplication, centralizes shared validation rules,
    // and allows each derived validator to add its own specific rules when needed.

    public abstract class TeacherBaseValidator<T> : AbstractValidator<T> where T : TeacherBaseDTO
    {
        protected TeacherBaseValidator()
        {
            RuleFor(t => t.Name)
            .NotEmpty().WithMessage("Teacher name is required.")
            .Length(1, 100).WithMessage("Teacher name must be between 1 and 100 characters.");

            RuleFor(t => t.Subject)
                .NotEmpty().WithMessage("Subject is required.")
                .MaximumLength(50).WithMessage("Subject maximum length is 50.");

            RuleFor(t => t.Salary)
                .GreaterThan(0).WithMessage("Teacher salary must be greater than zero.");

            RuleFor(t => t.DepartmentId)
                .GreaterThan(0).WithMessage("Department ID must be a valid positive integer.");

            // Conditional rule
            When(t => t.Salary > 6000m, () =>
            {
                RuleFor(t => t.Name)
                    .MinimumLength(5).WithMessage("For high salaries, the name must be at least 5 characters.");
            });

        }
    }
}
