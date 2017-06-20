using NETCore.DapperKit.ExpressionToSql.Core;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace NETCore.DapperKit.ExpressionToSql.SqlVisitor
{
    public class NewArraySqlVisitor : BaseSqlVisitor<NewArrayExpression>
    {
        protected override ISqlBuilder In(NewArrayExpression expression, ISqlBuilder sqlBuilder)
        {
            return sqlBuilder;
        }
    }
}
