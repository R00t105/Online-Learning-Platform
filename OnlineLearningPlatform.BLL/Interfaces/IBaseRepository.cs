
using System.Linq.Expressions;

namespace OnlineLearningPlatform.BLL.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task RemoveAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task<T> FindByExpress(Expression<Func<T, bool>> Criteria);
        Task<IEnumerable<T>> FindAllByExpress(Expression<Func<T, bool>> Criteria);
        Task<IEnumerable<T>> GetAllWithIncludeAsync(params Expression<Func<T, object>>[] includes);
        Task<T> GetFirstOrDefaultWithIncludeAsync(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] includes);
        Task<bool> AnyAsync(Expression<Func<T, bool>> filter);
    }
}
