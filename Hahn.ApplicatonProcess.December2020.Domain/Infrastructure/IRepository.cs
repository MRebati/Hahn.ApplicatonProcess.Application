using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Hahn.ApplicationProcess.December2020.Data.Infrastructure
{
    public interface IRepository<TEntity> : IReadOnlyRepository<TEntity>
    {
        TEntity Create(TEntity entity);
        void Update(TEntity entity);
        void Update(TEntity entity, params Expression<Func<TEntity, object>>[] properties);
        void Delete(object id);
        void Delete(TEntity entity);
        void DeleteLogically(object id);
        void DeleteLogically(TEntity entity);
        void Save();
        Task SaveAsync();
    }
}