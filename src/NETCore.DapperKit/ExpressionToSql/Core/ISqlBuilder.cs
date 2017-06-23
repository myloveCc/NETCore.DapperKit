using NETCore.DapperKit.ExpressionToSql.Internal;
using System;
using System.Collections.Generic;
using System.Text;

namespace NETCore.DapperKit.ExpressionToSql.Core
{
    public interface ISqlBuilder : IDisposable
    {
        /// <summary>
        /// sql formater
        /// </summary>
        ISqlFormater _SqlFormater { get; }

        /// <summary>
        /// set sql command type
        /// </summary>
        /// <param name="type"><see cref="SqlCommandType"/></param>
        void SetSqlCommandType(SqlCommandType type);

        /// <summary>
        /// set select multi table
        /// </summary>
        void SetSelectMultiTable();

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

        /// <summary>
        /// set sql param
        /// </summary>
        /// <param name="paramValue">param value</param>
        /// <returns></returns>
        string SetSqlParameter(object paramValue);

        /// <summary>
        /// get sql param dic
        /// </summary>
        /// <returns></returns>
        Dictionary<string, object> GetSqlParameters();

        /// <summary>
        /// get sql string
        /// </summary>
        /// <returns></returns>
        string GetSqlString();

        /// <summary>
        /// append insert sql string
        /// </summary>
        /// <param name="sql"></param>
        void AppendInsertSql(string sql);

        /// <summary>
        /// append delete sql string
        /// </summary>
        /// <param name="sql"></param>
        void AppendDeleteSql(string sql);

        /// <summary>
        /// append update sql string
        /// </summary>
        /// <param name="sql"></param>
        void AppendUpdateSql(string sql);

        /// <summary>
        /// append select sql string
        /// </summary>
        /// <param name="sql"></param>
        void AppendSelectSql(string sql);

        /// <summary>
        /// append insert sql string
        /// </summary>
        /// <param name="sql"></param>
        void AppendCalculateSql(string sql);

        /// <summary>
        /// append where sql string
        /// </summary>
        /// <param name="sql"></param>
        void AppendWhereSql(string sql);

        /// <summary>
        /// append join sql string
        /// </summary>
        /// <param name="sql"></param>
        void AppendJoinSql(string sql);

        /// <summary>
        /// append order sql
        /// </summary>
        /// <param name="sql"></param>

        void AppendOrderSql(string sql);

        /// <summary>
        /// append group sql
        /// </summary>
        /// <param name="sql"></param>
        void AppendGroupSql(string sql);

        /// <summary>
        /// add select column
        /// </summary>
        /// <param name="columnName"></param>
        /// <param name="columnAlaises"></param>

        void AddSelectColumn(string columnAlaises);

        /// <summary>
        /// add calculate column
        /// </summary>
        /// <param name="columnAlaises"></param>
        void AddCalculateColumn(string columnAlaises);


        /// <summary>
        /// set skip num
        /// </summary>
        /// <param name="skipNum"></param>
        void Skip(int skipNum);

        /// <summary>
        /// set take num
        /// </summary>
        /// <param name="takeNum"></param>
        void Take(int takeNum);

    }
}
