using Domain.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Core
{
    public class DepartmentDomain
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }

        public ICollection<TeacherDomain> Teachers { get; set; }

        public Result ValidateBusinessRules()
        {

            // Apply Business Rules

            return  Result.Success();
        }
    }
}
