using System.ComponentModel.DataAnnotations;

namespace Business.DTOs.Student
{
    public class StudentDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Age { get; set; }
        public int Grade { get; set; }
    }
}