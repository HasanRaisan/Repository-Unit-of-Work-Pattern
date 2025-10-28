using System.ComponentModel.DataAnnotations;

namespace Clean_Three_Tier_First.DTOs.Teaher
{
    using System.ComponentModel.DataAnnotations;

    public class TeacherDTO
    {
        //[Required(ErrorMessage = "Teacher ID is required.")]
        //[Range(1, int.MaxValue, ErrorMessage = "Teacher ID must be a positive number.")]
        public int Id { get; set; }

        //[Required(ErrorMessage = "Teacher name is required.")]
        //[MinLength(2, ErrorMessage = "Teacher name must be at least 2 characters long.")]
        //[MaxLength(100, ErrorMessage = "Teacher name cannot exceed 100 characters.")]
        public string Name { get; set; } = string.Empty;

        //[Required(ErrorMessage = "Subject is required.")]
        //[MinLength(2, ErrorMessage = "Subject name must be at least 2 characters long.")]
        //[MaxLength(100, ErrorMessage = "Subject name cannot exceed 100 characters.")]
        public string Subject { get; set; } = string.Empty;

        //[Required(ErrorMessage = "Salary is required.")]
        public decimal Salary { get; set; }

        //[Required(ErrorMessage = "Department ID is required.")]
        //[Range(1, int.MaxValue, ErrorMessage = "Department ID must be a positive number.")]
        public int DepartmentId { get; set; }
    }

}
