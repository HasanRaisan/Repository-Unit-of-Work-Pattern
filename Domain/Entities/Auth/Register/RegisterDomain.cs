using Domain.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Auth.Register
{
    public class RegisterDomain
    {
        public string FullName { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string ConfirmPassword { get; set; } = string.Empty;


        private RegisterDomain(string fullName, string userName, string email, string password, string confirmPassword)
        {
            FullName = fullName;
            UserName = userName;
            Email = email;
            Password = password;
            ConfirmPassword = confirmPassword;
        }

        private static List<string> Validate(string fullName, string userName, string email, string password, string confirmPassword)
        {
            return RegisterRules.ValidateFullName(fullName)
                .Concat(RegisterRules.ValidateUserName(userName))
                .Concat(RegisterRules.ValidateEmail(email))
                .Concat(RegisterRules.ValidatePassword(password))
                .Concat(RegisterRules.ValidateConfirmPassword(password, confirmPassword))
                .ToList();
        }

        public static Result<RegisterDomain> Create(string fullName, string userName, string email, string password, string confirmPassword)
        {
            var errors = Validate(fullName, userName, email, password, confirmPassword);

            if (errors.Any())
                return Result<RegisterDomain>.Fail(errors);

            var newRegistration = new RegisterDomain(fullName, userName, email, password, confirmPassword);

            return Result<RegisterDomain>.Success(newRegistration);
        }

        //public ResultForAuth ValidateBusinessRules()
        //{
        //    var errors = new List<string>();

        //    if (string.IsNullOrWhiteSpace(FullName))
        //        errors.Add("Full name is required.");

        //    if (string.IsNullOrWhiteSpace(UserName))
        //        errors.Add("Username is required.");

        //    if (string.IsNullOrWhiteSpace(Email) || !Email.Contains("@"))
        //        errors.Add("A valid email address is required.");

        //    if (string.IsNullOrWhiteSpace(Password))
        //        errors.Add("Password is required.");
        //    else if (Password.Length < 8)
        //        errors.Add("Password must be at least 8 characters long.");

        //    if (Password != ConfirmPassword)
        //        errors.Add("Password and confirmation password do not match.");

        //    return errors.Count == 0
        //        ? ResultForAuth.Success()
        //        : ResultForAuth.Failure(errors);
        //}
    }
}
