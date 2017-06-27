using NETCore.DapperKit.ExpressionVisitor.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using NETCore.DapperKit.ExpressionVisitor.Query;

namespace NETCore.DapperKit
{
    public static class DapperContextExtensions
    {
        public static ISqlQueryProvider<T> DbSet<T>(this IDapperContext provider) where T : class
        {
            return new SqlQueryProvider<T>(provider);
        }
    }
}
