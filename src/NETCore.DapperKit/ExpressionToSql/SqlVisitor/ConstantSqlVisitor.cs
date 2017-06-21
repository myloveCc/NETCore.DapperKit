using NETCore.DapperKit.ExpressionToSql.Core;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace NETCore.DapperKit.ExpressionToSql.SqlVisitor
{
    public class ConstantSqlVisitor : BaseSqlVisitor<ConstantExpression>
    {
        protected override ISqlBuilder Join(ConstantExpression expression, ISqlBuilder sqlBuilder)
        {
            var dbParamName = sqlBuilder.SetSqlParameter(expression.Value);
            sqlBuilder.AppendJoinSql($" {dbParamName}");
            return sqlBuilder;
        }

        protected override ISqlBuilder Where(ConstantExpression expression, ISqlBuilder sqlBuilder)
        {
            var dbParamName = sqlBuilder.SetSqlParameter(expression.Value);
            sqlBuilder.AppendWhereSql($" {dbParamName}");
            return sqlBuilder;
        }

        protected override ISqlBuilder In(ConstantExpression expression, ISqlBuilder sqlBuilder)
        {
            //TODO

            return sqlBuilder;
        }

        protected override ISqlBuilder Delete(ConstantExpression expression, ISqlBuilder sqlBuilder)
        {
            var sqlParamName = sqlBuilder.SetSqlParameter(expression.Value);
            sqlBuilder.AppendWhereSql($"{sqlParamName} ");

            return sqlBuilder;
        }
    }
}