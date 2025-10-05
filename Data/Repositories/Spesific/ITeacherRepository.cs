

using Data.Entities;
using Data.Repositories.Generic;

namespace Data.Repositories.Spesific
{
    public interface ITeacherRepository : IRepository<Teacher>
    {
       Task<IEnumerable<Teacher>> GetTeachersByDepartmentAsync(int Id);
    }
}
