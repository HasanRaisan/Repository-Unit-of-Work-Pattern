using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Entities
{
    public class TeacherEntity
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
        [ForeignKey("Department")]  // As long as you are using DepartmentId and Department, you can safely delete [Foreign Key(Department)] while preserving the relationship.
        public int DepartmentId { get; set; }

        public virtual DepartmentEntity Department { get; set; } = null!;
    }
}
