using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Hahn.ApplicationProcess.December2020.Domain.ValueObjects
{
    public interface IPagedListRequest<TEntity>
    {
        int PageNumber { get; set; }

        int PageSize { get; set; }

        int TotalCount { get; set; }

        string Order { get; }

        string Filter { get; set; }

        IEnumerable<Expression<Func<TEntity, bool>>> ExpressionFilters { get; set; }

        string Includes { get; set; }

        void SetOrder(string property, string order);

        void SetFilters(Expression<Func<TEntity, bool>> filter, out Expression<Func<TEntity, bool>> expression);
    }
}