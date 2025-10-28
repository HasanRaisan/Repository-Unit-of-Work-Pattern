using System.ComponentModel.DataAnnotations;

namespace Business.DTOs.Student
{
    public class StudentDTO
    {
        //[Required(ErrorMessage = "Student ID is required.")]
        //[Range(1, int.MaxValue, ErrorMessage = "Student ID must be a positive number.")]
        public int Id { get; set; }

        //[Required(ErrorMessage = "Student name is required.")]
        //[StringLength(100, MinimumLength = 1, ErrorMessage = "Student name must be between 1 and 100 characters.")]
        public string Name { get; set; } = string.Empty;

        //[Required(ErrorMessage = "Age is required.")]
        //[Range(1, 20, ErrorMessage = "Age must be between 1 and 20.")]
        public int Age { get; set; }

        //[Required(ErrorMessage = "Grade is required.")]
        //[Range(0, 100, ErrorMessage = "Grade must be between 0 and 100.")]
        public int Grade { get; set; }
    }
}