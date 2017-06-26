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

        public virtual int Exect()
        {
            using (SqlBuilder)
            {
                using (var conn = _DapperKitProvider.DbConnection)
                {
                    var sql = SqlBuilder.GetSql();
                    var paras = SqlBuilder.GetSqlParams();

                    return conn.Execute(sql, paras);
                }
            }
        }

        public virtual Task<int> ExectAsync()
        {
            using (SqlBuilder)
            {
                using (var conn = _DapperKitProvider.DbConnection)
                {
                    var sql = SqlBuilder.GetSql();
                    var paras = SqlBuilder.GetSqlParams();

                    return conn.ExecuteAsync(sql, paras);
                }
            }
        }
    }
}
