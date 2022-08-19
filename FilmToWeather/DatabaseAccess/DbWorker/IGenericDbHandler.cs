using DatabaseAccess.Entities;
using DatabaseAccess.Entities.Abstractions;

namespace DatabaseAccess.DbWorker
{
    public interface IGenericDbHandler<TEntity> where TEntity : BaseEntity
    {
        public Task Create(TEntity entity);
        public Task Update(TEntity entity);
        Task<TEntity> Get(Func<TEntity, bool> filter);
        Task<TEntity[]> GetMany(Func<TEntity, bool> filters);
    }
}
