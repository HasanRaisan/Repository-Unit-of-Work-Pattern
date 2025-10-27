using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Domains.Core
{
    public class StudentDomain
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Age { get; set; }
        public int Grade { get; set; }
        public bool HasPassed => Grade >= 50;
    }
}
