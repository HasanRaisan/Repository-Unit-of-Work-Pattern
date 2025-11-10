using Infrastructure.Data.Entities;
using Infrastructure.Repositories.Generic;
using Infrastructure.Repositories.Spesific;


namespace Infrastructure.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<StudentEntity> Students { get; }
        ITeacherRepository Teachers { get; }
        IRepository<ApplicationUserEntity> ApplicationUsers { get; }
        IRepository<ApplicationRoleEntity> ApplicationRoles { get; }

        Task<int> SaveChangesAsync();
    }
}
