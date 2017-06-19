using NETCore.DapperKit.Expression.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using NETCore.DapperKit.Expression.Query;

namespace NETCore.DapperKit.Expression.Extensions
{
    public static class DbConnectionExtensions
    {
        public static SqlQueryAble<T> SqlQuery<T>(this IDbConnection conn) where T : class
        {
            //TODO
            return new SqlQueryAble<T>(conn);
        }
    }
}
