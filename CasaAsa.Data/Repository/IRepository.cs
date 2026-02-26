using CasaAsa.Data.Models;
using System.Linq.Expressions;

namespace CasaAsa.Data.Repository
{
    public interface IRepository<T> where T : class, IAuditEntity
    {
        Task<T?> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task RemoveAsync(T entity, bool isHardDelete = true);
        Task<int> SaveChangesAsync();
        Task<T?> FindFirstOrDefaultAsync(Expression<Func<T, bool>> predicate);
    }
}
