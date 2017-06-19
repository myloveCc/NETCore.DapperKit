using System;
using System.Data;

namespace NETCore.DapperKit.Expression.Query
{
    public class SqlQueryAble<T> where T:class
    {
		private readonly IDbConnection _DbConnection;

		public SqlQueryAble(IDbConnection dbConnection)
		{
			_DbConnection = dbConnection;
		}


    }
}
