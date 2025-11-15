using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Department
{
    public abstract class DepartmentRules
    {
        public static IEnumerable<string> ValidateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                yield return "Department name is required.";

            if (!string.IsNullOrWhiteSpace(name) && name.Length > 100)
                yield return "Department name must not exceed 100 characters.";
        }

        public static IEnumerable<string> ValidateDescription(string description)
        {
            if (!string.IsNullOrWhiteSpace(description) && description.Length > 500)
                yield return "Description cannot exceed 500 characters.";
        }

        public static IEnumerable<string> ValidateId(int Id)
        {
            if (Id <= 0)
                yield return "Invalid ID value.";
        }
    }
}
