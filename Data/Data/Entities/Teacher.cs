using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Data.Entities
{
    public class Teacher
    {
        [Key]
        public int Id { get; set; }

        [Required] 
        [MaxLength(100)] 
        public string Name { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string Subject { get; set; } = string.Empty;

        [Column(TypeName = "decimal(18,2)")] 
        [Range(0, double.MaxValue)]
        public decimal Salary { get; set; }

        [Required]
        public int DepartmentId { get; set; }
    }
}
