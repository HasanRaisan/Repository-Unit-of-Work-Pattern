
namespace Business.DTOs.Teaher
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
