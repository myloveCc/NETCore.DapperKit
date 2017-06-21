using NETCore.DapperKit.ExpressionToSql.Core;
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

                if (property.IsIdentity(expression.Type))
                {
                    continue;
                }

                if (!property.IsDataColumn(expression.Type))
                {
                    continue;
                }
                //add column name
                columns.Add($"{sqlBuilder._SqlFormater.Left}{property.Name}{sqlBuilder._SqlFormater.Right}");

                string sqlParamName = string.Empty;
                var memberExpression = memberAss.Expression;
                if (memberExpression is ConstantExpression)
                {
                    ConstantExpression c = memberExpression as ConstantExpression;

                    //bool
                    var value = c.Value;
                    if (c.Type == typeof(bool))
                    {
                        if (Convert.ToBoolean(value))
                        {
                            value = 1;
                        }
                        else
                        {
                            value = 0;
                        }
                    }
                    sqlParamName = sqlBuilder.SetSqlParameter(value);
                }
                else
                {
                    var value = GetExpreesionValue(expression);
                    sqlParamName = sqlBuilder.SetSqlParameter(value);
                }
                parames.Add(sqlParamName);
            }
            sqlBuilder.AppendInsertSql($"({string.Join(",", columns)}) VALUES ({string.Join(",", parames)})");
            return sqlBuilder;
        }

        protected override ISqlBuilder Update(MemberInitExpression expression, ISqlBuilder sqlBuilder)
        {
            foreach (MemberAssignment memberAss in expression.Bindings)
            {

            }
            return sqlBuilder;
        }

        protected override ISqlBuilder Select(MemberInitExpression expression, ISqlBuilder sqlBuilder)
        {
            foreach (MemberAssignment memberAss in expression.Bindings)
            {

            }
            return sqlBuilder;
        }
    }
}
