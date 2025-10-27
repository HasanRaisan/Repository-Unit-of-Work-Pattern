using Data.Data.Entities;
using Data.Repositories.Generic;
using Data.Repositories.Spesific;


namespace Data.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Student> Students { get; }
        ITeacherRepository Teachers { get; }
        IRepository<ApplicationUser> ApplicationUsers { get; }
        IRepository<ApplicationRole> ApplicationRoles { get; }

        Task<int> SaveChangesAsync();
    }
}
