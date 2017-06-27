using NETCore.DapperKit.ExpressionVisitor.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NETCore.DapperKit.ExpressionVisitor.Query.Interface
{
    public interface ICollectionQueryAble<T> where T : class
    {
        ISqlBuilder SqlBuilder { get; }

        IDapperContext DapperKitProvider { get; }


        TResult Single<TResult>() where TResult : class;

        Task<TResult> SingleAsync<TResult>() where TResult : class;

        TResult FirstOrDefault<TResult>() where TResult : class;

        Task<TResult> FirstOrDefaultAsync<TResult>() where TResult : class;

        IEnumerable<TResult> ToList<TResult>() where TResult : class;

        Task<IEnumerable<TResult>> ToListAsync<TResult>() where TResult : class;
    }
}
