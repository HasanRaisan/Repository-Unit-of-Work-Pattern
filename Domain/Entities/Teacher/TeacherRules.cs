

namespace Domain.Entities.Teacher
{
    public static class TeacherRules
    {
        public static IEnumerable<string> ValidateName(string name, decimal salary)
        {
            if (string.IsNullOrWhiteSpace(name))
                yield return "Teacher name is required.";

            if (!string.IsNullOrWhiteSpace(name) && name.Length > 100)
                yield return "Teacher name must be between 1 and 100 characters.";

            if (!string.IsNullOrWhiteSpace(name) && salary > 6000m && name.Length < 5)
            {
                yield return "For high salaries, the name must be at least 5 characters.";
            }
        }

        public static IEnumerable<string> ValidateSubject(string subject)
        {
            if (string.IsNullOrWhiteSpace(subject))
                yield return "Subject is required.";

            if (!string.IsNullOrWhiteSpace(subject) && subject.Length > 50)
                yield return "Subject maximum length is 50.";
        }

        public static IEnumerable<string> ValidateSalary(decimal salary)
        {
            if (salary <= 0)
                yield return "Teacher salary must be greater than zero.";
        }

        public static IEnumerable<string> ValidateDepartmentId(int departmentId)
        {
            if (departmentId <= 0)
                yield return "Department ID must be a valid positive integer.";
        }
        public static IEnumerable<string> ValidateId(int Id)
        {
            if (Id <= 0)
                yield return "Invalid ID value.";
        }
    }
}
