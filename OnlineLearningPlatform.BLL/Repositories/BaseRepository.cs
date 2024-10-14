using Microsoft.EntityFrameworkCore;
using OnlineLearningPlatform.BLL.Interfaces;
using OnlineLearningPlatform.DAL.Data;
using System.Linq.Expressions;

namespace OnlineLearningPlatform.BLL.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly AppDbContext _context;
        private readonly DbSet<T> _dbSet;

        public BaseRepository(AppDbContext _context)
        {
            this._context = _context;
            _dbSet = _context.Set<T>();
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<T>> GetAllAsync() =>  
            await _dbSet.AsNoTracking().ToListAsync();

        public async Task<T> GetByIdAsync(int id) =>
            await _dbSet.FindAsync(id);

        public async Task<T> FindByExpress(Expression<Func<T, bool>> Criteria) => 
            await _dbSet.SingleOrDefaultAsync(Criteria);

        public async Task<IEnumerable<T>> FindAllByExpress(Expression<Func<T, bool>> Criteria) =>
            await _dbSet.Where(Criteria).ToListAsync();

        public async Task<IEnumerable<T>> GetAllWithIncludeAsync(params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbSet;
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
            return await query.ToListAsync();
        }

        public async Task<T> GetFirstOrDefaultWithIncludeAsync(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbSet;
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
            return await query.FirstOrDefaultAsync(filter);
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> filter)
        {
            return await _dbSet.AnyAsync(filter);
        }

    }
}
