using Domain.Results;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Core
{
    public class StudentDomain
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Age { get; set; }
        public int Grade { get; set; }

        public bool HasPassed => Grade >= 50;

        public Result ValidateBusinessRules()
        {
            var errors = new List<string>();

            if (Age < 5)
                errors.Add("Student age must be at least 5 years old.");

            if (Grade < 0 || Grade > 100)
                errors.Add("Grade must be between 0 and 100.");

            return errors.Count == 0
                ? Result.Success()
                : Result.Failure(errors);
        }
    }
}
