using NETCore.DapperKit.ExpressionToSql.Core;
using NETCore.DapperKit.ExpressionToSql.Internal;
using NETCore.DapperKit.Shared;
using System;
using System.Data;
using System.Linq.Expressions;
using NETCore.DapperKit.Extensions;
using NETCore.DapperKit.ExpressionToSql.Query.Interface;
using NETCore.DapperKit.ExpressionToSql.SqlVisitor;

namespace NETCore.DapperKit.ExpressionToSql.Query
{
    public class OrderQueryAble<T> : BaseCollectionQueryAble<T>, IOrderQueryAble<T> where T : class
    {
        public OrderQueryAble(ISqlBuilder sqlBuilder, IDapperKitProvider provider) : base(sqlBuilder, provider)
        {

        }

        public IPageQueryAble<T> Skip(int skipNum)
        {
            if (skipNum <= 0)
            {
                throw new Exception($"{skipNum} must be great than 0");
            }
            SqlBuilder.Skip(skipNum);
            return new PageQueryAble<T>(SqlBuilder, DapperKitProvider);
        }

        public IOrderQueryAble<T> ThenBy(Expression<Func<T, object>> expression)
        {
            Check.Argument.IsNotNull(expression, nameof(expression));
            SqlVistorProvider.ThenBy(expression.Body, SqlBuilder);
            return this;
        }

        public IOrderQueryAble<T> ThenByDescending(Expression<Func<T, object>> expression)
        {
            Check.Argument.IsNotNull(expression, nameof(expression));
            SqlVistorProvider.ThenBy(expression.Body, SqlBuilder);
            return this;
        }
    }
}
