using DatabaseAccess.Entities;
using DatabaseAccess.Entities.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseAccess.DbWorker
{
    public abstract class GenericDbHandler<TEntity> : IGenericDbHandler<TEntity> where TEntity : BaseEntity
    {
        private readonly ApplicationContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public GenericDbHandler(ApplicationContext context)
        {
            _dbSet = context.Set<TEntity>();
            _context = context;
        }
        
        public Task Create(TEntity entity)
        {
            _dbSet.AddAsync(entity);
            return Save();
        }

        public Task Update(TEntity entity)
        {
            _dbSet.Update(entity);
            return Save();
        }

        public Task<TEntity> Get(Func<TEntity, bool> filter)
        {
            var result = _dbSet.AsNoTracking().Single(filter);
            return Task.FromResult(result);
        }

        public Task<TEntity[]> GetMany(Func<TEntity, bool> filters)
        {
            var result = _dbSet.AsNoTracking().Where(filters).ToArray();
            return Task.FromResult(result);
        }

        private Task Save()
        {
            return _context.SaveChangesAsync();
        }
    }
}
