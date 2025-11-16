using Infrastructure.Data;
using Infrastructure.Data.Entities;
using Infrastructure.Repositories.ErrorLog;
using Infrastructure.Repositories.Generic;
using Infrastructure.Repositories.Teacher;


namespace Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        public IRepository<StudentEntity> Students { get; private set; }
        public ITeacherRepository Teachers { get; private set; }
        public IRepository<ApplicationRoleEntity> ApplicationRoles { get; private set; }
        public IRepository<ApplicationUserEntity> ApplicationUsers { get; private set; }
        public IRepository<DepartmentEntity> Departments {  get; private set; }
        public IErrorLogRepository ErrorLoggers { get; private set; }

        public UnitOfWork(AppDbContext context,
            IRepository<StudentEntity> studentRepository,
            IRepository<DepartmentEntity> departments,
            ITeacherRepository teacherRepository,
            IErrorLogRepository errorLoggers,
            IRepository<ApplicationRoleEntity> roleRepository,
            IRepository<ApplicationUserEntity> userRepository)
        {
            _context = context;
            ErrorLoggers = errorLoggers;
            Students = studentRepository;
            Departments = departments;
            Teachers = teacherRepository;
            ApplicationRoles = roleRepository;
            ApplicationUsers = userRepository;
        }

        public async Task<int> SaveChangesAsync() =>  await _context.SaveChangesAsync();
        public void Dispose() => _context.Dispose();

        public void Clear() => _context.ChangeTracker.Clear();
    }
}
