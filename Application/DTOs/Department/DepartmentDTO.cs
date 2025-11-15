using Application.DTOs.Teaher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Department
{
    public class DepartmentDTO : DepartmentBaseDTO
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public ICollection<TeacherDTO> Teachers { get; set; }
    }
}