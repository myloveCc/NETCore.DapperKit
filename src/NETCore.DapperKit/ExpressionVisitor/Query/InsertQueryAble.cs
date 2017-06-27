using NETCore.DapperKit.ExpressionVisitor.Core;
using NETCore.DapperKit.ExpressionVisitor.Query.Interface;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace NETCore.DapperKit.ExpressionVisitor.Query
{
    public class InsertQueryAble<T> : BaseSqlQueryAble<T>, IInsertQueryAble<T> where T : class
    {
        public InsertQueryAble(ISqlBuilder sqlBuilder, IDapperContext provider) : base(sqlBuilder, provider)
        {

        }
    }
}
