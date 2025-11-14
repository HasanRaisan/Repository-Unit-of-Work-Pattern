using Application.DTOs.Teaher;
using Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Department
{
    public class DepartmentDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public ICollection<TeacherDTO> Teachers { get; set; }
    }
}
