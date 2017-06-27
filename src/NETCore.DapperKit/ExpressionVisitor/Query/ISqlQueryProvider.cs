using NETCore.DapperKit.ExpressionVisitor.Query.Interface;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace NETCore.DapperKit.ExpressionVisitor.Query
{
    public interface ISqlQueryProvider<T> where T : class
    {
        IInsertQueryAble<T> Insert(Expression<Func<T>> expression);

        IDeleteQueryAble<T> Delete(Expression<Func<T, bool>> expression = null);

        IUpdateQueryAble<T> Update(Expression<Func<T>> expression);

        ISelectQueryAble<T> Select(Expression<Func<T, object>> expression = null);

        ISelectQueryAble<T> Select<T2>(Expression<Func<T, T2, object>> expression = null);

        ISelectQueryAble<T> Select<T2, T3>(Expression<Func<T, T2, T3, object>> expression = null);

        ISelectQueryAble<T> Select<T2, T3, T4>(Expression<Func<T, T2, T3, T4, object>> expression = null);

        ISelectQueryAble<T> Select<T2, T3, T4, T5>(Expression<Func<T, T2, T3, T4, T5, object>> expression = null);

        ISelectQueryAble<T> Select<T2, T3, T4, T5, T6>(Expression<Func<T, T2, T3, T4, T5, T6, object>> expression = null);

        ISelectQueryAble<T> Select<T2, T3, T4, T5, T6, T7>(Expression<Func<T, T2, T3, T4, T5, T6, T7, object>> expression = null);

        ISelectQueryAble<T> Select<T2, T3, T4, T5, T6, T7, T8>(Expression<Func<T, T2, T3, T4, T5, T6, T7, T8, object>> expression = null);

        ISelectQueryAble<T> Select<T2, T3, T4, T5, T6, T7, T8, T9>(Expression<Func<T, T2, T3, T4, T5, T6, T7, T8, T9, object>> expression = null);

        ISelectQueryAble<T> Select<T2, T3, T4, T5, T6, T7, T8, T9, T10>(Expression<Func<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, object>> expression = null);

        ICalculateQueryAble<T> Count(Expression<Func<T, object>> expression = null);

        ICalculateQueryAble<T> Avg(Expression<Func<T, object>> expression);

        ICalculateQueryAble<T> Max(Expression<Func<T, object>> expression);

        ICalculateQueryAble<T> Min(Expression<Func<T, object>> expression);

        ICalculateQueryAble<T> Sum(Expression<Func<T, object>> expression);
    }
}
