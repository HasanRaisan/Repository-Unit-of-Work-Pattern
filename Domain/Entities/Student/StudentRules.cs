using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Student
{
    public static class StudentRules
    {
        public static IEnumerable<string> ValidateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                yield return "Student name is required.";

            if (!string.IsNullOrWhiteSpace(name) && name.Length > 100)
                yield return "Student name must be between 1 and 100 characters.";
        }

        public static IEnumerable<string> ValidateAge(int age)
        {
            if (age < 1 || age > 120)
                yield return "Age must be between 1 and 120.";
        }

        public static IEnumerable<string> ValidateGrade(int grade)
        {
            if (grade < 1 || grade > 100)
                yield return "Grade must be between 1 and 100.";
        }
        public static IEnumerable<string> ValidateId(int Id)
        {
            if (Id <= 0)
                yield return "Invalid ID value.";
        }
    }
}
