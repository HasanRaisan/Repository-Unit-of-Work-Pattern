
using Domain.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Department
{
    public class DepartmentDomain
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        private DepartmentDomain(int id, string name, string description)
        {
            Id = id;
            Name = name;
            Description = description;
        }

        private static List<string> Validate(string name, string description)
        {
            return DepartmentRules
                .ValidateName(name)
                .Concat(DepartmentRules.ValidateDescription(description))
                .ToList();
        }

        public static Result<DepartmentDomain> Create(string name, string description)
        {
            var errors = Validate(name, description);

            if (errors.Any())
                return Result< DepartmentDomain>.Fail(errors);

            return Result<DepartmentDomain>.Success(new DepartmentDomain(0, name, description));
        }

        public static Result<DepartmentDomain> Update(int id, string name, string description)
        {
            var errors = Validate(name, description);
            errors.Concat(DepartmentRules.ValidateId(id));

            if (errors.Any())
                return Result<DepartmentDomain>.Fail(errors);

            return Result<DepartmentDomain>.Success(new DepartmentDomain(id, name, description));
        }


        //public Result ValidateBusinessRules()
        //{

        //    // Apply Business Rules

        //    return  Result.Success();
        //}
    }
}
