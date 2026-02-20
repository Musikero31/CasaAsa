using CasaAsa.Data.Models;
using System.Linq.Expressions;

namespace CasaAsa.Data.Repository
{
    public interface IRepository<T> where T : class, IEntityDefault
    {
        Task<T?> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
        Task AddAsync(T entity);
        void Update(T entity);
        void Remove(T entity, bool isHardDelete = true);
        Task<int> SaveChangesAsync();
    }
}
