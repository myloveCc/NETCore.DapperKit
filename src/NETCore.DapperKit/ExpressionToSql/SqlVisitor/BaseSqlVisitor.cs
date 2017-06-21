using NETCore.DapperKit.ExpressionToSql.SqlVisitor.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using NETCore.DapperKit.ExpressionToSql.Core;
using System.Linq.Expressions;
using System.Reflection;
using NETCore.DapperKit.Extensions;

namespace NETCore.DapperKit.ExpressionToSql.SqlVisitor
{
    public class BaseSqlVisitor<T> : ISqlVisitor where T : Expression
    {
        protected virtual ISqlBuilder Insert(T expression, ISqlBuilder sqlBuilder)
        {
            throw new NotImplementedException("Unimplemented " + typeof(T).Name + "Sql.Insert method");
        }
        protected virtual ISqlBuilder Update(T expression, ISqlBuilder sqlBuilder)
        {
            throw new NotImplementedException("Unimplemented " + typeof(T).Name + "Sql.Update method");
        }
        protected virtual ISqlBuilder Select(T expression, ISqlBuilder sqlBuilder)
        {
            throw new NotImplementedException("Unimplemented " + typeof(T).Name + "Sql.Select method");
        }
        protected virtual ISqlBuilder Join(T expression, ISqlBuilder sqlBuilder)
        {
            throw new NotImplementedException("Unimplemented " + typeof(T).Name + "Sql.Join method");
        }
        protected virtual ISqlBuilder Where(T expression, ISqlBuilder sqlBuilder)
        {
            throw new NotImplementedException("Unimplemented " + typeof(T).Name + "Sql.Where method");
        }
        protected virtual ISqlBuilder In(T expression, ISqlBuilder sqlBuilder)
        {
            throw new NotImplementedException("Unimplemented " + typeof(T).Name + "Sql.In method");
        }
        protected virtual ISqlBuilder GroupBy(T expression, ISqlBuilder sqlBuilder)
        {
            throw new NotImplementedException("Unimplemented " + typeof(T).Name + "Sql.GroupBy method");
        }
        protected virtual ISqlBuilder OrderBy(T expression, ISqlBuilder sqlBuilder)
        {
            throw new NotImplementedException("Unimplemented " + typeof(T).Name + "Sql.OrderBy method");
        }
        protected virtual ISqlBuilder ThenBy(T expression, ISqlBuilder sqlBuilder)
        {
            throw new NotImplementedException("Unimplemented " + typeof(T).Name + "Sql.ThenBy method");
        }
        protected virtual ISqlBuilder OrderByDescending(T expression, ISqlBuilder sqlBuilder)
        {
            throw new NotImplementedException("Unimplemented " + typeof(T).Name + "Sql.OrderByDescending method");
        }
        protected virtual ISqlBuilder ThenByDescending(T expression, ISqlBuilder sqlBuilder)
        {
            throw new NotImplementedException("Unimplemented " + typeof(T).Name + "Sql.ThenByDescending method");
        }
        protected virtual ISqlBuilder Max(T expression, ISqlBuilder sqlBuilder)
        {
            throw new NotImplementedException("Unimplemented " + typeof(T).Name + "Sql.Max method");
        }
        protected virtual ISqlBuilder Min(T expression, ISqlBuilder sqlBuilder)
        {
            throw new NotImplementedException("Unimplemented " + typeof(T).Name + "Sql.Min method");
        }
        protected virtual ISqlBuilder Avg(T expression, ISqlBuilder sqlBuilder)
        {
            throw new NotImplementedException("Unimplemented " + typeof(T).Name + "Sql.Avg method");
        }
        protected virtual ISqlBuilder Count(T expression, ISqlBuilder sqlBuilder)
        {
            throw new NotImplementedException("Unimplemented " + typeof(T).Name + "Sql.Count method");
        }
        protected virtual ISqlBuilder Sum(T expression, ISqlBuilder sqlBuilder)
        {
            throw new NotImplementedException("Unimplemented " + typeof(T).Name + "Sql.Sum method");
        }
        protected virtual ISqlBuilder Delete(T expression, ISqlBuilder sqlBuilder)
        {
            throw new NotImplementedException("Unimplemented " + typeof(T).Name + "Sql.Delete method");
        }

