using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Ordering.Application.Contracts.Persistence;
using Ordering.Domain.Common;
using Ordering.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.Repositories
{
    public class BaseRepository<T> : IAsynRepository<T> where T : EntityBase
    {
        protected readonly OrderContext _BaseDBContext;

        public BaseRepository(OrderContext baseDBContext)
        {
            _BaseDBContext = baseDBContext;
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _BaseDBContext.Set<T>().ToListAsync();
        }
        public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate)
        {
            return await _BaseDBContext.Set<T>().Where(predicate).ToArrayAsync();
        }

        public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeString = null, bool disableTracking = true)
        {

            IQueryable<T> query = _BaseDBContext.Set<T>();
            if (disableTracking) query = query.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(includeString)) query = query.Include(includeString);

            if (predicate != null) query = query.Where(predicate);

            if (orderBy != null)
                return await orderBy(query).ToListAsync();
            return await query.ToListAsync();
        }

        public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, List<Expression<Func<T, object>>> includes = null, bool disableTracking = true)
        {
            IQueryable<T> query = _BaseDBContext.Set<T>();
            if (disableTracking) query = query.AsNoTracking();

            if (includes != null) query = includes.Aggregate(query, (current, include) => current.Include(include));

            if (predicate != null) query = query.Where(predicate);

            if (orderBy != null)
                return await orderBy(query).ToListAsync();

            return await query.ToListAsync();

        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _BaseDBContext.Set<T>().FindAsync(id);
        }


        public async Task<T> AddAsync(T entity)
        {
            _BaseDBContext.Set<T>().Add(entity);

            await _BaseDBContext.SaveChangesAsync();

            return entity;
        }

        public async Task DeleteAsync(T entity)
        {
            _BaseDBContext.Entry<T>(entity).State = EntityState.Modified;

            await _BaseDBContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _BaseDBContext.Set<T>().Remove(entity);

            await _BaseDBContext.SaveChangesAsync();
        }
    }
}
