using NETCore.DapperKit.ExpressionVisitor.Query.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;
using NETCore.DapperKit.ExpressionVisitor.Core;
using NETCore.DapperKit.ExpressionVisitor.SqlVisitor;
using NETCore.DapperKit.Shared;

namespace NETCore.DapperKit.ExpressionVisitor.Query
{
    public class UpdateQueryAble<T> : BaseSqlQueryAble<T>, IUpdateQueryAble<T> where T : class
    {
        public UpdateQueryAble(ISqlBuilder sqlBuilder, IDapperContext provider) : base(sqlBuilder, provider)
        {

        }


        public IUpdateQueryAble<T> Where(Expression<Func<T, bool>> expression)
        {
            Check.Argument.IsNotNull(expression, nameof(expression));

            SqlVistorProvider.Where(expression.Body, SqlBuilder);
            return this;
        }
    }
}
