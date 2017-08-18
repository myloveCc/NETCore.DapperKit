using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace NETCore.DapperKit.ExpressionVisitor.Query.Interface
{
    public interface IPageQueryAble<T> : ICollectionQueryAble<T> where T : class
    {
        IPageQueryAble<T> Take(int takeNum);
    }
}
