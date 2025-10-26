using Data.Data;
using Data.Data.Entities;
using Data.Repositories.Generic;
using Data.Repositories.Spesific;


namespace Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        public IRepository<StudentEntity> Students { get; private set; }
        public ITeacherRepository Teachers { get; private set; }
        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            Students = new Repository<StudentEntity>(_context);
            Teachers = new TeacherRepository(_context);
        }
        public async Task<int> SaveChangesAsync() =>  await _context.SaveChangesAsync();
        public void Dispose() => _context.Dispose();
    }
}
