using Business.DTOs.Student;
using Business.DTOs.Teaher;
using FluentValidation;


namespace Business.Validation
{
    public class TeacherDTOValidator : AbstractValidator<TeacherDTO>
    {
        public TeacherDTOValidator()
        {
            RuleFor(teacher => teacher.Id)
                .GreaterThan(0).When(teacher => teacher.Id != 0).
                WithMessage("Teacher ID must be a positive number if provided."); ;

            RuleFor(teacher => teacher.Name)
                .NotEmpty().WithMessage("Teacher name is rquired.")
                .Length(1, 100).WithMessage("Teacher name must be between 1 and 100 characters.");
         
            RuleFor(teacher => teacher.Subject)
                .NotEmpty().WithMessage("Subject is required")
                .MaximumLength(50).WithMessage("Subject maximum lenght name is 50.");

            RuleFor(teacher => teacher.Salary)
            .GreaterThan(0).WithMessage("Teacher salary must be greater than zero.");

            RuleFor(teacher => teacher.DepartmentId)
            .NotNull().WithMessage("Department ID is required.")
            .GreaterThan(0).WithMessage("Department ID must be a valid positive integer.");

            // Testing
            When(teacher => teacher.Salary > 6000m, () =>
            {
                RuleFor(teacher => teacher.Name)
                .MinimumLength(5).WithMessage("For high salaries, the name must be at least 5 characters long.");
            }
            );
            
        }


    }
}
