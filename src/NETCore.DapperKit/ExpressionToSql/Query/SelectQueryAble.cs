using NETCore.DapperKit.ExpressionToSql.Core;
using NETCore.DapperKit.ExpressionToSql.Internal;
using NETCore.DapperKit.ExpressionToSql.Query.Interface;
using NETCore.DapperKit.ExpressionToSql.SqlVisitor;
using NETCore.DapperKit.Extensions;
using NETCore.DapperKit.Shared;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace NETCore.DapperKit.ExpressionToSql.Query
{
    public class SelectQueryAble<T> : BaseCollectionQueryAble<T>, ISelectQueryAble<T> where T : class
    {
        public ICollection<Type> TableTypeCollections { get; set; }

        public SelectQueryAble(ISqlBuilder sqlBuilder, IDapperKitProvider provider) : base(sqlBuilder, provider)
        {
            TableTypeCollections = new List<Type>();
        }

        public ISelectQueryAble<T> Where(Expression<Func<T, bool>> expression)
        {
            Check.Argument.IsNotNull(expression, nameof(expression));
            SqlVistorProvider.Where(expression.Body, SqlBuilder);
            return this;
        }

        private SelectQueryAble<T> JoinParser(Expression expression, Expression expressionBody, JoinType joinType, params Type[] types)
        {
            if (expression != null && expressionBody != null)
            {
                foreach (var type in types)
                {
                    if (!TableTypeCollections.Contains(type))
                    {
                        var tableName = types[types.Length - 1].GetDapperTableName(SqlBuilder._SqlFormater);
                        throw new Exception($"select query tables can not found {tableName}");
                    }
                }

                var joinStr = string.Empty;
                if (joinType != JoinType.NONE)
                {
                    joinStr = joinType.ToString();
                }

                var joinTableName = types[types.Length - 1].GetDapperTableName(SqlBuilder._SqlFormater);
                var joinTableAlias = SqlBuilder.GetTableAlias(joinTableName);


                SqlBuilder.AppendJoinSql($"{joinStr} JOIN {(joinTableName + " " + joinTableAlias)} ON ");

                SqlVistorProvider.Join(expressionBody, SqlBuilder);
            }
            return this;
        }

        public ISelectQueryAble<T> Join<T2>(Expression<Func<T, T2, bool>> expression)
        {
            Check.Argument.IsNotNull(expression, nameof(expression));
            JoinParser(expression, expression == null ? null : expression.Body, JoinType.NONE, typeof(T2));
            return this;
        }

        public ISelectQueryAble<T> Join<T2, T3>(Expression<Func<T2, T3, bool>> expression)
        {
            Check.Argument.IsNotNull(expression, nameof(expression));
            JoinParser(expression, expression == null ? null : expression.Body, JoinType.NONE, typeof(T2), typeof(T3));
            return this;
        }

        public ISelectQueryAble<T> InnerJoin<T2>(Expression<Func<T, T2, bool>> expression)
        {
            Check.Argument.IsNotNull(expression, nameof(expression));
            JoinParser(expression, expression == null ? null : expression.Body, JoinType.INNER, typeof(T2));
            return this;
        }

        public ISelectQueryAble<T> InnerJoin<T2, T3>(Expression<Func<T2, T3, bool>> expression)
        {
            Check.Argument.IsNotNull(expression, nameof(expression));
            JoinParser(expression, expression == null ? null : expression.Body, JoinType.INNER, typeof(T2), typeof(T3));
            return this;
        }

        public ISelectQueryAble<T> LeftJoin<T2>(Expression<Func<T, T2, bool>> expression)
        {
            Check.Argument.IsNotNull(expression, nameof(expression));
            JoinParser(expression, expression == null ? null : expression.Body, JoinType.LEFT, typeof(T2));
            return this;
        }

        public ISelectQueryAble<T> LeftJoin<T2, T3>(Expression<Func<T2, T3, bool>> expression)
        {
            Check.Argument.IsNotNull(expression, nameof(expression));
            JoinParser(expression, expression == null ? null : expression.Body, JoinType.RIGHT, typeof(T2), typeof(T3));
            return this;
        }

        public ISelectQueryAble<T> RightJoin<T2>(Expression<Func<T, T2, bool>> expression)
        {
            Check.Argument.IsNotNull(expression, nameof(expression));
            JoinParser(expression, expression == null ? null : expression.Body, JoinType.RIGHT, typeof(T2));
            return this;
        }

        public ISelectQueryAble<T> RightJoin<T2, T3>(Expression<Func<T2, T3, bool>> expression)
        {
            Check.Argument.IsNotNull(expression, nameof(expression));
            JoinParser(expression, expression == null ? null : expression.Body, JoinType.RIGHT, typeof(T2), typeof(T3));
            return this;
        }

        public ISelectQueryAble<T> FullJoin<T2>(Expression<Func<T, T2, bool>> expression)
        {
            Check.Argument.IsNotNull(expression, nameof(expression));
            JoinParser(expression, expression == null ? null : expression.Body, JoinType.FULL, typeof(T2));
            return this;
        }

        public ISelectQueryAble<T> FullJoin<T2, T3>(Expression<Func<T2, T3, bool>> expression)
        {
            Check.Argument.IsNotNull(expression, nameof(expression));
            JoinParser(expression, expression == null ? null : expression.Body, JoinType.FULL, typeof(T2), typeof(T3));
            return this;
        }

        public ISelectQueryAble<T> GroupBy(Expression<Func<T, object>> expression)
        {
            Check.Argument.IsNotNull(expression, nameof(expression));
            SqlVistorProvider.GroupBy(expression.Body, SqlBuilder);
            return this;
        }

        public IOrderQueryAble<T> OrderBy(Expression<Func<T, object>> expression)
        {
            Check.Argument.IsNotNull(expression, nameof(expression));
            SqlVistorProvider.OrderBy(expression.Body, SqlBuilder);
            return new OrderQueryAble<T>(SqlBuilder, DapperKitProvider);
        }

        public IOrderQueryAble<T> OrderByDescending(Expression<Func<T, object>> expression)
        {
            Check.Argument.IsNotNull(expression, nameof(expression));
            SqlVistorProvider.OrderByDescending(expression.Body, SqlBuilder);
            return new OrderQueryAble<T>(SqlBuilder, DapperKitProvider);
        }
    }
}
