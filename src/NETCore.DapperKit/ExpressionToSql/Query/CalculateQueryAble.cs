using NETCore.DapperKit.ExpressionToSql.Core;
using NETCore.DapperKit.ExpressionToSql.Query.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;

namespace NETCore.DapperKit.ExpressionToSql.Query
{
    public class CalculateQueryAble<T> : BaseSqlQueryAble<T>, ICalculateQueryAble<T> where T : class
    {
        public CalculateQueryAble(ISqlBuilder sqlBuilder, IDapperKitProvider provider) : base(sqlBuilder, provider)
        {

        }

        public ICalculateQueryAble<T> GroupBy(Expression<Func<T, object>> expression)
        {
            throw new NotImplementedException();
        }

        public ICalculateQueryAble<T> Where(Expression<Func<T, bool>> expression)
        {
            throw new NotImplementedException();
        }
    }
}

