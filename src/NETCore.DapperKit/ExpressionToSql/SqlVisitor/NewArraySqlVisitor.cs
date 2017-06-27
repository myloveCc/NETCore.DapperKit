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
            var ins = new List<string>();
            foreach (var expressionItem in expression.Expressions)
            {
                if (expressionItem is ConstantExpression)
                {
                    var constantExp = expressionItem as ConstantExpression;
                    if (constantExp.Type.Name == "String")
                    {
                        ins.Add($"'{constantExp.Value}'");
                    }
                    else
                    {
                        ins.Add($"{constantExp.Value}");
                    }
                }
            }
            sqlBuilder.AppendWhereSql($"({string.Join(",", ins)}) ");

            return sqlBuilder;
        }
    }
}
