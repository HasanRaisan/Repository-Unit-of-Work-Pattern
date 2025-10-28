using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTOs.Identity
{
    public class ApplicationRoleDTO
    {
        public string? Id { get; set; }  

        //[Required(ErrorMessage = "Role name is required.")]
        //[StringLength(50, MinimumLength = 2, ErrorMessage = "Role name must be between 2 and 50 characters.")]
        public string Name { get; set; } = string.Empty;

        //[StringLength(200, ErrorMessage = "Description cannot exceed 200 characters.")]
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
