using System.ComponentModel.DataAnnotations;

namespace Data.Data.Entities
{
    public class Student
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;
        [Range(1, 120)]
        public int Age { get; set; }
        [Range(0, 100)]
        public int Grade { get; set; }
    }
}
