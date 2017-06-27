using NETCore.DapperKit.ExpressionVisitor.Query.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;
using NETCore.DapperKit.ExpressionVisitor.Core;

namespace NETCore.DapperKit.ExpressionVisitor.Query
{
    public class DeleteQueryAble<T> : BaseSqlQueryAble<T>, IDeleteQueryAble<T> where T : class
    {
        public DeleteQueryAble(ISqlBuilder sqlBuilder, IDapperContext provider) : base(sqlBuilder, provider)
        {

        }
    }
}
