using Domain.Entities.Student;
using Domain.Results;

namespace Domain.Entities.Student
{
    public class StudentDomain
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Age { get; set; }
        public int Grade { get; set; }

        private StudentDomain(int id, string name, int age, int grade)
        {
            Id = id;
            Name = name;
            Age = age;
            Grade = grade;
        }

        private static List<string> Validate(string name, int age, int grade)
        {
            return StudentRules
                .ValidateName(name)
                .Concat(StudentRules.ValidateAge(age))
                .Concat(StudentRules.ValidateGrade(grade))
                .ToList();
        }

        public static Result<StudentDomain> Create(string name, int age, int grade)
        {
            var errors = Validate(name, age, grade);

            if (errors.Any())
                return Result<StudentDomain>.Fail(errors);

            return Result<StudentDomain>.Success(new StudentDomain(0, name, age, grade));
        }

        public static Result<StudentDomain> Update(int id, string name, int age, int grade)
        {
            var errors = Validate(name, age, grade);

            errors.AddRange(StudentRules.ValidateId(id));

            if (errors.Any())
                return Result<StudentDomain>.Fail(errors);

            return Result<StudentDomain>.Success(new StudentDomain(id, name, age, grade));
        }



        //public Result ValidateBusinessRules()
        //{
        //    var errors = new List<string>();

        //    if (Age < 5)
        //        errors.Add("Student age must be at least 5 years old.");

        //    if (Grade < 0 || Grade > 100)
        //        errors.Add("Grade must be between 0 and 100.");

        //    return errors.Count == 0
        //        ? Result.Success()
        //        : Result.Failure(errors);
        //}
    }
}
