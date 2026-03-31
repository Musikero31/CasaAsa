using CasaAsa.Data.Database;
using CasaAsa.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CasaAsa.Data.Repository
{
    public class GenericRepository<T> : IRepository<T> where T : class, IAuditEntity
    {
        protected readonly CasaAsaDbContext _context;
        protected readonly DbSet<T> _dbSet;

        public GenericRepository(CasaAsaDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public virtual async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public virtual async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }

        public virtual async Task<T?> FindFirstOrDefaultAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.FirstOrDefaultAsync(predicate);
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public virtual async Task<T?> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public virtual async Task RemoveAsync(T entity, bool isHardDelete = true)
        {
            await Task.Run(() =>
            {
                if (isHardDelete)
                {
                    _dbSet.Remove(entity);
                }
                else
                {
                    entity.ActiveStatus = false;
                    _dbSet.Update(entity);
                }
            });
        }

        public virtual async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public virtual async Task UpdateAsync(T entity)
        {
            await Task.Run(() => _dbSet.Update(entity));
        }
    }
}
