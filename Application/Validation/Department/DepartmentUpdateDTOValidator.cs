using Application.DTOs.Department;
using FluentValidation;

namespace Application.Validation.Department
{
    public class DepartmentUpdateDTOValidator : DepartmentBaseValidator<DepartmentUpdateDTO>
    {
        public DepartmentUpdateDTOValidator()
        {
            RuleFor(d => d.Id)
                .GreaterThan(0).WithMessage("Department ID must be a positive number.");
        }
    }
}
