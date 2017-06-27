using NETCore.DapperKit.ExpressionVisitor.Core;
using NETCore.DapperKit.ExpressionVisitor.Query.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace NETCore.DapperKit.ExpressionVisitor.Query
{
    public class BaseSqlQueryAble<T> : BaseCollectionQueryAble<T>, ICollectionQueryAble<T>, ISqlQueryAble<T> where T : class
    {
        public BaseSqlQueryAble(ISqlBuilder sqlBuilder, IDapperContext provider) : base(sqlBuilder, provider)
        {

        }

        public virtual int Exect()
        {
            using (SqlBuilder)
            {
                using (var conn = DapperKitProvider.DbConnection)
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
                using (var conn = DapperKitProvider.DbConnection)
                {
                    var sql = SqlBuilder.GetSql();
                    var paras = SqlBuilder.GetSqlParams();

                    return conn.ExecuteAsync(sql, paras);
                }
            }
        }
    }
}
