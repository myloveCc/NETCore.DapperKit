using NETCore.DapperKit.ExpressionToSql.Core;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using NETCore.DapperKit.Extensions;

namespace NETCore.DapperKit.ExpressionToSql.SqlVisitor
{
    public class NewSqlVisitor : BaseSqlVisitor<NewExpression>
    {
        protected override ISqlBuilder Update(NewExpression expression, ISqlBuilder sqlBuilder)
        {
            return sqlBuilder;
        }

        protected override ISqlBuilder Select(NewExpression expression, ISqlBuilder sqlBuilder)
        {
            List<string> selectFiles = new List<string>();

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
                var columnName = $"{tableAlias}{sqlBuilder._SqlFormater.Left}{mermberExp.Member.Name}{sqlBuilder._SqlFormater.Right}";
                //get data column name
                var fieldName = $"{sqlBuilder._SqlFormater.Left}{expression.Members[i].Name}{sqlBuilder._SqlFormater.Right}";

                selectFiles.Add($"{columnName} {fieldName}");
            }

            sqlBuilder.AppendSelectSql($"{string.Join(",", selectFiles)} ");

            return sqlBuilder;
        }

        protected override ISqlBuilder GroupBy(NewExpression expression, ISqlBuilder sqlBuilder)
        {
            return sqlBuilder;
        }

        protected override ISqlBuilder OrderBy(NewExpression expression, ISqlBuilder sqlBuilder)
        {
            return sqlBuilder;
        }

        protected override ISqlBuilder ThenBy(NewExpression expression, ISqlBuilder sqlBuilder)
        {
            return sqlBuilder;
        }

        protected override ISqlBuilder OrderByDescending(NewExpression expression, ISqlBuilder sqlBuilder)
        {
            return sqlBuilder;
        }

        protected override ISqlBuilder ThenByDescending(NewExpression expression, ISqlBuilder sqlBuilder)
        {
            return sqlBuilder;
        }
    }
}
