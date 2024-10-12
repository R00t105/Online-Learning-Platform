
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
    }
}
