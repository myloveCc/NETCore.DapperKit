using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace NETCore.DapperKit.ExpressionToSql.Query.Interface
{
    public interface IUpdateQueryAble<T> : ISqlQueryAble<T> where T : class
    {
        IUpdateQueryAble<T> Where(Expression<Func<T, bool>> expression);
    }
}
