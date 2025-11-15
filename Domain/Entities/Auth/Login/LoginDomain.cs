using Domain.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Auth.Login
{
    public class LoginDomain
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        private LoginDomain(string email, string password) {
            this.Password = password;
            this.Email = email;
        }

        private static List<string> Validate(string email, string password)
        {
          return  LoginRules.ValidateEmail(email).
               Concat(LoginRules.ValidatePassword(password)).ToList();
        }
        public static Result<LoginDomain> Create(string email, string password)
        {
            var errors = Validate(email, password);
            if (errors.Any())
                return Result<LoginDomain>.Fail(errors);
            return Result<LoginDomain>.Success(new LoginDomain(email, password));
        }



        //public ResultForAuth ValidateBusinessRules()
        //{
        //    var errors = new List<string>();


        //    if (string.IsNullOrWhiteSpace(Email) || !Email.Contains("@"))
        //        errors.Add("A valid email address is required.");

        //    if (string.IsNullOrWhiteSpace(Password))
        //        errors.Add("Password is required.");
        //    else if (Password.Length < 8)
        //        errors.Add("Password must be at least 8 characters long.");

        //    return errors.Count == 0
        //        ? ResultForAuth.Success()
        //        : ResultForAuth.Failure(errors);
        //}
    }
}
