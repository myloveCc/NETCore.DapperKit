
using NETCore.DapperKit.ExpressionToSql.Core;
using NETCore.DapperKit.Extensions;
using System;
using System.Linq.Expressions;
using System.Reflection;

namespace NETCore.DapperKit.ExpressionToSql.SqlVisitor
{
    class BinarySqlVisitor : BaseSqlVisitor<BinaryExpression>
    {
        private string OperatorParser(ExpressionType expressionNodeType, bool useIs = false)
        {
            switch (expressionNodeType)
            {
                case ExpressionType.And:
                case ExpressionType.AndAlso:
                    return "AND ";
                case ExpressionType.Equal:
                    if (useIs)
                    {
                        return "IS ";
                    }
                    else
                    {
                        return "= ";
                    }
                case ExpressionType.GreaterThan:
                    return "> ";
                case ExpressionType.GreaterThanOrEqual:
                    return ">= ";
                case ExpressionType.NotEqual:
                    if (useIs)
                    {
                        return "IS NOT";
                    }
                    else
                    {
                        return " <>";
                    }
                case ExpressionType.Or:
                case ExpressionType.OrElse:
                    return "OR ";
                case ExpressionType.LessThan:
                    return "< ";
                case ExpressionType.LessThanOrEqual:
                    return "<= ";
                default:
                    throw new NotImplementedException("Unimplemented expressionType:" + expressionNodeType);
            }
        }

        private bool IsNodeTypeSetValue(ExpressionType expressionNodeType)
        {
            switch (expressionNodeType)
            {
                case ExpressionType.And:
                case ExpressionType.AndAlso:
                case ExpressionType.Or:
                case ExpressionType.OrElse:
                    return false;
                case ExpressionType.Equal:
                case ExpressionType.GreaterThan:
                case ExpressionType.GreaterThanOrEqual:
                case ExpressionType.NotEqual:
                case ExpressionType.LessThan:
                case ExpressionType.LessThanOrEqual:
                    return true;

                default:
                    throw new NotImplementedException("Unimplemented expressionType:" + expressionNodeType);
            }
        }

        private static int GetOperatorPrecedence(Expression node)
        {
            switch (node.NodeType)
            {
                case ExpressionType.Add:
                case ExpressionType.AddChecked:
                case ExpressionType.Subtract:
                case ExpressionType.SubtractChecked:
                    return 10;
                case ExpressionType.And:
                    return 6;
                case ExpressionType.AndAlso:
                    return 3;
                case ExpressionType.Coalesce:
                case ExpressionType.Assign:
                case ExpressionType.AddAssign:
                case ExpressionType.AndAssign:
                case ExpressionType.DivideAssign:
                case ExpressionType.ExclusiveOrAssign:
                case ExpressionType.LeftShiftAssign:
                case ExpressionType.ModuloAssign:
                case ExpressionType.MultiplyAssign:
                case ExpressionType.OrAssign:
                case ExpressionType.PowerAssign:
                case ExpressionType.RightShiftAssign:
                case ExpressionType.SubtractAssign:
                case ExpressionType.AddAssignChecked:
                case ExpressionType.MultiplyAssignChecked:
                case ExpressionType.SubtractAssignChecked:
                    return 1;
                case ExpressionType.Constant:
                case ExpressionType.Parameter:
                    return 15;
                case ExpressionType.Convert:
                case ExpressionType.ConvertChecked:
                case ExpressionType.Negate:
                case ExpressionType.UnaryPlus:
                case ExpressionType.NegateChecked:
                case ExpressionType.Not:
                case ExpressionType.Decrement:
                case ExpressionType.Increment:
                case ExpressionType.Throw:
                case ExpressionType.Unbox:
                case ExpressionType.PreIncrementAssign:
                case ExpressionType.PreDecrementAssign:
                case ExpressionType.OnesComplement:
                case ExpressionType.IsTrue:
                case ExpressionType.IsFalse:
                    return 12;
                case ExpressionType.Divide:
                case ExpressionType.Modulo:
                case ExpressionType.Multiply:
                case ExpressionType.MultiplyChecked:
                    return 11;
                case ExpressionType.Equal:
                case ExpressionType.NotEqual:
                    return 7;
                case ExpressionType.ExclusiveOr:
                    return 5;
                case ExpressionType.GreaterThan:
                case ExpressionType.GreaterThanOrEqual:
                case ExpressionType.LessThan:
                case ExpressionType.LessThanOrEqual:
                case ExpressionType.TypeAs:
                case ExpressionType.TypeIs:
                case ExpressionType.TypeEqual:
                    return 8;
                case ExpressionType.LeftShift:
                case ExpressionType.RightShift:
                    return 9;
                case ExpressionType.Or:
                    return 4;
                case ExpressionType.OrElse:
                    return 2;
                case ExpressionType.Power:
                    return 13;
            }
            return 14;
        }

