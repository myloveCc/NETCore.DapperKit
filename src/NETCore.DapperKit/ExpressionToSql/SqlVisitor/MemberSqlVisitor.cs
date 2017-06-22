using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Reflection;
using NETCore.DapperKit.ExpressionToSql.Core;
using NETCore.DapperKit.Extensions;

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
                var name = propertyInfo.Name;
                columns.Add($"{sqlBuilder._SqlFormater.Left}{name}{sqlBuilder._SqlFormater.Right}");

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
            var columnName = $"{tableAlias}{sqlBuilder._SqlFormater.Left}{expression.Member.Name}{sqlBuilder._SqlFormater.Right}";
            //get data column name
            var fieldName = $"{sqlBuilder._SqlFormater.Left}{expression.Member.Name}{sqlBuilder._SqlFormater.Right}";

            sqlBuilder.AppendSelectSql($"{columnName} {fieldName} ");

            return sqlBuilder;
        }

        protected override ISqlBuilder Join(MemberExpression expression, ISqlBuilder sqlBuilder)
        {
            var tableAlias = GetTableAlias(expression, sqlBuilder);
            sqlBuilder.AppendJoinSql($"{tableAlias}{sqlBuilder._SqlFormater.Left}{expression.Member.Name}{sqlBuilder._SqlFormater.Right} ");
            return sqlBuilder;
        }

        protected override ISqlBuilder Where(MemberExpression expression, ISqlBuilder sqlBuilder)
        {
            var tableAlias = GetTableAlias(expression, sqlBuilder);
            sqlBuilder.AppendWhereSql($"{tableAlias}{sqlBuilder._SqlFormater.Left}{expression.Member.Name}{sqlBuilder._SqlFormater.Right} ");
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


        protected override ISqlBuilder Delete(MemberExpression expression, ISqlBuilder sqlBuilder)
        {
            sqlBuilder.AppendWhereSql($"{sqlBuilder._SqlFormater.Left}{expression.Member.Name}{sqlBuilder._SqlFormater.Right} ");

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
