using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Reflection;
using NETCore.DapperKit.ExpressionToSql.Core;

namespace NETCore.DapperKit.ExpressionToSql.SqlVisitor
{
    public class MemberSqlVisitor : BaseSqlVisitor<MemberExpression>
    {
        private static object GetValue(MemberExpression expr)
        {
            object value;
            var field = expr.Member as FieldInfo;
            if (field != null)
            {
                value = field.GetValue(((ConstantExpression)expr.Expression).Value);
            }
            else
            {
                value = ((PropertyInfo)expr.Member).GetValue(((ConstantExpression)expr.Expression).Value, null);
            }
            return value;
        }

        protected override ISqlBuilder Insert(MemberExpression expression, ISqlBuilder sqlBuilder)
        {
            return sqlBuilder;
        }

        protected override ISqlBuilder Select(MemberExpression expression, ISqlBuilder sqlBuilder)
        {
            return sqlBuilder;
        }

        protected override ISqlBuilder Join(MemberExpression expression, ISqlBuilder sqlBuilder)
        {

            return sqlBuilder;
        }

        protected override ISqlBuilder Where(MemberExpression expression, ISqlBuilder sqlBuilder)
        {

            return sqlBuilder;
        }

        protected override ISqlBuilder In(MemberExpression expression, ISqlBuilder sqlBuilder)
        {
            return sqlBuilder;
        }

        protected override ISqlBuilder GroupBy(MemberExpression expression, ISqlBuilder sqlBuilder)
        {
            return sqlBuilder;
        }

        protected override ISqlBuilder OrderBy(MemberExpression expression, ISqlBuilder sqlBuilder)
        {
            return sqlBuilder;
        }

        protected override ISqlBuilder ThenBy(MemberExpression expression, ISqlBuilder sqlBuilder)
        {
            return sqlBuilder;
        }

        protected override ISqlBuilder OrderByDescending(MemberExpression expression, ISqlBuilder sqlBuilder)
        {
            return sqlBuilder;
        }

        protected override ISqlBuilder ThenByDescending(MemberExpression expression, ISqlBuilder sqlBuilder)
        {
            return sqlBuilder;
        }

        private ISqlBuilder AggregateFunctionParser(MemberExpression expression, ISqlBuilder sqlBuilder, string functionName)
        {
            return sqlBuilder;
        }

        protected override ISqlBuilder Max(MemberExpression expression, ISqlBuilder sqlBuilder)
        {
            return AggregateFunctionParser(expression, sqlBuilder, "Max");
        }

        protected override ISqlBuilder Min(MemberExpression expression, ISqlBuilder sqlBuilder)
        {
            return AggregateFunctionParser(expression, sqlBuilder, "Min");
        }

        protected override ISqlBuilder Avg(MemberExpression expression, ISqlBuilder sqlBuilder)
        {
            return AggregateFunctionParser(expression, sqlBuilder, "Avg");
        }

        protected override ISqlBuilder Count(MemberExpression expression, ISqlBuilder sqlBuilder)
        {
            return AggregateFunctionParser(expression, sqlBuilder, "Count");
        }

        protected override ISqlBuilder Sum(MemberExpression expression, ISqlBuilder sqlBuilder)
        {
            return AggregateFunctionParser(expression, sqlBuilder, "Sum");
        }
    }
}
