using NETCore.DapperKit.ExpressionToSql.Core;
using NETCore.DapperKit.ExpressionToSql.Query.Interface;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace NETCore.DapperKit.ExpressionToSql.Query
{
    public class InsertQueryAble<T> : BaseSqlQueryAble<T>, IInsertQueryAble<T> where T : class
    {
        public InsertQueryAble(ISqlBuilder sqlBuilder, IDapperKitProvider provider) : base(sqlBuilder, provider)
        {

        }
    }
}
