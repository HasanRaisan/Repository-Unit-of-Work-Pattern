using Domain.Results;

namespace Domain.Entities.Core
{
    public class TeacherDomain
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;
        public decimal Salary { get; set; }
        public int DepartmentId { get; set; }

        public Result ValidateBusinessRules()
        {
            
            var errors = new List<string>();

            if (Subject == "Math" && Salary < 500)
                errors.Add("Math teachers must have salary above 500.");

            if (DepartmentId == 10 && Salary > 10000)
                errors.Add("Department 10 has a salary cap of 10,000.");

            return errors.Count == 0
                ? Result.Success()
                : Result.Failure(errors);
        }
    }
}
