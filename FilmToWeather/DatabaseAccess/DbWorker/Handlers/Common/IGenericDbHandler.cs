using DatabaseAccess.Entities;
using DatabaseAccess.Entities.Abstractions;
using System.Linq.Expressions;

namespace DatabaseAccess.DbWorker.Handlers.Common
{
    public interface IGenericDbHandler<TEntity> where TEntity : BaseEntity
    {
        public Task Create(TEntity entity);
        public Task Update(TEntity entity);
        Task<TEntity> GetOne(Expression<Func<TEntity, bool>> filter);
        Task<TEntity> GetOne(Expression<Func<TEntity, bool>> filter,
            params Expression<Func<TEntity, object>>[] includeProperties);
        Task<TEntity[]> GetMany(Expression<Func<TEntity, bool>> filter);
        Task<TEntity[]> GetMany(Expression<Func<TEntity, bool>> filter,
            params Expression<Func<TEntity, object>>[] includeProperties);
        Task<TEntity[]> FindMany(Expression<Func<TEntity, bool>> filter,
            params Expression<Func<TEntity, object>>[] includeProperties);
    }
}
