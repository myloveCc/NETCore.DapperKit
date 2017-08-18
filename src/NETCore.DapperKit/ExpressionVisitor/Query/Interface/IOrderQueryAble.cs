using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace NETCore.DapperKit.ExpressionVisitor.Query.Interface
{
    public interface IOrderQueryAble<T> : ICollectionQueryAble<T> where T : class
    {
        IOrderQueryAble<T> ThenBy(Expression<Func<T, object>> expression);

        IOrderQueryAble<T> ThenByDescending(Expression<Func<T, object>> expression);

        IPageQueryAble<T> Skip(int skipNum);
    }
}
