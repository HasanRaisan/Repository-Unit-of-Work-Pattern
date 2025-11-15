using Application.DTOs.Department;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validation.Department
{
    public abstract class DepartmentBaseValidator<T> : AbstractValidator<T>
        where T : DepartmentBaseDTO
    {
        protected DepartmentBaseValidator()
        {
            RuleFor(d => d.Name)
                .NotEmpty().WithMessage("Department name is required.")
                .MaximumLength(100).WithMessage("Department name must not exceed 100 characters.");

            RuleFor(d => d.Description)
                .MaximumLength(500).WithMessage("Description cannot exceed 300 characters.");
        }
    }
}
