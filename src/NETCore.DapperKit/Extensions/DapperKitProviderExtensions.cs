using NETCore.DapperKit.ExpressionToSql.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using NETCore.DapperKit.ExpressionToSql.Query;

namespace NETCore.DapperKit.Extensions
{
    public static class DapperKitProviderExtensions
    {
        public static ISqlQueryProvider<T> DataSet<T>(this IDapperKitProvider provider) where T : class
        {
            return new SqlQueryProvider<T>(provider);
        }
    }
}
