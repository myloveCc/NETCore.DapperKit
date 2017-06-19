using NETCore.DapperKit.Expression.Internal;
using System;
using System.Collections.Generic;
using System.Text;

namespace NETCore.DapperKit.Expression.Core
{
    public interface ISqlBuilder
    {
        /// <summary>
        /// set sql command type
        /// </summary>
        /// <param name="type"><see cref="SqlCommandType"/></param>
        void SetSqlCommandType(SqlCommandType type);

        /// <summary>
        /// set db param
        /// </summary>
        /// <param name="paramValue">param value</param>
        /// <returns></returns>
        string SetDbParameter(object paramValue);

        /// <summary>
        /// set table alias 
        /// </summary>
        /// <param name="tableName">table name</param>
        void SetTableAlias(string tableName);

        /// <summary>
        /// get table alias
        /// </summary>
        /// <param name="tableName">table name</param>
        /// <returns></returns>
        string GetTableAlias(string tableName);



    }
}
