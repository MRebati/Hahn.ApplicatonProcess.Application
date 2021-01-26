using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Hahn.ApplicationProcess.December2020.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Hahn.ApplicationProcess.December2020.Domain.Extensions
{
    public static class ExpressionExtension
    {
        public static Expression<Func<T, bool>> AndAlso<T>(
            this Expression<Func<T, bool>> expr1,
            Expression<Func<T, bool>> expr2)
        {
            var parameter = Expression.Parameter(typeof(T));

            var leftVisitor = new ReplaceExpressionVisitor(expr1.Parameters[0], parameter);
            var left = leftVisitor.Visit(expr1.Body);

            var rightVisitor = new ReplaceExpressionVisitor(expr2.Parameters[0], parameter);
            var right = rightVisitor.Visit(expr2.Body);

            return Expression.Lambda<Func<T, bool>>(
                Expression.AndAlso(left, right), parameter);
        }

        public class ReplaceExpressionVisitor
            : ExpressionVisitor
        {
            private readonly Expression _oldValue;
            private readonly Expression _newValue;

            public ReplaceExpressionVisitor(Expression oldValue,
                Expression newValue)
            {
                _oldValue = oldValue;
                _newValue = newValue;
            }

            public override Expression Visit(Expression node)
            {
                if (node == _oldValue)
                    return _newValue;
                return base.Visit(node);
            }
        }
    }

    public static class QueryableExtensions
    {
        public static IPagedListResult<T> ToPagedList<T>(this IQueryable<T> source, int totalCount, int pageSize = 20)
        {
            return new PagedListResult<T>().Create(source.ToList());
        }

        public static async Task<IPagedListResult<T>> ToPagedListAsync<T>(this IQueryable<T> source, int totalCount, int pageSize = 20)
        {
            return new PagedListResult<T>().Create(await source.ToListAsync(), totalCount, pageSize);
        }
    }

    public static class StringExtensions
    {
        public static bool IsNullOrWhiteSpace(this string value)
        {
            return String.IsNullOrWhiteSpace(value);
        }

        public static bool IsPopulated(this string value)
        {
            return !String.IsNullOrWhiteSpace(value) && !String.IsNullOrEmpty(value);
        }

        public static string Slugify(this string phrase)
        {
            var s = phrase.ToLower();
            //s = Regex.Replace(s, @"[^a-z0-9\s-]", "");                      // remove invalid characters
            s = Regex.Replace(s, @"\s+", " ").Trim();                       // single space
            s = s.Substring(0, s.Length <= 45 ? s.Length : 45).Trim();      // cut and trim
            s = Regex.Replace(s, @"\s", "-");                               // insert hyphens
            return s.ToLower();
        }
    }
}
