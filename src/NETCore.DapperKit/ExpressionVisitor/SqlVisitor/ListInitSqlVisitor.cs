using NETCore.DapperKit.ExpressionVisitor.Core;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace NETCore.DapperKit.ExpressionVisitor.SqlVisitor
{
    public class ListInitSqlVisitor : BaseSqlVisitor<ListInitExpression>
    {
        protected override ISqlBuilder In(ListInitExpression expression, ISqlBuilder sqlBuilder)
        {
            var ins = new List<string>();
            foreach (var expressionInit in expression.Initializers)
            {
                foreach (var expressionItem in expressionInit.Arguments)
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
            }
            sqlBuilder.AppendWhereSql($"({string.Join(",", ins)}) ");
            return sqlBuilder;
        }
    }
}
