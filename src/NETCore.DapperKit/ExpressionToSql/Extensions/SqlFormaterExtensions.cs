using NETCore.DapperKit.ExpressionToSql.Core;
using NETCore.DapperKit.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace NETCore.DapperKit.ExpressionToSql.Extensions
{
    internal static class SqlBuilderExtensions
    {
        internal static string Formate(this ISqlBuilder builder, string sqlName)
        {
            Check.Argument.IsNotEmpty(sqlName, nameof(sqlName));
            return $"{builder._SqlFormater.Left}{sqlName}{builder._SqlFormater.Right}";
        }

    }
}
