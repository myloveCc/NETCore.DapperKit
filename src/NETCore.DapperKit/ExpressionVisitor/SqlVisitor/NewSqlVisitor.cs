using NETCore.DapperKit.ExpressionVisitor.Core;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using NETCore.DapperKit.Extensions;
using NETCore.DapperKit.ExpressionVisitor.Extensions;

namespace NETCore.DapperKit.ExpressionVisitor.SqlVisitor
{
    public class NewSqlVisitor : BaseSqlVisitor<NewExpression>
    {
        protected override ISqlBuilder Update(NewExpression expression, ISqlBuilder sqlBuilder)
        {
            return sqlBuilder;
        }

        protected override ISqlBuilder Select(NewExpression expression, ISqlBuilder sqlBuilder)
        {
            for (int i = 0; i < expression.Arguments.Count; i++)
            {
                var argument = expression.Arguments[i];

                var mermberExp = ((MemberExpression)argument);
                PropertyInfo property = mermberExp.Member as PropertyInfo;

                if (!property.IsDataConlumnProperty(mermberExp.Expression.Type))
                {
                    continue;
                }
                //get table name
                var tableName = mermberExp.Member.DeclaringType.GetDapperTableName(sqlBuilder._SqlFormater);
                string tableAlias = sqlBuilder.GetTableAlias(tableName);
                if (!string.IsNullOrWhiteSpace(tableAlias))
                {
                    tableAlias = $"{tableAlias}.";
                }
                //get table column name
                var columnName = $"{tableAlias}{sqlBuilder.Formate(mermberExp.Member.Name)}";
                //get data column name
                var fieldName = $"{sqlBuilder.Formate(expression.Members[i].Name)}";

                sqlBuilder.AddSelectColumn($"{columnName} {fieldName}");
                sqlBuilder.AddSelectPageColumn(fieldName);
            }

            return sqlBuilder;
        }



        protected override ISqlBuilder GroupBy(NewExpression expression, ISqlBuilder sqlBuilder)
        {
            for (int i = 0; i < expression.Arguments.Count; i++)
            {
                var memberExp = expression.Arguments[0] as MemberExpression;
                var memberInfo = expression.Members[0];

                sqlBuilder.AddCalculateColumn($"{sqlBuilder.Formate(memberExp.Member.Name)} {sqlBuilder.Formate(memberInfo.Name)}");
                sqlBuilder.AppendGroupSql($"{sqlBuilder.Formate(memberExp.Member.Name)}");
            }

            return sqlBuilder;
        }

        protected override ISqlBuilder OrderBy(NewExpression expression, ISqlBuilder sqlBuilder)
        {
            foreach (Expression item in expression.Arguments)
            {
                SqlVistorProvider.OrderBy(item, sqlBuilder);
            }

            return sqlBuilder;
        }

        protected override ISqlBuilder ThenBy(NewExpression expression, ISqlBuilder sqlBuilder)
        {
            foreach (Expression item in expression.Arguments)
            {
                SqlVistorProvider.ThenBy(item, sqlBuilder);
            }
            return sqlBuilder;
        }

        protected override ISqlBuilder OrderByDescending(NewExpression expression, ISqlBuilder sqlBuilder)
        {
            foreach (Expression item in expression.Arguments)
            {
                SqlVistorProvider.OrderByDescending(item, sqlBuilder);
            }
            return sqlBuilder;
        }

        protected override ISqlBuilder ThenByDescending(NewExpression expression, ISqlBuilder sqlBuilder)
        {
            foreach (Expression item in expression.Arguments)
            {
                SqlVistorProvider.ThenByDescending(item, sqlBuilder);
            }
            return sqlBuilder;
        }

        protected override ISqlBuilder Max(NewExpression expression, ISqlBuilder sqlBuilder)
        {
            if (expression.Arguments.Count > 1)
            {
                throw new Exception("Calculate method can only contain one property ");
            }

            var memberExp = expression.Arguments[0] as MemberExpression;
            var memberInfo = expression.Members[0];

            sqlBuilder.AddCalculateColumn($"MAX({sqlBuilder.Formate(memberExp.Member.Name)}) {sqlBuilder.Formate(memberInfo.Name)}");

            return sqlBuilder;
        }

        protected override ISqlBuilder Min(NewExpression expression, ISqlBuilder sqlBuilder)
        {
            if (expression.Arguments.Count > 1)
            {
                throw new Exception("Calculate method can only contain one property ");
            }

            var memberExp = expression.Arguments[0] as MemberExpression;
            var memberInfo = expression.Members[0];

            sqlBuilder.AddCalculateColumn($"MIN({sqlBuilder.Formate(memberExp.Member.Name)}) {sqlBuilder.Formate(memberInfo.Name)}");

            return sqlBuilder;
        }

        protected override ISqlBuilder Avg(NewExpression expression, ISqlBuilder sqlBuilder)
        {
            if (expression.Arguments.Count > 1)
            {
                throw new Exception("Calculate method can only contain one property ");
            }

            var memberExp = expression.Arguments[0] as MemberExpression;
            var memberInfo = expression.Members[0];

            sqlBuilder.AddCalculateColumn($"AVG({sqlBuilder.Formate(memberExp.Member.Name)}) {sqlBuilder.Formate(memberInfo.Name)}");

            return sqlBuilder;
        }

        protected override ISqlBuilder Count(NewExpression expression, ISqlBuilder sqlBuilder)
        {
            if (expression.Arguments.Count > 1)
            {
                throw new Exception("Calculate method can only contain one property ");
            }

            var memberExp = expression.Arguments[0] as MemberExpression;
            var memberInfo = expression.Members[0];

            sqlBuilder.AddCalculateColumn($"COUNT({sqlBuilder.Formate(memberExp.Member.Name)}) {sqlBuilder.Formate(memberInfo.Name)}");

            return sqlBuilder;
        }

        protected override ISqlBuilder Sum(NewExpression expression, ISqlBuilder sqlBuilder)
        {
            if (expression.Arguments.Count > 1)
            {
                throw new Exception("Calculate method can only contain one property ");
            }

            var memberExp = expression.Arguments[0] as MemberExpression;
            var memberInfo = expression.Members[0];

            sqlBuilder.AddCalculateColumn($"SUM({sqlBuilder.Formate(memberExp.Member.Name)}) {sqlBuilder.Formate(memberInfo.Name)}");

            return sqlBuilder;
        }
    }
}