        private static bool NeedsParenthesesLast(Expression parent, Expression child)
        {
            BinaryExpression binaryExpression = parent as BinaryExpression;
            return binaryExpression != null && child == binaryExpression.Right;
        }

        private static bool NeedsParenthesesPrecedence(Expression parent, Expression child)
        {
            int operatorPrecedenceChild = GetOperatorPrecedence(child);
            int operatorPrecedenceParent = GetOperatorPrecedence(parent);
            if (operatorPrecedenceChild == operatorPrecedenceParent)
            {
                var nodeType = parent.NodeType;
                if (nodeType <= ExpressionType.MultiplyChecked)
                {
                    if (nodeType <= ExpressionType.Divide)
                    {
                        switch (nodeType)
                        {
                            case ExpressionType.Add:
                            case ExpressionType.AddChecked:
                                break;
                            case ExpressionType.And:
                            case ExpressionType.AndAlso:
                                return false;
                            default:
                                if (nodeType != ExpressionType.Divide)
                                {
                                    return true;
                                }
                                return NeedsParenthesesLast(parent, child);
                        }
                    }
                    else
                    {
                        if (nodeType == ExpressionType.ExclusiveOr)
                        {
                            return false;
                        }
                        switch (nodeType)
                        {
                            case ExpressionType.Modulo:
                                return NeedsParenthesesLast(parent, child);
                            case ExpressionType.Multiply:
                            case ExpressionType.MultiplyChecked:
                                break;
                            default:
                                return true;
                        }
                    }
                    return false;
                }
                if (nodeType <= ExpressionType.OrElse)
                {
                    if (nodeType != ExpressionType.Or && nodeType != ExpressionType.OrElse)
                    {
                        return true;
                    }
                }
                else
                {
                    if (nodeType != ExpressionType.Subtract && nodeType != ExpressionType.SubtractChecked)
                    {
                        return true;
                    }
                    return NeedsParenthesesLast(parent, child);
                }
                return false;
            }
            return (child.NodeType == ExpressionType.Constant && (parent.NodeType == ExpressionType.Negate || parent.NodeType == ExpressionType.NegateChecked)) || operatorPrecedenceChild < operatorPrecedenceParent;
        }

        private static bool IsNeedsParentheses(Expression parent, Expression child)
        {
            if (child == null)
            {
                return false;
            }
            ExpressionType nodeType = parent.NodeType;
            if (nodeType <= ExpressionType.Increment)
            {
                if (nodeType != ExpressionType.Decrement && nodeType != ExpressionType.Increment)
                {
                    return NeedsParenthesesPrecedence(parent, child);
                }
            }
            else if (nodeType != ExpressionType.Unbox && nodeType != ExpressionType.IsTrue && nodeType != ExpressionType.IsFalse)
            {
                return NeedsParenthesesPrecedence(parent, child);
            }
            return true;
        }

