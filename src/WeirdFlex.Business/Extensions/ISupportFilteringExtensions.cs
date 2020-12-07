using AutoMapper.Internal;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using WeirdFlex.Business.Interfaces;
using WeirdFlex.Data.EF;

namespace Tieto.Lama.Business.UseCases
{
    public static class ISupportFilteringExtensions
    {
        public static IQueryable<TQuery> ApplyFilter<TQuery>(this ISupportFiltering filterProvider, IQueryable<TQuery> query)
        {
            if (filterProvider.Filter == null)
            {
                return query;
            }

            var filters = filterProvider.Filter;
            var queryType = typeof(TQuery);

            var parameterExpression = Expression.Parameter(typeof(TQuery), "x");
            foreach (var filter in filters)
            {
                var queryProp = queryType.GetProperty(filter.MemberName);
                if (queryProp == null)
                {
                    continue;
                }

                if (queryProp.PropertyType == typeof(string))
                {
                    query = ApplyStringLikeFilter(query, parameterExpression, filter.Value, queryProp);
                }
                else if (queryProp.PropertyType.IsEnum || 
                    (queryProp.PropertyType.IsNullableType() && queryProp.PropertyType.GetTypeOfNullable().IsEnum))
                {
                    query = ApplyEnumFilter(query, parameterExpression, filter.Value, queryProp);
                }
                else
                {
                    query = ApplyPropertyTypeFilter(query, parameterExpression, filter.Value, queryProp);
                }
            }

            return query;
        }

        private static IQueryable<TQuery> ApplyPropertyTypeFilter<TQuery>(IQueryable<TQuery> query, ParameterExpression parameterExpression, object? filterValue, PropertyInfo queryProp)
        {
            query = query.Where(Expression.Lambda<Func<TQuery, bool>>(
                Expression.Equal(
                    Expression.Property(parameterExpression, queryProp),
                    Expression.Constant(filterValue, queryProp.PropertyType)),
                new ParameterExpression[1]
                {
                            parameterExpression
                }));
            return query;
        }

        private static IQueryable<TQuery> ApplyEnumFilter<TQuery>(IQueryable<TQuery> query, ParameterExpression parameterExpression, object? filterValue, PropertyInfo queryProp)
        {
            var intType = queryProp.PropertyType.IsNullableType() ? typeof(int?) : typeof(int);
            query = query.Where(Expression.Lambda<Func<TQuery, bool>>(
                Expression.Equal(
                    Expression.Convert(Expression.Property(parameterExpression, queryProp), intType),
                    Expression.Convert(Expression.Constant(filterValue, queryProp.PropertyType), intType)),
                new ParameterExpression[1]
                {
                            parameterExpression
                }));
            return query;
        }

        private static IQueryable<TQuery> ApplyStringLikeFilter<TQuery>(IQueryable<TQuery> query, ParameterExpression parameterExpression, object? filterValue, PropertyInfo queryProp)
        {
            var wildcards = filterValue?.ToString().WithWildcards();
            query = query.Where(Expression.Lambda<Func<TQuery, bool>>(
                Expression.Call(null, DbFunctionsExpressions.LikeMethod, new Expression[3]
                {
                    Expression.Constant(Microsoft.EntityFrameworkCore.EF.Functions),
                    Expression.Property(parameterExpression, queryProp),
                    Expression.Constant(wildcards, typeof(string))
                }), new ParameterExpression[1]
            {
                        parameterExpression
            }));
            return query;
        }
    }
}
