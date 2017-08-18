using NETCore.DapperKit.ExpressionVisitor.Core;
using NETCore.DapperKit.ExpressionVisitor.SqlVisitor.Interface;
using NETCore.DapperKit.Shared;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace NETCore.DapperKit.ExpressionVisitor.SqlVisitor
{
    internal class SqlVistorProvider
    {
        private static ISqlVisitor GetSqlManager(Expression expression)
        {
            if (expression == null)
            {
                Check.Argument.IsNotNull(expression, nameof(expression), "SqlVistorProvider expression must not null");
            }

            if (expression is BinaryExpression)
            {
                return new BinarySqlVisitor();
            }
            if (expression is ConstantExpression)
            {
                return new ConstantSqlVisitor();
            }
            if (expression is ListInitExpression)
            {
                return new ListInitSqlVisitor();
            }
            if (expression is MemberExpression)
            {
                return new MemberSqlVisitor();
            }
            if (expression is MemberInitExpression)
            {
                return new MemberInitSqlVisitor();
            }
            if (expression is MethodCallExpression)
            {
                return new MethodCallSqlVisitor();
            }
            if (expression is NewArrayExpression)
            {
                return new NewArraySqlVisitor();
            }
            if (expression is NewExpression)
            {
                return new NewSqlVisitor();
            }
            if (expression is ParameterExpression)
            {
                return new ParameterSqlVisitor();
            }
            if (expression is UnaryExpression)
            {
                return new UnarySqlVisitor();
            }
            if (expression is DebugInfoExpression)
            {
                throw new NotImplementedException("Unimplemented DebugInfoExpressionSqlManager");
            }
            if (expression is DefaultExpression)
            {
                throw new NotImplementedException("Unimplemented DefaultExpressionSqlManager");
            }
            if (expression is DynamicExpression)
            {
                throw new NotImplementedException("Unimplemented DynamicExpressionSqlManager");
            }
            if (expression is GotoExpression)
            {
                throw new NotImplementedException("Unimplemented GotoExpressionSqlManager");
            }
            if (expression is IndexExpression)
            {
                throw new NotImplementedException("Unimplemented IndexExpressionSqlManager");
            }
            if (expression is InvocationExpression)
            {
                throw new NotImplementedException("Unimplemented InvocationExpressionSqlManager");
            }
            if (expression is LabelExpression)
            {
                throw new NotImplementedException("Unimplemented LabelExpressionSqlManager");
            }
            if (expression is LambdaExpression)
            {
                throw new NotImplementedException("Unimplemented LambdaExpressionSqlManager");
            }
            if (expression is LoopExpression)
            {
                throw new NotImplementedException("Unimplemented LoopExpressionSqlManager");
            }
            if (expression is RuntimeVariablesExpression)
            {
                throw new NotImplementedException("Unimplemented RuntimeVariablesExpressionSqlManager");
            }
            if (expression is SwitchExpression)
            {
                throw new NotImplementedException("Unimplemented SwitchExpressionSqlManager");
            }
            if (expression is TryExpression)
            {
                throw new NotImplementedException("Unimplemented TryExpressionSqlManager");
            }
            if (expression is TypeBinaryExpression)
            {
                throw new NotImplementedException("Unimplemented TypeBinaryExpressionSqlManager");
            }
            if (expression is BlockExpression)
            {
                throw new NotImplementedException("Unimplemented BlockExpressionSqlManager");
            }
            if (expression is ConditionalExpression)
            {
                throw new NotImplementedException("Unimplemented ConditionalExpressionSqlManager");
            }
            throw new NotImplementedException("Unimplemented ExpressionSqlManager");
        }

        internal static void Insert(Expression expression, ISqlBuilder sqlBuilder)
        {
            GetSqlManager(expression).Insert(expression, sqlBuilder);
        }

        internal static void Update(Expression expression, ISqlBuilder sqlBuilder)
        {
            GetSqlManager(expression).Update(expression, sqlBuilder);
        }

        internal static void Select(Expression expression, ISqlBuilder sqlBuilder)
        {
            GetSqlManager(expression).Select(expression, sqlBuilder);
        }

        internal static void Join(Expression expression, ISqlBuilder sqlBuilder)
        {
            GetSqlManager(expression).Join(expression, sqlBuilder);
        }

        internal static void Where(Expression expression, ISqlBuilder sqlBuilder)
        {
            GetSqlManager(expression).Where(expression, sqlBuilder);
        }

        internal static void In(Expression expression, ISqlBuilder sqlBuilder)
        {
            GetSqlManager(expression).In(expression, sqlBuilder);
        }

        internal static void GroupBy(Expression expression, ISqlBuilder sqlBuilder)
        {
            GetSqlManager(expression).GroupBy(expression, sqlBuilder);
        }

        internal static void OrderBy(Expression expression, ISqlBuilder sqlBuilder)
        {
            GetSqlManager(expression).OrderBy(expression, sqlBuilder);
        }
        internal static void ThenBy(Expression expression, ISqlBuilder sqlBuilder)
        {
            GetSqlManager(expression).ThenBy(expression, sqlBuilder);
        }
        internal static void OrderByDescending(Expression expression, ISqlBuilder sqlBuilder)
        {
            GetSqlManager(expression).OrderByDescending(expression, sqlBuilder);
        }
        internal static void ThenByDescending(Expression expression, ISqlBuilder sqlBuilder)
        {
            GetSqlManager(expression).ThenByDescending(expression, sqlBuilder);
        }

        internal static void Max(Expression expression, ISqlBuilder sqlBuilder)
        {
            GetSqlManager(expression).Max(expression, sqlBuilder);
        }

        internal static void Min(Expression expression, ISqlBuilder sqlBuilder)
        {
            GetSqlManager(expression).Min(expression, sqlBuilder);
        }

        internal static void Avg(Expression expression, ISqlBuilder sqlBuilder)
        {
            GetSqlManager(expression).Avg(expression, sqlBuilder);
        }

        internal static void Count(Expression expression, ISqlBuilder sqlBuilder)
        {
            GetSqlManager(expression).Count(expression, sqlBuilder);
        }

        public static void Sum(Expression expression, ISqlBuilder sqlBuilder)
        {
            GetSqlManager(expression).Sum(expression, sqlBuilder);
        }

        public static void Delete(Expression expression, ISqlBuilder sqlBuilder)
        {
            GetSqlManager(expression).Delete(expression, sqlBuilder);
        }

    }
}
