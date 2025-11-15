using Domain.Entities.Teacher;
using Domain.Results;

namespace Domain.Entities.Teacher
{
    public class TeacherDomain
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;
        public decimal Salary { get; set; }
        public int DepartmentId { get; set; }


        private TeacherDomain(int id, string name, string subject, decimal salary, int departmentId)
        {
            Id = id;
            Name = name;
            Subject = subject;
            Salary = salary;
            DepartmentId = departmentId;
        }

        private static List<string> Validate(string name, string subject, decimal salary, int departmentId)
        {
            return TeacherRules.ValidateName(name, salary)
                .Concat(TeacherRules.ValidateSubject(subject))
                .Concat(TeacherRules.ValidateSalary(salary))
                .Concat(TeacherRules.ValidateDepartmentId(departmentId))
                .ToList();
        }

        public static Result<TeacherDomain> Create(string name, string subject, decimal salary, int departmentId)
        {
            var errors = Validate(name, subject, salary, departmentId);

            if (errors.Any())
                return Result<TeacherDomain>.Fail(errors);

            return Result<TeacherDomain>.Success(new TeacherDomain(0, name, subject, salary, departmentId));
        }

        public static Result<TeacherDomain> Update(int id, string name, string subject, decimal salary, int departmentId)
        {
            var errors = Validate(name, subject, salary, departmentId);

            errors.AddRange(TeacherRules.ValidateId(id));

            if (errors.Any())
                return Result<TeacherDomain>.Fail(errors);

            return Result<TeacherDomain>.Success(new TeacherDomain(id, name, subject, salary, departmentId));
        }


        //public Result ValidateBusinessRules()
        //{

        //    var errors = new List<string>();

        //    if (Subject == "Math" && Salary < 500)
        //        errors.Add("Math teachers must have salary above 500.");

        //    if (DepartmentId == 10 && Salary > 10000)
        //        errors.Add("Department 10 has a salary cap of 10,000.");

        //    return errors.Count == 0
        //        ? Result.Success()
        //        : Result.Failure(errors);
        //}
    }
}
