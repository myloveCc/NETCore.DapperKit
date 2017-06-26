using NETCore.DapperKit.ExpressionToSql.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NETCore.DapperKit.ExpressionToSql.Query.Interface
{
    public interface ISqlQueryAble<T> : ICollectionQueryAble<T> where T : class
    {

        int Exect();

        Task<int> ExectAsync();
    }
}
