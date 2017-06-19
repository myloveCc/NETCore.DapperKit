using NETCore.DapperKit.Expression.Internal;
using NETCore.DapperKit.Infrastructure.Internal;
using System;
using System.Collections.Generic;
using System.Text;

namespace NETCore.DapperKit.Expression.Core
{
    public class SqlBuilder : ISqlBuilder, IDisposable
    {
        private readonly List<string> _TableAliaCharts;
        private readonly Dictionary<string, string> _TableNames;
        private readonly SqlCommandType _SqlCommandType;
        private readonly DatabaseType _DatabaseType;

        /// <summary>
        /// ctor
        /// </summary>
        public SqlBuilder()
        {
            _TableAliaCharts = new List<string> { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
            _TableNames = new Dictionary<string, string>();
        }

        #region SqlCommand Type

        /// <summary>
        /// set sql command type
        /// </summary>
        /// <param name="type"><see cref="SqlCommandType"/></param>
        public void SetSqlCommandType(SqlCommandType type)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region DbParameter

        /// <summary>
        /// set db param
        /// </summary>
        /// <param name="paramValue">param value</param>
        /// <returns></returns>

        public string SetDbParameter(object paramValue)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region TableAlias

        /// <summary>
        /// set table alias 
        /// </summary>
        /// <param name="tableName">table name</param>
        public string GetTableAlias(string tableName)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// get table alias
        /// </summary>
        /// <param name="tableName">table name</param>
        /// <returns></returns>
        public void SetTableAlias(string tableName)
        {
            throw new NotImplementedException();
        }

        #endregion


        #region Dispose
        public void Dispose()
        {

        }

        #endregion



    }
}
