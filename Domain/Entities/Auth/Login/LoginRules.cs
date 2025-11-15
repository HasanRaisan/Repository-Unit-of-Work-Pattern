using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Domain.Entities.Auth.Login
{
    public static class LoginRules
    {
        public static IEnumerable<string> ValidateEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                yield return "Email is required.";

            if (email != null)
            {
                // تعبير نمطي بسيط للتحقق من شكل البريد الإلكتروني
                string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
                if (!Regex.IsMatch(email, emailPattern))
                    yield return "Invalid email format.";
            }
        }

        public static IEnumerable<string> ValidatePassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                yield return "Password is required.";

            if (password != null)
            {
                if (password.Length < 6)
                    yield return "Password must be at least 6 characters long.";
            }
        }
    }
}
