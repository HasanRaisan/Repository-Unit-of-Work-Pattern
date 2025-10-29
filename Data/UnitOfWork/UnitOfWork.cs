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
        public IRepository<ApplicationRoleEntity> ApplicationRoles { get; private set; }
        public IRepository<ApplicationUserEntity> ApplicationUsers { get; private set; }
        public UnitOfWork(AppDbContext context,
            IRepository<StudentEntity> studentRepository,
            ITeacherRepository teacherRepository,
            IRepository<ApplicationRoleEntity> roleRepository,
            IRepository<ApplicationUserEntity> userRepository)
        {
            _context = context;

            Students = studentRepository;
            Teachers = teacherRepository;
            ApplicationRoles = roleRepository;
            ApplicationUsers = userRepository;
        }
        public async Task<int> SaveChangesAsync() =>  await _context.SaveChangesAsync();
        public void Dispose() => _context.Dispose();
    }
}
