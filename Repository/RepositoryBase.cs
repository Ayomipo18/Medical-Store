using Contracts;
using Microsoft.EntityFrameworkCore;
using Repository.DbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected AppDbContext _appDbContext;

        public RepositoryBase(AppDbContext appDbContext) => _appDbContext = appDbContext;

        public IQueryable<T> FindAll(bool trackChanges) =>
            !trackChanges ? _appDbContext.Set<T>().AsNoTracking() : _appDbContext.Set<T>();

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges) => !trackChanges
            ? _appDbContext.Set<T>().Where(expression).AsNoTracking()
            : _appDbContext.Set<T>().Where(expression);

        public void Create(T entity) => _appDbContext.Set<T>().Add(entity);


        public void Delete(T entity) => _appDbContext.Set<T>().Remove(entity);
        public virtual async Task AddAsync(T entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            await _appDbContext.Set<T>().AddAsync(entity);
        }

        public virtual async Task AddRangeAsync(IEnumerable<T> entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            await _appDbContext.Set<T>().AddRangeAsync(entity);
        }

        public virtual async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await _appDbContext.Set<T>().Where(predicate).ToListAsync();
        }

        public virtual bool Exists(Expression<Func<T, bool>> predicate)
        {
            return _appDbContext.Set<T>().Any(predicate);
        }

        public virtual async Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate)
        {
            return await _appDbContext.Set<T>().AnyAsync(predicate);
        }

        public virtual async Task<int> CountAsync(Expression<Func<T, bool>> predicate)
        {
            return await _appDbContext.Set<T>().CountAsync(predicate);
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _appDbContext.Set<T>().ToListAsync();
        }
        public IQueryable<T> QueryAll(Expression<Func<T, bool>> predicate = null)
        {
            return predicate == null ? _appDbContext.Set<T>() : _appDbContext.Set<T>().Where(predicate);

        }

        public virtual async Task<T> GetByIdAsync(Guid id)
        {
            var entity = await _appDbContext.Set<T>().FindAsync(id);

            if (entity == null) return null;

            return entity;
        }

        public virtual void Remove(T entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            _appDbContext.Set<T>().Remove(entity);
        }

        public virtual async Task<bool> SaveChangesAsync()
        {
            return (await _appDbContext.SaveChangesAsync() > 0);
        }

        public virtual void Update(T entity)
        {
            _appDbContext.Set<T>().Update(entity);
        }
        public virtual void Patch(T entity)
        {
            _appDbContext.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Patch(T entity, bool alreadyTracked)
        {
            if (alreadyTracked == false)
                _appDbContext.Entry(entity).State = EntityState.Modified;
        }
        public virtual Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> predicate)
        {
            return _appDbContext.Set<T>().SingleOrDefaultAsync(predicate);
        }

        public virtual Task<T> SingleOrDefaultAsyncNoTracking(Expression<Func<T, bool>> predicate)
        {
            return _appDbContext.Set<T>().AsNoTracking().SingleOrDefaultAsync(predicate);
        }

        public virtual Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate)
        {
            return _appDbContext.Set<T>().FirstOrDefaultAsync(predicate);
        }
        public virtual Task<T> FirstOrDefaultAsyncNoTracking(Expression<Func<T, bool>> predicate)
        {
            return _appDbContext.Set<T>().AsNoTracking().FirstOrDefaultAsync(predicate);
        }
        public virtual void UpdateRange(IEnumerable<T> entity)
        {
            _appDbContext.Set<T>().UpdateRange(entity);
        }
        public virtual async Task<T> FirstOrDefaultAsyncAndIgnoreQueryFilters(Expression<Func<T, bool>> expression)
        {
            var query = _appDbContext.Set<T>().AsQueryable();

            return await query.IgnoreQueryFilters().FirstOrDefaultAsync(expression);
        }
        public virtual IQueryable<T> Get(Expression<Func<T, bool>> predicate = null)
        {
            return predicate == null ? _appDbContext.Set<T>() : _appDbContext.Set<T>().Where(predicate);
        }
    }
}
