using NETCore.DapperKit.ExpressionToSql.Core;
using NETCore.DapperKit.ExpressionToSql.Query.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;
using NETCore.DapperKit.Shared;
using NETCore.DapperKit.ExpressionToSql.SqlVisitor;

namespace NETCore.DapperKit.ExpressionToSql.Query
{
    public class CalculateQueryAble<T> : BaseSqlQueryAble<T>, ICalculateQueryAble<T> where T : class
    {
        public CalculateQueryAble(ISqlBuilder sqlBuilder, IDapperKitProvider provider) : base(sqlBuilder, provider)
        {

        }

        public ICalculateQueryAble<T> GroupBy(Expression<Func<T, object>> expression)
        {
            Check.Argument.IsNotNull(expression, nameof(expression));
            SqlVistorProvider.GroupBy(expression.Body, SqlBuilder);
            return this;
        }

        public ICalculateQueryAble<T> Where(Expression<Func<T, bool>> expression)
        {
            Check.Argument.IsNotNull(expression, nameof(expression));
            SqlVistorProvider.Where(expression.Body, SqlBuilder);
            return this;
        }
    }
}

