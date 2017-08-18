using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace NETCore.DapperKit.ExpressionVisitor.Query.Interface
{
    public interface IDeleteQueryAble<T> : ISqlQueryAble<T> where T : class
    {

    }
}
