using NETCore.DapperKit.ExpressionToSql.Query.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;
using NETCore.DapperKit.ExpressionToSql.Core;

namespace NETCore.DapperKit.ExpressionToSql.Query
{
    public class UpdateQueryAble<T> : BaseSqlQueryAble<T>, IUpdateQueryAble<T> where T : class
    {
        public UpdateQueryAble(ISqlBuilder sqlBuilder, IDapperKitProvider provider) : base(sqlBuilder, provider)
        {

        }


        public IUpdateQueryAble<T> Where(Expression<Func<T, bool>> expression)
        {
            throw new NotImplementedException();
        }
    }
}
