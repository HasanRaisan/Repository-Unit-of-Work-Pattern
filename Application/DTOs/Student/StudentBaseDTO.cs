using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Student
{
    public class StudentBaseDTO
    {
        public string Name { get; set; } = string.Empty;
        public int Age { get; set; }
        public int Grade { get; set; }
    }
}
