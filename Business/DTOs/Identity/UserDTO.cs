using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTOs.Identity
{
    public class UserDTO
    {
        //[Required(ErrorMessage = "User ID is required.")]
        public string Id { get; set; } = string.Empty;

        //[Required(ErrorMessage = "Full name is required.")]
        //[StringLength(100, MinimumLength = 3, ErrorMessage = "Full name must be between 3 and 100 characters.")]
        public string FullName { get; set; } = string.Empty;

        //[Required(ErrorMessage = "Email address is required.")]
        //[EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; } = string.Empty;

        //[Required(ErrorMessage = "Username is required.")]
        //[StringLength(50, MinimumLength = 3, ErrorMessage = "Username must be between 3 and 50 characters.")]
        public string UserName { get; set; } = string.Empty;

        public List<string> Roles { get; set; } = new();

        //[Required(ErrorMessage = "Account creation date is required.")]
        //[DataType(DataType.Date, ErrorMessage = "Invalid date format.")]
        public DateTime CreatedAt { get; set; }
    }
}