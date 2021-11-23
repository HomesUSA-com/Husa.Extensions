namespace Husa.Extensions.Linq
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    public static class ExpressionExtensions
    {
        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> baseQuery, Expression<Func<T, bool>> subQuery)
        {
            if (subQuery is null)
            {
                return baseQuery;
            }

            var parameterExpression = baseQuery.Parameters[0];

            var visitor = new SubstExpressionVisitor
            {
                Subst =
                {
                    [subQuery.Parameters[0]] = parameterExpression,
                },
            };

            Expression body = Expression.AndAlso(baseQuery.Body, visitor.Visit(subQuery.Body));
            return Expression.Lambda<Func<T, bool>>(body, parameterExpression);
        }

        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> baseQuery, Expression<Func<T, bool>> subQuery)
        {
            var parameterExpression = baseQuery.Parameters[0];

            var visitor = new SubstExpressionVisitor
            {
                Subst =
                {
                    [subQuery.Parameters[0]] = parameterExpression,
                },
            };

            Expression body = Expression.OrElse(baseQuery.Body, visitor.Visit(subQuery.Body));
            return Expression.Lambda<Func<T, bool>>(body, parameterExpression);
        }
8
        public static Expression<Func<T, bool>> AndIf<T>(this Expression<Func<T, bool>> baseQuery, bool addExpression, Expression<Func<T, bool>> subQuery)
        {
            return addExpression ? baseQuery.And(subQuery) : baseQuery;
        }

        internal class SubstExpressionVisitor : ExpressionVisitor
        {
            public IDictionary<Expression, Expression> Subst = new Dictionary<Expression, Expression>();

            protected override Expression VisitParameter(ParameterExpression node)
            {
                return this.Subst.TryGetValue(node, out var newValue) ? newValue : node;
            }
        }
    }
}
