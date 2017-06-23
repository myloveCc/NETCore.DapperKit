using NETCore.DapperKit.ExpressionToSql.Core;
using NETCore.DapperKit.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace NETCore.DapperKit.ExpressionToSql.SqlVisitor
{
    public class MethodCallSqlVisitor : BaseSqlVisitor<MethodCallExpression>
    {
        static Dictionary<string, Action<MethodCallExpression, ISqlBuilder>> _Methods = new Dictionary<string, Action<MethodCallExpression, ISqlBuilder>>
        {
            {"Contains",Like },
            {"Like",Like},
            {"LikeLeft",LikeLeft},
            {"LikeRight",LikeRight},
            {"In",InnerIn}
        };

        private static void InnerIn(MethodCallExpression expression, ISqlBuilder sqlBuilder)
        {
            if (expression.Arguments != null && expression.Arguments.Any())
            {
                var mermberExp = expression.Arguments[0] as MemberExpression;

                if (MemberIsDataColumn(mermberExp, sqlBuilder))
                {
                    SqlVistorProvider.Where(mermberExp, sqlBuilder);
                    sqlBuilder.AppendWhereSql("IN ");
                    SqlVistorProvider.In(expression.Arguments[1], sqlBuilder);
                }
            }
        }

        private static void Like(MethodCallExpression expression, ISqlBuilder sqlBuilder)
        {
            if (expression.Object != null)
            {
                var mermberExp = expression.Object as MemberExpression;

                if (MemberIsDataColumn(mermberExp, sqlBuilder))
                {
                    SqlVistorProvider.Where(mermberExp, sqlBuilder);
                    sqlBuilder.AppendWhereSql("LIKE '%");

                    var valueExp = expression.Arguments[0];

                    if (valueExp is ConstantExpression)
                    {
                        var contantExp = valueExp as ConstantExpression;
                        sqlBuilder.AppendWhereSql($"{contantExp.Value}");
                    }
                    else
                    {
                        var memberExp = valueExp as MemberExpression;
                        var value = GetExpreesionValue(memberExp);
                        sqlBuilder.AppendWhereSql($"{value}");
                    }
                    sqlBuilder.AppendWhereSql("%' ");
                }
            }
            else
            {
                var mermberExp = expression.Arguments[0] as MemberExpression;

                if (MemberIsDataColumn(mermberExp, sqlBuilder))
                {
                    SqlVistorProvider.Where(mermberExp, sqlBuilder);
                    sqlBuilder.AppendWhereSql("LIKE '%");
                    var valueExp = expression.Arguments[1];
                    if (valueExp is ConstantExpression)
                    {
                        var contantExp = valueExp as ConstantExpression;
                        sqlBuilder.AppendWhereSql($"{contantExp.Value}");
                    }
                    else
                    {
                        var memberExp = valueExp as MemberExpression;
                        var value = GetExpreesionValue(memberExp);
                        sqlBuilder.AppendWhereSql($"{value}");
                    }
                    sqlBuilder.AppendWhereSql("%' ");
                }
            }
        }

        private static void LikeLeft(MethodCallExpression expression, ISqlBuilder sqlBuilder)
        {
            if (expression.Object != null)
            {
                var mermberExp = expression.Object as MemberExpression;

                if (MemberIsDataColumn(mermberExp, sqlBuilder))
                {
                    SqlVistorProvider.Where(mermberExp, sqlBuilder);
                    sqlBuilder.AppendWhereSql("LIKE '%");

                    var valueExp = expression.Arguments[0];

                    if (valueExp is ConstantExpression)
                    {
                        var contantExp = valueExp as ConstantExpression;
                        sqlBuilder.AppendWhereSql($"{contantExp.Value}");
                    }
                    else
                    {
                        var memberExp = valueExp as MemberExpression;
                        var value = GetExpreesionValue(memberExp);
                        sqlBuilder.AppendWhereSql($"{value}");
                    }
                    sqlBuilder.AppendWhereSql("' ");
                }
            }
            else
            {
                var mermberExp = expression.Arguments[0] as MemberExpression;

                if (MemberIsDataColumn(mermberExp, sqlBuilder))
                {
                    SqlVistorProvider.Where(mermberExp, sqlBuilder);
                    sqlBuilder.AppendWhereSql("LIKE '%");
                    var valueExp = expression.Arguments[1];
                    if (valueExp is ConstantExpression)
                    {
                        var contantExp = valueExp as ConstantExpression;
                        sqlBuilder.AppendWhereSql($"{contantExp.Value}");
                    }
                    else
                    {
                        var memberExp = valueExp as MemberExpression;
                        var value = GetExpreesionValue(memberExp);
                        sqlBuilder.AppendWhereSql($"{value}");
                    }
                    sqlBuilder.AppendWhereSql("' ");
                }
            }
        }

        private static void LikeRight(MethodCallExpression expression, ISqlBuilder sqlBuilder)
        {
            if (expression.Object != null)
            {
                var mermberExp = expression.Object as MemberExpression;

                if (MemberIsDataColumn(mermberExp, sqlBuilder))
                {
                    SqlVistorProvider.Where(mermberExp, sqlBuilder);
                    sqlBuilder.AppendWhereSql("LIKE '");

                    var valueExp = expression.Arguments[0];

                    if (valueExp is ConstantExpression)
                    {
                        var contantExp = valueExp as ConstantExpression;
                        sqlBuilder.AppendWhereSql($"{contantExp.Value}");
                    }
                    else
                    {
                        var memberExp = valueExp as MemberExpression;
                        var value = GetExpreesionValue(memberExp);
                        sqlBuilder.AppendWhereSql($"{value}");
                    }
                    sqlBuilder.AppendWhereSql("%' ");
                }
            }
            else
            {
                var mermberExp = expression.Arguments[0] as MemberExpression;

                if (MemberIsDataColumn(mermberExp, sqlBuilder))
                {
                    SqlVistorProvider.Where(mermberExp, sqlBuilder);
                    sqlBuilder.AppendWhereSql("LIKE '");
                    var valueExp = expression.Arguments[1];
                    if (valueExp is ConstantExpression)
                    {
                        var contantExp = valueExp as ConstantExpression;
                        sqlBuilder.AppendWhereSql($"{contantExp.Value}");
                    }
                    else
                    {
                        var memberExp = valueExp as MemberExpression;
                        var value = GetExpreesionValue(memberExp);
                        sqlBuilder.AppendWhereSql($"{value}");
                    }
                    sqlBuilder.AppendWhereSql("%' ");
                }
            }
        }


        protected override ISqlBuilder Where(MethodCallExpression expression, ISqlBuilder sqlBuilder)
        {
            var key = expression.Method;
            if (key.IsGenericMethod)
            {
                key = key.GetGenericMethodDefinition();
            }

            Action<MethodCallExpression, ISqlBuilder> action;
            if (_Methods.TryGetValue(key.Name, out action))
            {
                action(expression, sqlBuilder);
                return sqlBuilder;
            }

            throw new NotImplementedException("Unimplemented method:" + expression.Method);
        }

        protected override ISqlBuilder Delete(MethodCallExpression expression, ISqlBuilder sqlBuilder)
        {
            var value = GetExpreesionValue(expression);
            var sqlParamName = sqlBuilder.SetSqlParameter(value);

            sqlBuilder.AppendWhereSql($"{sqlParamName} ");
            return sqlBuilder;
        }
    }
}
