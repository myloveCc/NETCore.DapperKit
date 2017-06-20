using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace NETCore.DapperKit.ExpressionToSql.Query.Interface
{
    public interface ISelectQueryAble<T> : ICollectionQueryAble<T> where T : class
    {
        ISelectQueryAble<T> Join<T2>(Expression<Func<T, T2, bool>> expression);

        ISelectQueryAble<T> Join<T2, T3>(Expression<Func<T, T2, T3, bool>> expression);

        ISelectQueryAble<T> Join<T2, T3, T4>(Expression<Func<T, T2, T3, T4, bool>> expression);

        ISelectQueryAble<T> InnerJoin<T2>(Expression<Func<T, T2, bool>> expression);

        ISelectQueryAble<T> InnerJoin<T2, T3>(Expression<Func<T, T2, T3, bool>> expression);

        ISelectQueryAble<T> InnerJoin<T2, T3, T4>(Expression<Func<T, T2, T3, T4, bool>> expression);

        ISelectQueryAble<T> LeftJoin<T2>(Expression<Func<T, T2, bool>> expression);

        ISelectQueryAble<T> LeftJoin<T2, T3>(Expression<Func<T, T2, T3, bool>> expression);

        ISelectQueryAble<T> LeftJoin<T2, T3, T4>(Expression<Func<T, T2, T3, T4, bool>> expression);

        ISelectQueryAble<T> RightJoin<T2>(Expression<Func<T, T2, bool>> expression);

        ISelectQueryAble<T> RightJoin<T2, T3>(Expression<Func<T, T2, T3, bool>> expression);

        ISelectQueryAble<T> RightJoin<T2, T3, T4>(Expression<Func<T, T2, T3, T4, bool>> expression);

        ISelectQueryAble<T> FullJoin<T2>(Expression<Func<T, T2, bool>> expression);

        ISelectQueryAble<T> FullJoin<T2, T3>(Expression<Func<T, T2, T3, bool>> expression);

        ISelectQueryAble<T> FullJoin<T2, T3, T4>(Expression<Func<T, T2, T3, T4, bool>> expression);

        ISelectQueryAble<T> Where(Expression<Func<T, bool>> expression);

        ISelectQueryAble<T> OrderBy(Expression<Func<T, object>> expression);

        ISelectQueryAble<T> OrderByDescending(Expression<Func<T, object>> expression);

        ISelectQueryAble<T> ThenBy(Expression<Func<T, object>> expression);

        ISelectQueryAble<T> ThenByDescending(Expression<Func<T, object>> expression);

        ISelectQueryAble<T> GroupBy(Expression<Func<T, object>> expression);

        ISelectQueryAble<T> Skip(int skipNum);

        ISelectQueryAble<T> Take(int takeNum);

    }
}
