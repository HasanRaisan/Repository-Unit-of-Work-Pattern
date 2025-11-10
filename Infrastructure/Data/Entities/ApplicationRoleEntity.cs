using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Entities
{
    public class ApplicationRoleEntity : IdentityRole
    {
        [MaxLength(250)]
        public string? Description { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [MaxLength(150)]
        public string? CreatedBy { get; set; }
    }
}
