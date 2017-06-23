using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Reflection;
using NETCore.DapperKit.ExpressionToSql.Core;
using NETCore.DapperKit.ExpressionToSql.Extensions;
using NETCore.DapperKit.Extensions;
using System.Collections;
using System.Linq;

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
            List<string> columns = new List<string>();
            List<string> parames = new List<string>();

            object entity = GetValue(expression);
            var properties = expression.Type.GetDataCloumnProperties();

            foreach (var propertyInfo in properties)
            {
                if (propertyInfo.IsKeyProperty(expression.Type))
                {
                    continue;
                }

                //columns
                var columnName = propertyInfo.Name;
                columns.Add($"{sqlBuilder.Formate(columnName)}");

                var value = propertyInfo.GetValue(entity, null);
                //formater bool value to true:1 false:0
                if (value is bool)
                {
                    if ((bool)value)
                    {
                        value = 1;
                    }
                    else
                    {
                        value = 0;
                    }
                }
                string sqlParamName = sqlBuilder.SetSqlParameter(value);
                //params
                parames.Add($"{sqlParamName}");
            }

            sqlBuilder.AppendInsertSql($"({string.Join(",", columns)}) VALUES ({string.Join(",", parames)})");

            return sqlBuilder;
        }

        protected override ISqlBuilder Select(MemberExpression expression, ISqlBuilder sqlBuilder)
        {

            PropertyInfo propertyInfo = expression.Member as PropertyInfo;
            if (!propertyInfo.IsDataConlumnProperty(expression.Member.DeclaringType))
            {
                throw new Exception($"{ expression.Member.Name } is not data column");
            }

            var tableAlias = GetTableAlias(expression, sqlBuilder);
            //get table column name
            var columnName = $"{tableAlias}{sqlBuilder.Formate(expression.Member.Name)}";
            //get data column name
            var fieldName = $"{sqlBuilder.Formate(expression.Member.Name)}";

            sqlBuilder.AddSelectColumn($"{columnName} {fieldName} ");

            return sqlBuilder;
        }

        protected override ISqlBuilder Join(MemberExpression expression, ISqlBuilder sqlBuilder)
        {
            var tableAlias = GetTableAlias(expression, sqlBuilder);
            sqlBuilder.AppendJoinSql($"{tableAlias}{sqlBuilder.Formate(expression.Member.Name)} ");
            return sqlBuilder;
        }

        protected override ISqlBuilder Where(MemberExpression expression, ISqlBuilder sqlBuilder)
        {
            var tableAlias = GetTableAlias(expression, sqlBuilder);
            sqlBuilder.AppendWhereSql($"{tableAlias}{sqlBuilder.Formate(expression.Member.Name)} ");
            return sqlBuilder;
        }

        protected override ISqlBuilder In(MemberExpression expression, ISqlBuilder sqlBuilder)
        {
            var field = expression.Member as FieldInfo;
            if (field != null)
            {
                object val = field.GetValue(((ConstantExpression)expression.Expression).Value);

                if (val != null)
                {
                    var ins = new List<string>();
                    IEnumerable array = val as IEnumerable;
                    foreach (var item in array)
                    {
                        if (field.FieldType.Name == "String[]" || field.FieldType == typeof(List<string>))
                        {
                            ins.Add($"'{item}'");
                        }
                        else
                        {
                            ins.Add($"{item}");
                        }
                    }

                    if (ins.Any())
                    {
                        sqlBuilder.AppendWhereSql($"({string.Join(",", ins)})");
                    }
                }
            }
            return sqlBuilder;
        }

        protected override ISqlBuilder GroupBy(MemberExpression expression, ISqlBuilder sqlBuilder)
        {
            var tableAlias = GetTableAlias(expression, sqlBuilder);

            var columnAlias = $"{tableAlias}{sqlBuilder.Formate(expression.Member.Name)} ";
            sqlBuilder.AddCalculateColumn(columnAlias);
            sqlBuilder.AppendGroupSql(columnAlias);
            return sqlBuilder;
        }

        protected override ISqlBuilder OrderBy(MemberExpression expression, ISqlBuilder sqlBuilder)
        {
            var tableAlias = GetTableAlias(expression, sqlBuilder);
            sqlBuilder.AppendOrderSql($"{tableAlias}{sqlBuilder.Formate(expression.Member.Name)} ASC ");

            return sqlBuilder;
        }

        protected override ISqlBuilder ThenBy(MemberExpression expression, ISqlBuilder sqlBuilder)
        {
            var tableAlias = GetTableAlias(expression, sqlBuilder);
            sqlBuilder.AppendOrderSql($"{tableAlias}{sqlBuilder.Formate(expression.Member.Name)} ASC ");

            return sqlBuilder;
        }

        protected override ISqlBuilder OrderByDescending(MemberExpression expression, ISqlBuilder sqlBuilder)
        {
            var tableAlias = GetTableAlias(expression, sqlBuilder);
            sqlBuilder.AppendOrderSql($"{tableAlias}{sqlBuilder.Formate(expression.Member.Name)} DESC ");

            return sqlBuilder;
        }

        protected override ISqlBuilder ThenByDescending(MemberExpression expression, ISqlBuilder sqlBuilder)
        {
            var tableAlias = GetTableAlias(expression, sqlBuilder);
            sqlBuilder.AppendOrderSql($"{tableAlias}{sqlBuilder.Formate(expression.Member.Name)} DESC ");

            return sqlBuilder;
        }

        protected override ISqlBuilder Delete(MemberExpression expression, ISqlBuilder sqlBuilder)
        {
            sqlBuilder.AppendWhereSql($"{sqlBuilder.Formate(expression.Member.Name)} ");

            return sqlBuilder;
        }

        private ISqlBuilder AggregateFunctionParser(MemberExpression expression, ISqlBuilder sqlBuilder, string functionName)
        {
            string columnName = expression.Member.Name;
            sqlBuilder.AddCalculateColumn($"{functionName}({sqlBuilder.Formate(columnName)})");
            return sqlBuilder;
        }

        protected override ISqlBuilder Max(MemberExpression expression, ISqlBuilder sqlBuilder)
        {
            return AggregateFunctionParser(expression, sqlBuilder, "MAX");
        }

        protected override ISqlBuilder Min(MemberExpression expression, ISqlBuilder sqlBuilder)
        {
            return AggregateFunctionParser(expression, sqlBuilder, "MIN");
        }

        protected override ISqlBuilder Avg(MemberExpression expression, ISqlBuilder sqlBuilder)
        {
            return AggregateFunctionParser(expression, sqlBuilder, "AVG");
        }

        protected override ISqlBuilder Count(MemberExpression expression, ISqlBuilder sqlBuilder)
        {
            return AggregateFunctionParser(expression, sqlBuilder, "COUNT");
        }

        protected override ISqlBuilder Sum(MemberExpression expression, ISqlBuilder sqlBuilder)
        {
            return AggregateFunctionParser(expression, sqlBuilder, "SUM");
        }
    }
}
