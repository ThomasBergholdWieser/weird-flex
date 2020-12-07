using System;
using System.Linq;
using System.Linq.Expressions;
using WeirdFlex.Business.Interfaces;
using WeirdFlex.Common.Enums;
using WeirdFlex.Common.Model;

namespace Tieto.Lama.Business.UseCases
{
    public static class ISupportSortingExtensions
    {
        public static IQueryable<TQuery> ApplySorting<TQuery>(this ISupportSorting sortingProvider, IQueryable<TQuery> query,
            Func<IQueryable<TQuery>, IOrderedQueryable<TQuery>>? defaultSort = null)
        {
            if (sortingProvider.Sorting != null && sortingProvider.Sorting.Any())
            {
                var parameterExpression = Expression.Parameter(typeof(TQuery), "x");

                var sortedSortItems = sortingProvider.Sorting
                    .OrderBy(x => x.Order.GetValueOrDefault(99));

                IOrderedQueryable<TQuery>? ordered = null;
                foreach (var sortItem in sortedSortItems)
                {
                    ordered = ordered.Append(query, sortItem, parameterExpression);
                }
                query = ordered ?? query;
            }
            else if(defaultSort != null)
            {
                query = defaultSort(query);
            }

            return query;
        }

        static IOrderedQueryable<TQuery>? Append<TQuery>(this IOrderedQueryable<TQuery>? ordered, IQueryable<TQuery> root, SortItem sortItem, ParameterExpression parameterExpression)
        {
            var queryProp = typeof(TQuery).GetProperty(sortItem.MemberName);

            if(queryProp == null)
            {
                return ordered;
            }

            return queryProp.PropertyType switch
            {
                Type t when t == typeof(string) => GetOrdered(ordered, sortItem, root, Expression.Lambda<Func<TQuery, string>>(Expression.Property(parameterExpression, queryProp), new ParameterExpression[1] { parameterExpression })),
                Type t when t == typeof(DateTime) => GetOrdered(ordered, sortItem, root, Expression.Lambda<Func<TQuery, DateTime>>(Expression.Property(parameterExpression, queryProp), new ParameterExpression[1] { parameterExpression })),
                Type t when t == typeof(DateTime?) => GetOrdered(ordered, sortItem, root, Expression.Lambda<Func<TQuery, DateTime?>>(Expression.Property(parameterExpression, queryProp), new ParameterExpression[1] { parameterExpression })),
                Type t when t == typeof(int) => GetOrdered(ordered, sortItem, root, Expression.Lambda<Func<TQuery, int>>(Expression.Property(parameterExpression, queryProp), new ParameterExpression[1] { parameterExpression })),
                Type t when t == typeof(int?) => GetOrdered(ordered, sortItem, root, Expression.Lambda<Func<TQuery, int?>>(Expression.Property(parameterExpression, queryProp), new ParameterExpression[1] { parameterExpression })),
                Type t when t == typeof(long) => GetOrdered(ordered, sortItem, root, Expression.Lambda<Func<TQuery, long>>(Expression.Property(parameterExpression, queryProp), new ParameterExpression[1] { parameterExpression })),
                Type t when t == typeof(long?) => GetOrdered(ordered, sortItem, root, Expression.Lambda<Func<TQuery, long?>>(Expression.Property(parameterExpression, queryProp), new ParameterExpression[1] { parameterExpression })),
                Type t when t == typeof(double) => GetOrdered(ordered, sortItem, root, Expression.Lambda<Func<TQuery, double>>(Expression.Property(parameterExpression, queryProp), new ParameterExpression[1] { parameterExpression })),
                Type t when t == typeof(double?) => GetOrdered(ordered, sortItem, root, Expression.Lambda<Func<TQuery, double?>>(Expression.Property(parameterExpression, queryProp), new ParameterExpression[1] { parameterExpression })),
                Type t when t == typeof(decimal) => GetOrdered(ordered, sortItem, root, Expression.Lambda<Func<TQuery, decimal>>(Expression.Property(parameterExpression, queryProp), new ParameterExpression[1] { parameterExpression })),
                Type t when t == typeof(decimal?) => GetOrdered(ordered, sortItem, root, Expression.Lambda<Func<TQuery, decimal?>>(Expression.Property(parameterExpression, queryProp), new ParameterExpression[1] { parameterExpression })),
                Type t when t == typeof(bool) => GetOrdered(ordered, sortItem, root, Expression.Lambda<Func<TQuery, bool>>(Expression.Property(parameterExpression, queryProp), new ParameterExpression[1] { parameterExpression })),
                Type t when t == typeof(bool?) => GetOrdered(ordered, sortItem, root, Expression.Lambda<Func<TQuery, bool?>>(Expression.Property(parameterExpression, queryProp), new ParameterExpression[1] { parameterExpression })),
                Type t when t == typeof(short) => GetOrdered(ordered, sortItem, root, Expression.Lambda<Func<TQuery, short>>(Expression.Property(parameterExpression, queryProp), new ParameterExpression[1] { parameterExpression })),
                Type t when t == typeof(short?) => GetOrdered(ordered, sortItem, root, Expression.Lambda<Func<TQuery, short?>>(Expression.Property(parameterExpression, queryProp), new ParameterExpression[1] { parameterExpression })),
                _ => throw new NotImplementedException(),
            };
        }

        static IOrderedQueryable<TQuery> GetOrdered<TQuery, TSort>(IOrderedQueryable<TQuery>? ordered, SortItem sortItem, IQueryable<TQuery> root,
           Expression<Func<TQuery, TSort>> expression)
        {
            return ordered == null
                    ? GetOrderBy(sortItem, root, expression)
                    : GetThenBy(sortItem, ordered, expression);
        }

        static IOrderedQueryable<TQuery> GetOrderBy<TQuery, TSort>(SortItem sortItem, IQueryable<TQuery> query,
           Expression<Func<TQuery, TSort>> expression)
        {
            return sortItem.Direction switch
            {
                SortDirection.Descending => query.OrderByDescending(expression),
                _ => query.OrderBy(expression),
            };
        }

        static IOrderedQueryable<TQuery> GetThenBy<TQuery, TSort>(SortItem sortItem, IOrderedQueryable<TQuery> query,
            Expression<Func<TQuery, TSort>> expression)
        {
            return sortItem.Direction switch
            {
                SortDirection.Descending => query.ThenByDescending(expression),
                _ => query.ThenBy(expression),
            };
        }
    }
}