        protected override ISqlBuilder Join(BinaryExpression expression, ISqlBuilder sqlBuilder)
        {
            var expressionLeft = expression.Left;
            var expressionRight = expression.Right;

            if (expressionLeft is MemberExpression)
            {
                var memberExp = expressionLeft as MemberExpression;
                if (!MemberIsDataColumn(memberExp, sqlBuilder))
                    throw new NotImplementedException($"{memberExp.Member.Name} is not data column");
            }
            if (expressionRight is MemberExpression)
            {
                var memberExp = expressionRight as MemberExpression;
                if (!MemberIsDataColumn(memberExp, sqlBuilder))
                    throw new NotImplementedException($"{memberExp.Member.Name} is not data column");
            }

            var isRightHandle = false;

            if (IsNeedsParentheses(expression, expressionLeft))
            {
                sqlBuilder.AppendJoinSql("( ");
            }

            if (expressionLeft is MemberExpression && expressionLeft.Type == typeof(bool) && expressionLeft.NodeType == ExpressionType.MemberAccess && (expression.NodeType == ExpressionType.AndAlso || expressionRight is ConstantExpression))
            {
                SqlVistorProvider.Join(expressionLeft, sqlBuilder);

                sqlBuilder.AppendJoinSql($"{OperatorParser(ExpressionType.Equal)}");

                var sqlParamName = string.Empty;
                if (expressionRight is ConstantExpression)
                {
                    isRightHandle = true;
                    var value = Convert.ToBoolean(((ConstantExpression)expressionRight).Value) ? 1 : 0;
                    sqlParamName = sqlBuilder.SetSqlParameter(value);
                }
                else
                {
                    sqlParamName = sqlBuilder.SetSqlParameter(1);
                }

                sqlBuilder.AppendJoinSql($"{sqlParamName} ");
            }
            else
            {
                SqlVistorProvider.Join(expression.Left, sqlBuilder);
            }

            if (IsNeedsParentheses(expression, expressionLeft))
            {
                sqlBuilder.AppendJoinSql(") ");
            }

            if (!isRightHandle)
            {
                var whereOperator = string.Empty;
                if ((expressionRight is ConstantExpression) && ((ConstantExpression)expressionRight).Value == null)
                {
                    whereOperator = OperatorParser(expression.NodeType, true);
                }
                else
                {
                    whereOperator = OperatorParser(expression.NodeType);
                }
                sqlBuilder.AppendJoinSql($"{whereOperator}");

                if (IsNeedsParentheses(expression, expressionRight))
                {
                    sqlBuilder.AppendJoinSql("( ");
                }

                if (expressionRight is MemberExpression && expressionRight.Type == typeof(bool) && expressionRight.NodeType == ExpressionType.MemberAccess && expression.NodeType == ExpressionType.AndAlso)
                {
                    //（m=>m.IsAdmin) 
                    SqlVistorProvider.Join(expressionRight, sqlBuilder);

                    sqlBuilder.AppendJoinSql($"{OperatorParser(ExpressionType.Equal)}");

                    var sqlParamName = sqlBuilder.SetSqlParameter(1);
                    sqlBuilder.AppendJoinSql($"{sqlParamName} ");
                }
                else if(expressionRight is MemberExpression && IsNodeTypeSetValue(expression.NodeType))
                {
                    var memberExp = expressionRight as MemberExpression;
                    var value = TryGetExpreesionValue(memberExp);

                    if (value != null)
                    {
                        var sqlParamName = sqlBuilder.SetSqlParameter(value);
                        sqlBuilder.AppendWhereSql($"{sqlParamName} ");
                    }
                    else
                    {
                        SqlVistorProvider.Join(expression.Right, sqlBuilder);
                    }
                }
                else
                {
                    SqlVistorProvider.Join(expression.Right, sqlBuilder);
                }

                if (IsNeedsParentheses(expression, expressionRight))
                {
                    sqlBuilder.AppendWhereSql(") ");
                }
            }

            return sqlBuilder;
        }

