using NETCore.DapperKit.ExpressionToSql.Query.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;
using NETCore.DapperKit.ExpressionToSql.Core;

namespace NETCore.DapperKit.ExpressionToSql.Query
{
    public class DeleteQueryAble<T> : BaseSqlQueryAble<T>, IDeleteQueryAble<T> where T : class
    {
        public DeleteQueryAble(ISqlBuilder sqlBuilder, IDapperKitProvider provider) : base(sqlBuilder, provider)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="whereExpression"></param>
        /// <returns></returns>
        public IDeleteQueryAble<T> Where(Expression<Func<T, bool>> whereExpression)
        {
            throw new NotImplementedException();
        }
    }
}
