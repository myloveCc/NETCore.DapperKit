using NETCore.DapperKit.ExpressionToSql.Core;
using NETCore.DapperKit.ExpressionToSql.Extensions;
using NETCore.DapperKit.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace NETCore.DapperKit.ExpressionToSql.SqlVisitor
{
    public class MemberInitSqlVisitor : BaseSqlVisitor<MemberInitExpression>
    {
        protected override ISqlBuilder Insert(MemberInitExpression expression, ISqlBuilder sqlBuilder)
        {
            List<string> columns = new List<string>();
            List<string> parames = new List<string>();

            foreach (MemberAssignment memberAss in expression.Bindings)
            {
                var property = memberAss.Member as PropertyInfo;

                if (property.IsKeyProperty(expression.Type))
                {
                    continue;
                }

                if (!property.IsDataConlumnProperty(expression.Type))
                {
                    continue;
                }
                //add column name
                columns.Add($"{sqlBuilder.Formate(property.Name)}");

                string sqlParamName = string.Empty;
                var memeberExp = memberAss.Expression;

                var value = GetExpreesionValue(memeberExp);
                sqlParamName = sqlBuilder.SetSqlParameter(value);

                parames.Add(sqlParamName);
            }
            sqlBuilder.AppendInsertSql($"({string.Join(",", columns)}) VALUES ({string.Join(",", parames)})");
            return sqlBuilder;
        }

        protected override ISqlBuilder Update(MemberInitExpression expression, ISqlBuilder sqlBuilder)
        {
            var updates = new List<string>();

            foreach (MemberAssignment memberAss in expression.Bindings)
            {
                var memberExp = memberAss.Expression;
                var property = memberAss.Member as PropertyInfo;

                if (property.IsKeyProperty(expression.Type))
                {
                    continue;
                }
                if (!property.IsDataConlumnProperty(expression.Type))
                {
                    continue;
                }

                MemberInfo member = memberAss.Member;

                //var tablAliax = GetTableAlias(memberExp, sqlBuilder);

                var columnName = $"{sqlBuilder.Formate(member.Name)}";
                var value = GetExpreesionValue(memberExp);
                string sqlParamName = sqlBuilder.SetSqlParameter(value);
                updates.Add($"{columnName} = {sqlParamName}");
            }

            sqlBuilder.AppendUpdateSql($"{string.Join(",", updates)} ");

            return sqlBuilder;
        }

        protected override ISqlBuilder Select(MemberInitExpression expression, ISqlBuilder sqlBuilder)
        {
            var isHasAnyColumn = false;
            foreach (MemberAssignment memberAss in expression.Bindings)
            {
                isHasAnyColumn = true;

                var property = memberAss.Member as PropertyInfo;
                if (!property.IsDataConlumnProperty(expression.Type))
                {
                    continue;
                }

                var memberExp = memberAss.Expression as MemberExpression;
                var tablAlias = GetTableAlias(memberExp, sqlBuilder);
                var columnName = $"{tablAlias}{sqlBuilder.Formate(memberExp.Member.Name)}";

                MemberInfo member = memberAss.Member;
                var filedName = $"{sqlBuilder.Formate(member.Name)}";

                sqlBuilder.AddSelectColumn($"{columnName} {filedName}");
            }

            if (!isHasAnyColumn)
            {
                sqlBuilder.AddSelectColumn("* ");
            }

            return sqlBuilder;
        }
    }
}
