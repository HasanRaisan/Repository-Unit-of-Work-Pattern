using Domain.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Auth
{
    public class LoginDomain
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        // ✅ تحقق من قواعد تسجيل الدخول
        public Result ValidateBusinessRules()
        {
            var errors = new List<string>();


            if (string.IsNullOrWhiteSpace(Email) || !Email.Contains("@"))
                errors.Add("A valid email address is required.");

            if (string.IsNullOrWhiteSpace(Password))
                errors.Add("Password is required.");
            else if (Password.Length < 8)
                errors.Add("Password must be at least 8 characters long.");

            return errors.Count == 0
                ? Result.Success()
                : Result.Failure(errors);
        }
    }
}
