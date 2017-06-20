using NETCore.DapperKit.ExpressionToSql.Core;
using System;
using System.Collections.Generic;
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

        }

        private static void Like(MethodCallExpression expression, ISqlBuilder sqlBuilder)
        {
        }

        private static void LikeLeft(MethodCallExpression expression, ISqlBuilder sqlBuilder)
        {

        }

        private static void LikeRight(MethodCallExpression expression, ISqlBuilder sqlBuilder)
        {

        }

        protected override ISqlBuilder Where(MethodCallExpression expression, ISqlBuilder sqlBuilder)
        {

            throw new NotImplementedException("Unimplemented method:" + expression.Method);
        }
    }
}
