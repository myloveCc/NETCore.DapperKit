using System;
using System.Collections.Generic;
using System.Text;

namespace NETCore.DapperKit.ExpressionVisitor.Query.Interface
{
    public interface IInsertQueryAble<T> : ISqlQueryAble<T> where T : class
    {
       
    }
}
