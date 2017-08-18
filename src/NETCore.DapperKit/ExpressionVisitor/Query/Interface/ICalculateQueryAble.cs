using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace NETCore.DapperKit.ExpressionVisitor.Query.Interface
{
    public interface ICalculateQueryAble<T> : ISqlQueryAble<T> where T : class
    {
        ICalculateQueryAble<T> Where(Expression<Func<T, bool>> expression);

        ICalculateQueryAble<T> GroupBy(Expression<Func<T, object>> expression);
    }
}
