using NETCore.DapperKit.ExpressionVisitor.Core;
using NETCore.DapperKit.ExpressionVisitor.Internal;
using NETCore.DapperKit.Shared;
using System;
using System.Data;
using System.Linq.Expressions;
using NETCore.DapperKit.Extensions;
using NETCore.DapperKit.ExpressionVisitor.Query.Interface;
using NETCore.DapperKit.ExpressionVisitor.SqlVisitor;

namespace NETCore.DapperKit.ExpressionVisitor.Query
{
    public class OrderQueryAble<T> : BaseCollectionQueryAble<T>, IOrderQueryAble<T> where T : class
    {
        public OrderQueryAble(ISqlBuilder sqlBuilder, IDapperContext provider) : base(sqlBuilder, provider)
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
            SqlVistorProvider.ThenByDescending(expression.Body, SqlBuilder);
            return this;
        }
    }
}
