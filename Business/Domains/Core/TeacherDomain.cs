using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Domains.Core
{
    public class TeacherDomain
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;
        public decimal Salary { get; set; }
        public int DepartmentId { get; set; }
    }
}
