using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NETCore.DapperKit.ExpressionToSql.Query.Interface
{
    public interface ICollectionQueryAble<T> where T : class
    {
        TResult FirstOrDefault<TResult>();

        Task<TResult> FirstOrDefaultAsync<TResult>();

        IEnumerable<TResult> ToList<TResult>();

        Task<IEnumerable<TResult>> ToListAsync<TResult>();
    }
}
