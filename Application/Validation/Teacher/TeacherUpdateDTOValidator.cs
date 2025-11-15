using Application.DTOs.Teaher;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validation.Teacher
{
    public class TeacherUpdateDTOValidator : TeacherBaseValidator<TeacherUpdateDTO>
    {
        public TeacherUpdateDTOValidator()
        {
            RuleFor(t => t.Id)
                .GreaterThan(0).WithMessage("Teacher ID is required and must be positive.");
        }

    }
}
