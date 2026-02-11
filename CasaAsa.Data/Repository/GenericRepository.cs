using CasaAsa.Core.Abstraction;
using CasaAsa.Data.Database;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CasaAsa.Data.Repository
{
    public class GenericRepository<T> : IRepository<T> where T : class
    {
        protected readonly CasaAsaDbContext _context;
        protected readonly DbSet<T> _dbSet;
        private readonly ICurrentUserService _currentUser;

        public GenericRepository(CasaAsaDbContext context, ICurrentUserService currentUser)
        {
            _context = context;
            _dbSet = _context.Set<T>();
            _currentUser = currentUser;
        }

        public virtual async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public virtual async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public virtual async Task<T?> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public virtual void Remove(T entity)
        {
            _dbSet.Remove(entity);
        }

        public virtual async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public virtual void Update(T entity)
        {
            _dbSet.Update(entity);
        }
    }
}
