using NETCore.DapperKit.ExpressionToSql.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NETCore.DapperKit.ExpressionToSql.Query.Interface
{
    public interface ISqlQueryAble<T> where T : class
    {
        ISqlBuilder SqlBuilder { get; }

        TResult Exect<TResult>();

        Task<TResult> ExectAsync<TResult>();
    }
}
