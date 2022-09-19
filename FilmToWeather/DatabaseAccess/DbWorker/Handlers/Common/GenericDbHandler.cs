using DatabaseAccess.Entities;
using DatabaseAccess.Entities.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseAccess.DbWorker.Handlers.Common
{
    public abstract class GenericDbHandler<TEntity> : IGenericDbHandler<TEntity> where TEntity : BaseEntity
    {
        private readonly DbSet<TEntity> _dbSet;

        protected GenericDbHandler(ApplicationContext context)
        {
            _dbSet = context.Set<TEntity>();
        }

        public async Task Create(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public Task Update(TEntity entity)
        {
            _dbSet.Update(entity);
            return Task.CompletedTask;
        }

        public Task<TEntity> GetOne(Expression<Func<TEntity, bool>> filter)
        {
            var result = _dbSet.Where(filter).First();
            if (result == null)
            {
                throw new InvalidOperationException($"{nameof(TEntity)} does not exist");
            }

            return Task.FromResult(result);
        }

        public Task<TEntity?> FindOne(Expression<Func<TEntity, bool>> filter,
            params Expression<Func<TEntity, object>>[] includeProperties)
        {
            var result = Include(includeProperties).SingleOrDefault(filter);

            return Task.FromResult(result);
        }

        public Task<TEntity[]> FindMany(Expression<Func<TEntity, bool>> filter,
            params Expression<Func<TEntity, object>>[] includeProperties)
        {
            var result = Include(includeProperties).Where(filter).ToArray();
            if (result == null)
            {
                return Task.FromResult<TEntity[]>(Array.Empty<TEntity>());
            }

            return Task.FromResult(result);
        }

        public Task<TEntity[]> GetMany(Expression<Func<TEntity, bool>> filter)
        {
            var result = _dbSet.Where(filter).ToArray();
            return Task.FromResult(result);
        }

        public Task<TEntity[]> GetMany(Expression<Func<TEntity, bool>> filter,
            params Expression<Func<TEntity, object>>[] includeProperties)
        {
            var result = Include(includeProperties).Where(filter).ToArray();
            return Task.FromResult(result);
        }

        private IQueryable<TEntity> Include(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            return includeProperties.Aggregate(_dbSet.AsNoTracking(), (current, property) => current.Include(property));
        }
    }
}
