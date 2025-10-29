

using Data.Data.Entities;
using Data.Repositories.Generic;

namespace Data.Repositories.Spesific
{
    public interface ITeacherRepository : IRepository<TeacherEntity>
    {
       Task<IEnumerable<TeacherEntity>> GetTeachersByDepartmentAsync(int Id);
    }
}
