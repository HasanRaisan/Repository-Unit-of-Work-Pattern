using Infrastructure.Data;
using Infrastructure.Data.Entities;
using Infrastructure.Repositories.Generic;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Teacher
{

    public class TeacherRepository : Repository<TeacherEntity>, ITeacherRepository
    {
        public TeacherRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<TeacherEntity>> GetTeachersByDepartmentAsync(int DepartmentId)
        {
            return await _context.Teachers
                                 .Where(t => t.DepartmentId == DepartmentId)
                                 .ToListAsync();
        }
    }

}
