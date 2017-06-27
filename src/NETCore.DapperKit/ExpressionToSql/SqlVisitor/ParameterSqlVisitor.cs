using NETCore.DapperKit.ExpressionToSql.Core;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace NETCore.DapperKit.ExpressionToSql.SqlVisitor
{
    public class ParameterSqlVisitor : BaseSqlVisitor<ParameterExpression>
    {
        protected override ISqlBuilder Select(ParameterExpression expression, ISqlBuilder sqlBuilder)
        {
            SqlVistorProvider.In(expression, sqlBuilder);
            return sqlBuilder;
        }

        protected override ISqlBuilder Where(ParameterExpression expression, ISqlBuilder sqlBuilder)
        {
            SqlVistorProvider.In(expression, sqlBuilder);
            return sqlBuilder;
        }

        protected override ISqlBuilder GroupBy(ParameterExpression expression, ISqlBuilder sqlBuilder)
        {
            SqlVistorProvider.In(expression, sqlBuilder);
            return sqlBuilder;
        }

        protected override ISqlBuilder OrderBy(ParameterExpression expression, ISqlBuilder sqlBuilder)
        {
            SqlVistorProvider.In(expression, sqlBuilder);
            return sqlBuilder;
        }

        protected override ISqlBuilder Max(ParameterExpression expression, ISqlBuilder sqlBuilder)
        {
            SqlVistorProvider.In(expression, sqlBuilder);
            return sqlBuilder;
        }

        protected override ISqlBuilder Min(ParameterExpression expression, ISqlBuilder sqlBuilder)
        {
            SqlVistorProvider.In(expression, sqlBuilder);
            return sqlBuilder;
        }

        protected override ISqlBuilder Avg(ParameterExpression expression, ISqlBuilder sqlBuilder)
        {
            SqlVistorProvider.In(expression, sqlBuilder);
            return sqlBuilder;
        }

        protected override ISqlBuilder Count(ParameterExpression expression, ISqlBuilder sqlBuilder)
        {
            SqlVistorProvider.In(expression, sqlBuilder);
            return sqlBuilder;
        }

        protected override ISqlBuilder Sum(ParameterExpression expression, ISqlBuilder sqlBuilder)
        {
            SqlVistorProvider.In(expression, sqlBuilder);
            return sqlBuilder;
        }
    }
}
