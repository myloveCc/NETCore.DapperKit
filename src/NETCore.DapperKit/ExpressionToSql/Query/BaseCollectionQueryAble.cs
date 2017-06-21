using NETCore.DapperKit.ExpressionToSql.Core;
using NETCore.DapperKit.ExpressionToSql.Query.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace NETCore.DapperKit.ExpressionToSql.Query
{
    public class BaseCollectionQueryAble<T> : ICollectionQueryAble<T> where T : class
    {
        public ISqlBuilder SqlBuilder { get; private set; }
        public readonly IDapperKitProvider _DapperKitProvider;

        public BaseCollectionQueryAble(ISqlBuilder sqlBuilder, IDapperKitProvider provider)
        {
            SqlBuilder = sqlBuilder;
            _DapperKitProvider = provider;
        }


        public TResult FirstOrDefault<TResult>()
        {
            throw new NotImplementedException();
        }

        public Task<TResult> FirstOrDefaultAsync<TResult>()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TResult> ToList<TResult>()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TResult>> ToListAsync<TResult>()
        {
            throw new NotImplementedException();
        }
    }
}
