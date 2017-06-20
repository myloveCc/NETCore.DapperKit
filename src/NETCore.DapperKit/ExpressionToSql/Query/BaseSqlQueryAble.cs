using NETCore.DapperKit.ExpressionToSql.Core;
using NETCore.DapperKit.ExpressionToSql.Query.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace NETCore.DapperKit.ExpressionToSql.Query
{
    public class BaseSqlQueryAble<T> : ISqlQueryAble<T> where T : class
    {
        public ISqlBuilder SqlBuilder { get; private set; }
        public readonly IDapperKitProvider _DapperKitProvider;

        public BaseSqlQueryAble(ISqlBuilder sqlBuilder, IDapperKitProvider provider)
        {
            SqlBuilder = sqlBuilder;
            _DapperKitProvider = provider;
        }

        public virtual TResult Exect<TResult>()
        {
            using (SqlBuilder)
            {
                using (var conn = _DapperKitProvider.DbConnection)
                {
                    //TODO
                }
            }
            return default(TResult);
        }

        public virtual Task<TResult> ExectAsync<TResult>()
        {
            return Task<TResult>.Factory.StartNew(() =>
            {
                using (SqlBuilder)
                {
                    //TODO
                }

                return default(TResult);
            });
        }
    }
}
