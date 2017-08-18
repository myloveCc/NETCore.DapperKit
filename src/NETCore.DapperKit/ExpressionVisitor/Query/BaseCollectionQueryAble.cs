using NETCore.DapperKit.ExpressionVisitor.Core;
using NETCore.DapperKit.ExpressionVisitor.Query.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace NETCore.DapperKit.ExpressionVisitor.Query
{
    public class BaseCollectionQueryAble<T> : ICollectionQueryAble<T> where T : class
    {
        public ISqlBuilder SqlBuilder { get; private set; }
        public IDapperContext DapperKitProvider { get; private set; }

        public BaseCollectionQueryAble(ISqlBuilder sqlBuilder, IDapperContext provider)
        {
            SqlBuilder = sqlBuilder;
            DapperKitProvider = provider;
        }

        public TResult Single<TResult>() where TResult : class
        {
            using (SqlBuilder)
            {
                using (var conn = DapperKitProvider.DbConnection)
                {
                    var sql = SqlBuilder.GetSql();
                    var paras = SqlBuilder.GetSqlParams();

                    return conn.QuerySingle<TResult>(sql, paras);
                }
            }
        }

        public Task<TResult> SingleAsync<TResult>() where TResult : class
        {
            using (SqlBuilder)
            {
                var conn = DapperKitProvider.DbConnection;
                var sql = SqlBuilder.GetSql();
                var paras = SqlBuilder.GetSqlParams();

                return conn.QuerySingleAsync<TResult>(sql, paras);
            }
        }

        public TResult FirstOrDefault<TResult>() where TResult : class
        {
            using (SqlBuilder)
            {
                using (var conn = DapperKitProvider.DbConnection)
                {
                    var sql = SqlBuilder.GetSql();
                    var paras = SqlBuilder.GetSqlParams();

                    return conn.QueryFirstOrDefault<TResult>(sql, paras);
                }
            }
        }

        public Task<TResult> FirstOrDefaultAsync<TResult>() where TResult : class
        {
            using (SqlBuilder)
            {
                var conn = DapperKitProvider.DbConnection;
                var sql = SqlBuilder.GetSql();
                var paras = SqlBuilder.GetSqlParams();
                return conn.QueryFirstOrDefaultAsync<TResult>(sql, paras);
            }
        }

        public IEnumerable<TResult> ToList<TResult>() where TResult : class
        {
            using (SqlBuilder)
            {
                using (var conn = DapperKitProvider.DbConnection)
                {
                    var sql = SqlBuilder.GetSql();
                    var paras = SqlBuilder.GetSqlParams();

                    return conn.Query<TResult>(sql, paras);
                }
            }
        }

        public Task<IEnumerable<TResult>> ToListAsync<TResult>() where TResult : class
        {
            using (SqlBuilder)
            {
                var conn = DapperKitProvider.DbConnection;

                var sql = SqlBuilder.GetSql();
                var paras = SqlBuilder.GetSqlParams();
                return conn.QueryAsync<TResult>(sql, paras);
            }
        }
    }
}
