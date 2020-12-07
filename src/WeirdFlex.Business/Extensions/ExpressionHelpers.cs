using System.Collections.Generic;
using System.Reflection;

namespace System.Linq.Expressions
{
    public static class ExpressionHelpers
    {
        public static MemberExpression Member<TEntity, TProperty>(ParameterExpression parameterExpression, Expression<Func<TEntity, TProperty>> taskMember)
        {
            return Expression.Property(parameterExpression, Property(taskMember));
        }

        public static PropertyInfo Property<TEntity, TProperty>(Expression<Func<TEntity, TProperty>> exp)
        {
            return (PropertyInfo)(exp.Body as MemberExpression)!.Member;
        }

        public static Expression And(IEnumerable<Expression> expressions, ParameterExpression parameterExpression)
        {
            Expression overallExpression = Expression.IsTrue(Expression.Constant(true, typeof(bool)));

            if (expressions.Count() == 1)
            {
                overallExpression = expressions.First();
            }

            if (expressions.Count() > 1)
            {
                Expression? start = null;
                foreach (var expression in expressions)
                {
                    start = start == null
                        ? expression
                        : Expression.AndAlso(start, expression);
                }
                if (start != null)
                {
                    overallExpression = start!;
                }
            }

            return overallExpression;
        }
    }
}
