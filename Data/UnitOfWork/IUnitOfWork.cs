using Data.Entities;
using Data.Repositories.Generic;
using Data.Repositories.Spesific;


namespace Data.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<StudentEntity> Students { get; }
        ITeacherRepository Teachers { get; }

        Task<int> SaveChangesAsync();
    }
}
