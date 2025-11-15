using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Teaher
{
    public class TeacherBaseDTO
    {
        public string Name { get; set; }
        public string Subject { get; set; }
        public decimal Salary { get; set; }
        public int DepartmentId { get; set; }
    }
}

