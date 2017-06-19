using NETCore.DapperKit.Expression.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using NETCore.DapperKit.Expression.QueryAble;

namespace NETCore.DapperKit.Expression.Extensions
{
    public static class DbConnectionExtensions
    {
        public static SqlQueryProvier<T> SqlQuery<T>(this IDbConnection conn) where T : class
        {
            //TODO
            return new SqlQueryProvier<T>(conn);
        }
    }
}
