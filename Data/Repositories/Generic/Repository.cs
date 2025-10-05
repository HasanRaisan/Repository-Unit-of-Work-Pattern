using Microsoft.EntityFrameworkCore;

namespace Data.Repositories.Generic
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly AppDbContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<T?> GetByIdAsync(int id) => await _dbSet.FindAsync(id);

        public async Task<IEnumerable<T>> GetAllAsync() => await _dbSet.ToListAsync();

        public async Task AddAsync(T entity) => await _dbSet.AddAsync(entity);

        public async Task UpdateAsync(T entity) => await Task.Run(() => _dbSet.Update(entity));

        public async Task DeleteAsync(T entity) => await Task.Run(() => _dbSet.Remove(entity));

        /*
         
         there in no delete async or update async so we can't use async/await
         but we can use Task.Run to make it async
        
         */


    }
}