        //insert
        public ISqlBuilder Insert(Expression expression, ISqlBuilder sqlBuilder)
        {
            return Insert((T)expression, sqlBuilder);
        }
        //update
        public ISqlBuilder Update(Expression expression, ISqlBuilder sqlBuilder)
        {
            return Update((T)expression, sqlBuilder);
        }
        //select
        public ISqlBuilder Select(Expression expression, ISqlBuilder sqlBuilder)
        {
            return Select((T)expression, sqlBuilder);
        }
        //join
        public ISqlBuilder Join(Expression expression, ISqlBuilder sqlBuilder)
        {
            return Join((T)expression, sqlBuilder);
        }
        //where
        public ISqlBuilder Where(Expression expression, ISqlBuilder sqlBuilder)
        {
            return Where((T)expression, sqlBuilder);
        }
        public ISqlBuilder In(Expression expression, ISqlBuilder sqlBuilder)
        {
            return In((T)expression, sqlBuilder);
        }
        //group
        public ISqlBuilder GroupBy(Expression expression, ISqlBuilder sqlBuilder)
        {
            return GroupBy((T)expression, sqlBuilder);
        }
        //oreder
        public ISqlBuilder OrderBy(Expression expression, ISqlBuilder sqlBuilder)
        {
            return OrderBy((T)expression, sqlBuilder);
        }
        public ISqlBuilder ThenBy(Expression expression, ISqlBuilder sqlBuilder)
        {
            return ThenBy((T)expression, sqlBuilder);
        }
        public ISqlBuilder OrderByDescending(Expression expression, ISqlBuilder sqlBuilder)
        {
            return OrderByDescending((T)expression, sqlBuilder);
        }
        public ISqlBuilder ThenByDescending(Expression expression, ISqlBuilder sqlBuilder)
        {
            return ThenByDescending((T)expression, sqlBuilder);
        }
        //calculate
        public ISqlBuilder Max(Expression expression, ISqlBuilder sqlBuilder)
        {
            return Max((T)expression, sqlBuilder);
        }
        public ISqlBuilder Min(Expression expression, ISqlBuilder sqlBuilder)
        {
            return Min((T)expression, sqlBuilder);
        }
        public ISqlBuilder Avg(Expression expression, ISqlBuilder sqlBuilder)
        {
            return Avg((T)expression, sqlBuilder);
        }
        public ISqlBuilder Count(Expression expression, ISqlBuilder sqlBuilder)
        {
            return Count((T)expression, sqlBuilder);
        }
        public ISqlBuilder Sum(Expression expression, ISqlBuilder sqlBuilder)
        {
            return Sum((T)expression, sqlBuilder);
        }

        public ISqlBuilder Delete(Expression expression, ISqlBuilder sqlBuilder)
        {
            return Delete((T)expression, sqlBuilder);
        }

        //common
        protected bool MemberIsDataColumn(MemberExpression expression, ISqlBuilder sqlBuilder)
        {
            var memberExpression = expression as MemberExpression;
            PropertyInfo property = memberExpression.Member as PropertyInfo;
            var type = memberExpression.Expression.Type;
            if (property.IsDataConlumnProperty(type))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        protected object GetExpreesionValue(Expression expression)
        {
            object value = null;
            if (expression is ConstantExpression)
            {
                ConstantExpression constranExp = expression as ConstantExpression;
                //bool
                value = constranExp.Value;
                if (constranExp.Type == typeof(bool))
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
            }
            else
            {
                LambdaExpression lambda = Expression.Lambda(expression);
                Delegate fn = lambda.Compile();
                ConstantExpression constantExp = Expression.Constant(fn.DynamicInvoke(null), expression.Type);
                value = constantExp.Value;
            }
            return value;
        }
    }
}
