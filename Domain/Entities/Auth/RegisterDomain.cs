using Domain.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Auth
{
    public class RegisterDomain
    {
        public string FullName { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string ConfirmPassword { get; set; } = string.Empty;

        public Result ValidateBusinessRules()
        {
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(FullName))
                errors.Add("Full name is required.");

            if (string.IsNullOrWhiteSpace(UserName))
                errors.Add("Username is required.");

            if (string.IsNullOrWhiteSpace(Email) || !Email.Contains("@"))
                errors.Add("A valid email address is required.");

            if (string.IsNullOrWhiteSpace(Password))
                errors.Add("Password is required.");
            else if (Password.Length < 8)
                errors.Add("Password must be at least 8 characters long.");

            if (Password != ConfirmPassword)
                errors.Add("Password and confirmation password do not match.");

            return errors.Count == 0
                ? Result.Success()
                : Result.Failure(errors);
        }
    }
}
