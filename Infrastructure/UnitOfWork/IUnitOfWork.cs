using Infrastructure.Data.Entities;
using Infrastructure.Repositories;
using Infrastructure.Repositories.ErrorLog;
using Infrastructure.Repositories.Generic;
using Infrastructure.Repositories.Teacher;


namespace Infrastructure.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<StudentEntity> Students { get; }
        IRepository<DepartmentEntity> Departments { get; }
        ITeacherRepository Teachers { get; }
        IRepository<ApplicationUserEntity> ApplicationUsers { get; }
        IRepository<ApplicationRoleEntity> ApplicationRoles { get; }
        IErrorLogRepository ErrorLoggers { get; }
        Task<int> SaveChangesAsync();
    }
}
