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

        int Exect();

        Task<int> ExectAsync();
    }
}
