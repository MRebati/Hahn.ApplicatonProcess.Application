using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Hahn.ApplicationProcess.December2020.Domain.ValueObjects;

namespace Hahn.ApplicationProcess.December2020.Data.Infrastructure
{
    public interface IReadOnlyRepository<TEntity>
    {
        IQueryable<TEntity> GetQueryable(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = null,
            int? skip = null,
            int? take = null);

        IEnumerable<TEntity> GetAll(
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = null,
            int? skip = null,
            int? take = null);

        Task<IEnumerable<TEntity>> GetAllAsync(
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = null,
            int? skip = null,
            int? take = null);

        IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = null,
            int? skip = null,
            int? take = null);

        IPagedListResult<TEntity> GetAsPagedList(
            IPagedListRequest<TEntity> pagedListRequest,
            Expression<Func<TEntity, bool>> filter = null,
            string includeProperties = null
        );

        Task<IPagedListResult<TEntity>> GetAsPagedListAsync(
            IPagedListRequest<TEntity> pagedListRequest,
            Expression<Func<TEntity, bool>> filter = null,
            string includeProperties = null
        );

        Task<IEnumerable<TEntity>> GetAsync(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = null,
            int? skip = null,
            int? take = null);

        TEntity GetOne(
            Expression<Func<TEntity, bool>> filter = null,
            string includeProperties = null);

        Task<TEntity> GetOneAsync(
            Expression<Func<TEntity, bool>> filter = null,
            string includeProperties = null);

        TEntity GetFirst(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = null);

        Task<TEntity> GetFirstAsync(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = null);

        TEntity GetById(object id);

        Task<TEntity> GetByIdAsync(object id);

        int GetCount(Expression<Func<TEntity, bool>> filter = null);

        Task<int> GetCountAsync(Expression<Func<TEntity, bool>> filter = null);

        bool GetExists(Expression<Func<TEntity, bool>> filter = null);

        Task<bool> GetExistsAsync(Expression<Func<TEntity, bool>> filter = null);
    }
}