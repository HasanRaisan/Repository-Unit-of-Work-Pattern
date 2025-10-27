using System.ComponentModel.DataAnnotations;

namespace Clean_Three_Tier_First.DTOs.Teaher
{
    public class TeacherDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;
        public decimal Salary { get; set; }
        public int DepartmentId { get; set; }
    }
}
