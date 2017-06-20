using NETCore.DapperKit.ExpressionToSql.Core;
using NETCore.DapperKit.ExpressionToSql.Query.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;

namespace NETCore.DapperKit.ExpressionToSql.Query
{
    public class SelectQueryAble<T> : BaseCollectionQueryAble<T>, ISelectQueryAble<T> where T : class
    {
        public SelectQueryAble(ISqlBuilder sqlBuilder, IDapperKitProvider provider) : base(sqlBuilder, provider)
        {

        }

        public ISelectQueryAble<T> Where(Expression<Func<T, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public ISelectQueryAble<T> Join<T2>(Expression<Func<T, T2, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public ISelectQueryAble<T> Join<T2, T3>(Expression<Func<T, T2, T3, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public ISelectQueryAble<T> Join<T2, T3, T4>(Expression<Func<T, T2, T3, T4, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public ISelectQueryAble<T> InnerJoin<T2>(Expression<Func<T, T2, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public ISelectQueryAble<T> InnerJoin<T2, T3>(Expression<Func<T, T2, T3, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public ISelectQueryAble<T> InnerJoin<T2, T3, T4>(Expression<Func<T, T2, T3, T4, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public ISelectQueryAble<T> LeftJoin<T2>(Expression<Func<T, T2, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public ISelectQueryAble<T> LeftJoin<T2, T3>(Expression<Func<T, T2, T3, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public ISelectQueryAble<T> LeftJoin<T2, T3, T4>(Expression<Func<T, T2, T3, T4, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public ISelectQueryAble<T> RightJoin<T2>(Expression<Func<T, T2, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public ISelectQueryAble<T> RightJoin<T2, T3>(Expression<Func<T, T2, T3, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public ISelectQueryAble<T> RightJoin<T2, T3, T4>(Expression<Func<T, T2, T3, T4, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public ISelectQueryAble<T> FullJoin<T2>(Expression<Func<T, T2, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public ISelectQueryAble<T> FullJoin<T2, T3>(Expression<Func<T, T2, T3, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public ISelectQueryAble<T> FullJoin<T2, T3, T4>(Expression<Func<T, T2, T3, T4, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public ISelectQueryAble<T> GroupBy(Expression<Func<T, object>> expression)
        {
            throw new NotImplementedException();
        }

        public ISelectQueryAble<T> OrderBy(Expression<Func<T, object>> expression)
        {
            throw new NotImplementedException();
        }

        public ISelectQueryAble<T> OrderByDescending(Expression<Func<T, object>> expression)
        {
            throw new NotImplementedException();
        }

        public ISelectQueryAble<T> ThenBy(Expression<Func<T, object>> expression)
        {
            throw new NotImplementedException();
        }

        public ISelectQueryAble<T> ThenByDescending(Expression<Func<T, object>> expression)
        {
            throw new NotImplementedException();
        }

        public ISelectQueryAble<T> Skip(int skipNum)
        {
            throw new NotImplementedException();
        }

        public ISelectQueryAble<T> Take(int takeNum)
        {
            throw new NotImplementedException();
        }
    }
}
