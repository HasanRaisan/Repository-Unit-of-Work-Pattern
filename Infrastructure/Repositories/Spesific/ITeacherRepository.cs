

using Infrastructure.Data.Entities;
using Infrastructure.Repositories.Generic;

namespace Infrastructure.Repositories.Spesific
{
    public interface ITeacherRepository : IRepository<TeacherEntity>
    {
       Task<IEnumerable<TeacherEntity>> GetTeachersByDepartmentAsync(int Id);
    }
}
