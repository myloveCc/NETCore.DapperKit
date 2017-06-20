using NETCore.DapperKit.ExpressionToSql.SqlVisitor.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using NETCore.DapperKit.ExpressionToSql.Core;
using System.Linq.Expressions;

namespace NETCore.DapperKit.ExpressionToSql.SqlVisitor
{
    public class BaseSqlVisitor<T> : ISqlVisitor where T : Expression
    {
        protected virtual ISqlBuilder Insert(T expression, ISqlBuilder ISqlBuilder)
        {
            throw new NotImplementedException("Unimplemented " + typeof(T).Name + "Sql.Insert method");
        }
        protected virtual ISqlBuilder Update(T expression, ISqlBuilder ISqlBuilder)
        {
            throw new NotImplementedException("Unimplemented " + typeof(T).Name + "Sql.Update method");
        }
        protected virtual ISqlBuilder Select(T expression, ISqlBuilder ISqlBuilder)
        {
            throw new NotImplementedException("Unimplemented " + typeof(T).Name + "Sql.Select method");
        }
        protected virtual ISqlBuilder Join(T expression, ISqlBuilder ISqlBuilder)
        {
            throw new NotImplementedException("Unimplemented " + typeof(T).Name + "Sql.Join method");
        }
        protected virtual ISqlBuilder Where(T expression, ISqlBuilder ISqlBuilder)
        {
            throw new NotImplementedException("Unimplemented " + typeof(T).Name + "Sql.Where method");
        }
        protected virtual ISqlBuilder In(T expression, ISqlBuilder ISqlBuilder)
        {
            throw new NotImplementedException("Unimplemented " + typeof(T).Name + "Sql.In method");
        }
        protected virtual ISqlBuilder GroupBy(T expression, ISqlBuilder ISqlBuilder)
        {
            throw new NotImplementedException("Unimplemented " + typeof(T).Name + "Sql.GroupBy method");
        }
        protected virtual ISqlBuilder OrderBy(T expression, ISqlBuilder ISqlBuilder)
        {
            throw new NotImplementedException("Unimplemented " + typeof(T).Name + "Sql.OrderBy method");
        }
        protected virtual ISqlBuilder ThenBy(T expression, ISqlBuilder ISqlBuilder)
        {
            throw new NotImplementedException("Unimplemented " + typeof(T).Name + "Sql.ThenBy method");
        }
        protected virtual ISqlBuilder OrderByDescending(T expression, ISqlBuilder ISqlBuilder)
        {
            throw new NotImplementedException("Unimplemented " + typeof(T).Name + "Sql.OrderByDescending method");
        }
        protected virtual ISqlBuilder ThenByDescending(T expression, ISqlBuilder ISqlBuilder)
        {
            throw new NotImplementedException("Unimplemented " + typeof(T).Name + "Sql.ThenByDescending method");
        }
        protected virtual ISqlBuilder Max(T expression, ISqlBuilder ISqlBuilder)
        {
            throw new NotImplementedException("Unimplemented " + typeof(T).Name + "Sql.Max method");
        }
        protected virtual ISqlBuilder Min(T expression, ISqlBuilder ISqlBuilder)
        {
            throw new NotImplementedException("Unimplemented " + typeof(T).Name + "Sql.Min method");
        }
        protected virtual ISqlBuilder Avg(T expression, ISqlBuilder ISqlBuilder)
        {
            throw new NotImplementedException("Unimplemented " + typeof(T).Name + "Sql.Avg method");
        }
        protected virtual ISqlBuilder Count(T expression, ISqlBuilder ISqlBuilder)
        {
            throw new NotImplementedException("Unimplemented " + typeof(T).Name + "Sql.Count method");
        }
        protected virtual ISqlBuilder Sum(T expression, ISqlBuilder ISqlBuilder)
        {
            throw new NotImplementedException("Unimplemented " + typeof(T).Name + "Sql.Sum method");
        }
        protected virtual ISqlBuilder Delete(T expression, ISqlBuilder ISqlBuilder)
        {
            throw new NotImplementedException("Unimplemented " + typeof(T).Name + "Sql.Delete method");
        }

        //insert
        public ISqlBuilder Insert(Expression expression, ISqlBuilder ISqlBuilder)
        {
            return Insert((T)expression, ISqlBuilder);
        }
        //update
        public ISqlBuilder Update(Expression expression, ISqlBuilder ISqlBuilder)
        {
            return Update((T)expression, ISqlBuilder);
        }
        //select
        public ISqlBuilder Select(Expression expression, ISqlBuilder ISqlBuilder)
        {
            return Select((T)expression, ISqlBuilder);
        }
        //join
        public ISqlBuilder Join(Expression expression, ISqlBuilder ISqlBuilder)
        {
            return Join((T)expression, ISqlBuilder);
        }
        //where
        public ISqlBuilder Where(Expression expression, ISqlBuilder ISqlBuilder)
        {
            return Where((T)expression, ISqlBuilder);
        }
        public ISqlBuilder In(Expression expression, ISqlBuilder ISqlBuilder)
        {
            return In((T)expression, ISqlBuilder);
        }
        //group
        public ISqlBuilder GroupBy(Expression expression, ISqlBuilder ISqlBuilder)
        {
            return GroupBy((T)expression, ISqlBuilder);
        }
        //oreder
        public ISqlBuilder OrderBy(Expression expression, ISqlBuilder ISqlBuilder)
        {
            return OrderBy((T)expression, ISqlBuilder);
        }
        public ISqlBuilder ThenBy(Expression expression, ISqlBuilder ISqlBuilder)
        {
            return ThenBy((T)expression, ISqlBuilder);
        }
        public ISqlBuilder OrderByDescending(Expression expression, ISqlBuilder ISqlBuilder)
        {
            return OrderByDescending((T)expression, ISqlBuilder);
        }
        public ISqlBuilder ThenByDescending(Expression expression, ISqlBuilder ISqlBuilder)
        {
            return ThenByDescending((T)expression, ISqlBuilder);
        }
        //calculate
        public ISqlBuilder Max(Expression expression, ISqlBuilder ISqlBuilder)
        {
            return Max((T)expression, ISqlBuilder);
        }
        public ISqlBuilder Min(Expression expression, ISqlBuilder ISqlBuilder)
        {
            return Min((T)expression, ISqlBuilder);
        }
        public ISqlBuilder Avg(Expression expression, ISqlBuilder ISqlBuilder)
        {
            return Avg((T)expression, ISqlBuilder);
        }
        public ISqlBuilder Count(Expression expression, ISqlBuilder ISqlBuilder)
        {
            return Count((T)expression, ISqlBuilder);
        }
        public ISqlBuilder Sum(Expression expression, ISqlBuilder ISqlBuilder)
        {
            return Sum((T)expression, ISqlBuilder);
        }

        public ISqlBuilder Delete(Expression expression, ISqlBuilder ISqlBuilder)
        {
            return Delete((T)expression, ISqlBuilder);
        }
    }
}