        protected override ISqlBuilder Where(BinaryExpression expression, ISqlBuilder sqlBuilder)
        {
            var expressionLeft = expression.Left;
            var expressionRight = expression.Right;

            if (expressionLeft is MemberExpression)
            {
                var memberExp = expressionLeft as MemberExpression;
                if (!MemberIsDataColumn(memberExp, sqlBuilder))
                    throw new NotImplementedException($"{memberExp.Member.Name} is not data column");
            }
            if (expressionRight is MemberExpression)
            {
                var memberExp = expressionRight as MemberExpression;
                if (!MemberIsDataColumn(memberExp, sqlBuilder))
                    throw new NotImplementedException($"{memberExp.Member.Name} is not data column");
            }

            var isRightHandle = false;

            if (IsNeedsParentheses(expression, expressionLeft))
            {
                sqlBuilder.AppendWhereSql("( ");
            }

            if (expressionLeft is MemberExpression && expressionLeft.Type == typeof(bool) && expressionLeft.NodeType == ExpressionType.MemberAccess && (expression.NodeType == ExpressionType.AndAlso || expressionRight is ConstantExpression))
            {
                SqlVistorProvider.Where(expressionLeft, sqlBuilder);

                sqlBuilder.AppendWhereSql($"{OperatorParser(ExpressionType.Equal)}");

                var sqlParamName = string.Empty;
                if (expressionRight is ConstantExpression)
                {
                    isRightHandle = true;
                    var value = Convert.ToBoolean(((ConstantExpression)expressionRight).Value) ? 1 : 0;
                    sqlParamName = sqlBuilder.SetSqlParameter(value);
                }
                else
                {
                    sqlParamName = sqlBuilder.SetSqlParameter(1);
                }

                sqlBuilder.AppendWhereSql($"{sqlParamName} ");
            }
            else
            {
                SqlVistorProvider.Where(expression.Left, sqlBuilder);
            }

            if (IsNeedsParentheses(expression, expressionLeft))
            {
                sqlBuilder.AppendWhereSql(") ");
            }

            if (!isRightHandle)
            {
                var whereOperator = string.Empty;
                if ((expressionRight is ConstantExpression) && ((ConstantExpression)expressionRight).Value == null)
                {
                    whereOperator = OperatorParser(expression.NodeType, true);
                }
                else
                {
                    whereOperator = OperatorParser(expression.NodeType);
                }
                sqlBuilder.AppendWhereSql($"{whereOperator}");

                if (IsNeedsParentheses(expression, expressionRight))
                {
                    sqlBuilder.AppendWhereSql("( ");
                }

                if (expressionRight is MemberExpression && expressionRight.Type == typeof(bool) && expression.NodeType == ExpressionType.AndAlso)
                {
                    //（m=>m.IsAdmin) 
                    SqlVistorProvider.Where(expressionRight, sqlBuilder);

                    sqlBuilder.AppendWhereSql($"{OperatorParser(ExpressionType.Equal)}");

                    var sqlParamName = sqlBuilder.SetSqlParameter(1);
                    sqlBuilder.AppendWhereSql($"{sqlParamName} ");
                }
                else if (expressionRight is MemberExpression && IsNodeTypeSetValue(expression.NodeType))
                {
                    var memberExp = expressionRight as MemberExpression;
                    var value = TryGetExpreesionValue(memberExp);
                    if (value != null)
                    {
                        var sqlParamName = sqlBuilder.SetSqlParameter(value);
                        sqlBuilder.AppendWhereSql($"{sqlParamName} ");
                    }
                    else
                    {
                        SqlVistorProvider.Join(expression.Right, sqlBuilder);
                    }

                }
                else
                {
                    SqlVistorProvider.Where(expression.Right, sqlBuilder);
                }

                if (IsNeedsParentheses(expression, expressionRight))
                {
                    sqlBuilder.AppendWhereSql(") ");
                }
            }

            return sqlBuilder;
        }

