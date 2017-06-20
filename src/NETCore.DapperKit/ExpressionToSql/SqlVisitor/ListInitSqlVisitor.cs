using NETCore.DapperKit.ExpressionToSql.Core;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace NETCore.DapperKit.ExpressionToSql.SqlVisitor
{
    public class ListInitSqlVisitor : BaseSqlVisitor<ListInitExpression>
    {
        protected override ISqlBuilder In(ListInitExpression expression, ISqlBuilder sqlBuilder)
        {
            //TODO
            return sqlBuilder;
        }
    }
}
