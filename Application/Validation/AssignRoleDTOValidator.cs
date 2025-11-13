using Application.Constants;
using Application.DTOs.Identity;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validation
{
    public class AssignRoleDTOValidator : AbstractValidator<AssignRoleDTO>
    {
        public AssignRoleDTOValidator()
        {
            RuleFor(dto => dto.UserId)
                .NotEmpty()
                .WithMessage("User ID is required for role assignment.");

            RuleFor(dto => dto.Role)
                .NotEmpty()
                .WithMessage("The role name is required for assignment.")
                .Length(3, 50)
                .WithMessage("Role name must be between 3 and 50 characters.");


            RuleFor(dto => dto.Role)
                .Must(BeAValidRole)
                .WithMessage(dto => $"{dto.Role} is not a valid role name in the system.");
        }

        private bool BeAValidRole(string role)
        {
            if (string.IsNullOrEmpty(role))
                return true; // already handeled by NotEmpty()

            var validRoles = new List<string> {
                RoleConstants.Admin,
                RoleConstants.Teacher,
                RoleConstants.Student
            };

            return validRoles.Contains(role, StringComparer.OrdinalIgnoreCase);
        }

    }
}
