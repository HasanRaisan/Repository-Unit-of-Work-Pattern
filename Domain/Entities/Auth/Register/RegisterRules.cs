using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Domain.Entities.Auth.Register
{
    public static class RegisterRules
    {
        public static IEnumerable<string> ValidateFullName(string fullName)
        {
            if (string.IsNullOrWhiteSpace(fullName))
                yield return "Full Name is required.";

            if (fullName != null)
            {
                if (fullName.Length < 3)
                    yield return "Full Name must be at least 3 characters long.";

                if (fullName.Length > 200)
                    yield return "Full Name cannot exceed 200 characters.";
            }
        }

        public static IEnumerable<string> ValidateUserName(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName))
                yield return "User Name is required.";

            if (userName != null)
            {
                if (userName.Length < 2) 
                    yield return "User Name must be at least 2 characters long.";

                if (userName.Length > 50)
                    yield return "User Name cannot exceed 50 characters.";
            }
        }

        public static IEnumerable<string> ValidateEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                yield return "Email address is required.";

            if (email != null)
            {
                string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
                if (!Regex.IsMatch(email, emailPattern))
                    yield return "A valid email address is required.";
            }
        }

        public static IEnumerable<string> ValidatePassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                yield return "Password is required.";

            if (password != null)
            {
                if (password.Length < 8)
                    yield return "Password must be at least 8 characters long.";

                if (!Regex.IsMatch(password, "[A-Z]"))
                    yield return "Password must contain at least one uppercase letter.";

                if (!Regex.IsMatch(password, "[a-z]"))
                    yield return "Password must contain at least one lowercase letter.";

                if (!Regex.IsMatch(password, "[0-9]"))
                    yield return "Password must contain at least one number.";
            }
        }

        public static IEnumerable<string> ValidateConfirmPassword(string password, string confirmPassword)
        {
            if (string.IsNullOrWhiteSpace(confirmPassword))
                yield return "Confirmation Password is required.";

            if (password != null && confirmPassword != null && password != confirmPassword)
                yield return "The password and confirmation password do not match.";
        }
    }
}
