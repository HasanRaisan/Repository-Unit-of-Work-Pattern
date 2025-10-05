using Data.Entities;
using Data.Repositories.Generic;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.Spesific
{

    public class TeacherRepository : Repository<Teacher>, ITeacherRepository
    {
        public TeacherRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<Teacher>> GetTeachersByDepartmentAsync(int DepartmentId)
        {
            return await _context.Teachers
                                 .Where(t => t.DepartmentId == DepartmentId)
                                 .ToListAsync();
        }
    }

}
