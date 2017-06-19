using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace NETCore.DapperKit.Expression.QueryAble
{
    public class SqlQueryProvier<T> where T : class
    {
        private readonly IDbConnection _DbConnection;

        public SqlQueryProvier(IDbConnection dbConnection)
        {
            _DbConnection = dbConnection;
        }


    }
}
