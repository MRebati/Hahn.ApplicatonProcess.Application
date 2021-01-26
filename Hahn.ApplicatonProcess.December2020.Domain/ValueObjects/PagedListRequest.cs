using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Hahn.ApplicationProcess.December2020.Domain.Extensions;

namespace Hahn.ApplicationProcess.December2020.Domain.ValueObjects
{
    public class PagedListRequest<TEntity> : IPagedListRequest<TEntity>
    {
        public PagedListRequest()
        {
            this.ExpressionFilters = new List<Expression<Func<TEntity, bool>>>();
        }

        public int PageNumber { get; set; }

        public int PageSize { get; set; }

        public int TotalCount { get; set; }

        public string Order { get; private set; }

        public string Filter { get; set; }

        public IEnumerable<Expression<Func<TEntity, bool>>> ExpressionFilters { get; set; }

        public string Includes { get; set; }

        public void SetOrder(string property, string order)
        {
            if (!string.IsNullOrWhiteSpace(property) && !string.IsNullOrWhiteSpace(order))
                Order = property + " " + order.ToUpper();
        }

        public IPagedListRequest<TEntity> Create(int? pageSize, int? pageNumber, string order, string orderBy)
        {
            SetOrder(orderBy, order);
            PageNumber = pageNumber ?? 1;
            PageSize = pageSize ?? 20;
            return this;
        }

        public virtual void SetFilters(Expression<Func<TEntity, bool>> filter, out Expression<Func<TEntity, bool>> expression)
        {
            foreach (var expressionItem in ExpressionFilters)
            {
                filter = filter == null ? expressionItem : filter.AndAlso(expressionItem);
            }
            expression = filter;
        }
    }
}