        protected override ISqlBuilder Delete(BinaryExpression expression, ISqlBuilder sqlBuilder)
        {
            var expressionLeft = expression.Left;
            var expressionRight = expression.Right;

            if (expressionLeft is MemberExpression)
            {
                var memberExp = expressionLeft as MemberExpression;
                if (!MemberIsDataColumn(memberExp, sqlBuilder))
                    throw new NotImplementedException($"{memberExp.Member.Name} is not data column");
            }
            if (expressionRight is MemberExpression)
            {
                var memberExp = expressionRight as MemberExpression;
                if (!MemberIsDataColumn(memberExp, sqlBuilder))
                    throw new NotImplementedException($"{memberExp.Member.Name} is not data column");
            }

            var isRightHandle = false;

            if (IsNeedsParentheses(expression, expressionLeft))
            {
                sqlBuilder.AppendWhereSql("( ");
            }

            if (expressionLeft is MemberExpression && expressionLeft.Type == typeof(bool) && expressionLeft.NodeType == ExpressionType.MemberAccess && (expression.NodeType == ExpressionType.AndAlso || expressionRight is ConstantExpression))
            {
                SqlVistorProvider.Where(expressionLeft, sqlBuilder);
                sqlBuilder.AppendWhereSql($"{OperatorParser(ExpressionType.Equal)}");

                var sqlParamName = string.Empty;
                //（m=>m.IsAdmin==true) 
                if (expressionRight is ConstantExpression)
                {
                    isRightHandle = true;
                    var value = Convert.ToBoolean(((ConstantExpression)expressionRight).Value) ? 1 : 0;
                    sqlParamName = sqlBuilder.SetSqlParameter(value);
                }
                else
                {
                    //（m=>m.IsAdmin) 
                    sqlParamName = sqlBuilder.SetSqlParameter(1);
                }

                sqlBuilder.AppendWhereSql($"{sqlParamName} ");
            }
            else
            {
                SqlVistorProvider.Delete(expression.Left, sqlBuilder);
            }

            if (IsNeedsParentheses(expression, expressionLeft))
            {
                sqlBuilder.AppendWhereSql(") ");
            }

            if (!isRightHandle)
            {
                var whereOperator = string.Empty;
                if ((expressionRight is ConstantExpression) && ((ConstantExpression)expressionRight).Value == null)
                {
                    whereOperator = OperatorParser(expression.NodeType, true);
                }
                else
                {
                    whereOperator = OperatorParser(expression.NodeType);
                }
                sqlBuilder.AppendWhereSql($"{whereOperator}");

                if (IsNeedsParentheses(expression, expressionRight))
                {
                    sqlBuilder.AppendWhereSql("( ");
                }

                if (expressionRight is MemberExpression && expressionRight.Type == typeof(bool) && expression.NodeType == ExpressionType.AndAlso)
                {
                    //（m=>m.IsAdmin) 
                    SqlVistorProvider.Where(expressionRight, sqlBuilder);

                    sqlBuilder.AppendWhereSql($"{OperatorParser(ExpressionType.Equal)}");

                    var sqlParamName = sqlBuilder.SetSqlParameter(1);
                    sqlBuilder.AppendWhereSql($"{sqlParamName} ");
                }
                else if (expressionRight is MemberExpression && IsNodeTypeSetValue(expression.NodeType))
                {
                    var memberExp = expressionRight as MemberExpression;
                    var value = TryGetExpreesionValue(memberExp);
                    if (value != null)
                    {
                        var sqlParamName = sqlBuilder.SetSqlParameter(value);
                        sqlBuilder.AppendWhereSql($"{sqlParamName} ");
                    }
                    else
                    {
                        SqlVistorProvider.Join(expression.Right, sqlBuilder);
                    }
                }
                else
                {
                    SqlVistorProvider.Delete(expression.Right, sqlBuilder);
                }

                if (IsNeedsParentheses(expression, expressionRight))
                {
                    sqlBuilder.AppendWhereSql(") ");
                }
            }

            return sqlBuilder;
        }
    }
}