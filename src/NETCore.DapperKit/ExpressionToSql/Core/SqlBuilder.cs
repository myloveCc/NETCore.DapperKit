using NETCore.DapperKit.ExpressionToSql.Internal;
using NETCore.DapperKit.Infrastructure.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NETCore.DapperKit.ExpressionToSql.Core
{
    public class SqlBuilder : ISqlBuilder, IDisposable
    {
        private readonly List<string> _TableAlias;
        private Queue<string> _QueueTableAlias;
        private Dictionary<string, string> _TableNames;
        private SqlCommandType _SqlCommandType;
        private readonly DatabaseType _DatabaseType;
        private Dictionary<string, object> _SqlParameters;
        private StringBuilder _InsertSqlBuilder;
        private StringBuilder _DeleteSqlBuilder;
        private StringBuilder _UpdateSqlBuilder;
        private StringBuilder _SelectSqlBuilder;
        private StringBuilder _CalculateSqlBuilder;
        private StringBuilder _WhereSqlBuilder;
        private StringBuilder _JoinSqlBuilder;
        private StringBuilder _OrderSqlBuilder;
        private StringBuilder _GroupSqlBuilder;
        private int SkipNum = 0;
        private int TakeNum = 0;
        public ISqlFormater _SqlFormater { get; private set; }

        /// <summary>
        /// ctor
        /// </summary>
        public SqlBuilder(DatabaseType databaseType)
        {
            _TableAlias = new List<string> { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
            _QueueTableAlias = new Queue<string>(_TableAlias);
            _DatabaseType = databaseType;
            _SqlFormater = FormaterFactory.SqlFormater(_DatabaseType);
        }

        #region SqlCommand Type

        /// <summary>
        /// set sql command type
        /// </summary>
        /// <param name="type"><see cref="SqlCommandType"/></param>
        public void SetSqlCommandType(SqlCommandType type)
        {
            _SqlCommandType = type;
        }

        #endregion

        #region DbParameter

        /// <summary>
        /// set sql param
        /// </summary>
        /// <param name="paramValue">param value</param>
        /// <returns></returns>

        public string SetSqlParameter(object paramValue)
        {
            if (_SqlParameters == null)
            {
                _SqlParameters = new Dictionary<string, object>();
            }
            string paramName = "";
            paramName = _SqlFormater.Prefix + "param" + this._SqlParameters.Count;

            _SqlParameters.Add(paramName, paramValue);
            return paramName;
        }

        /// <summary>
        /// get sql param dictionary
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, object> GetSetSqlParameters()
        {
            return _SqlParameters;
        }

        /// <summary>
        /// get sql string
        /// </summary>
        /// <returns></returns>
        public string GetSqlString()
        {
            if (_SqlCommandType == SqlCommandType.Insert)
            {
                return _InsertSqlBuilder.ToString();
            }

            //TODO
            return string.Empty;
        }

        #endregion

        #region TableAlias

        /// <summary>
        /// set table alias 
        /// </summary>
        /// <param name="tableName">table name</param>
        public string GetTableAlias(string tableName)
        {
            if (this._TableNames.Keys.Contains(tableName))
            {
                return this._TableNames[tableName];
            }
            return "";
        }

        /// <summary>
        /// get table alias
        /// </summary>
        /// <param name="tableName">table name</param>
        /// <returns></returns>
        public void SetTableAlias(string tableName)
        {
            if (_TableNames == null)
            {
                _TableNames = new Dictionary<string, string>();
            }
            if (!_TableNames.Keys.Contains(tableName))
            {
                _TableNames.Add(tableName, _QueueTableAlias.Dequeue());
            }
            throw new NotImplementedException();
        }
        #endregion

        #region Sql Append
        /// <summary>
        /// append insert sql
        /// </summary>
        /// <param name="sql">sql string</param>
        public void AppendInsertSql(string sql)
        {
            if (!string.IsNullOrEmpty(sql))
            {
                if (_InsertSqlBuilder == null)
                {
                    _InsertSqlBuilder = new StringBuilder();
                }
                _InsertSqlBuilder.Append(sql);
            }
        }

        /// <summary>
        /// append delete sql
        /// </summary>
        /// <param name="sql">sql string</param>
        public void AppendDeleteSql(string sql)
        {
            if (!string.IsNullOrEmpty(sql))
            {
                if (_DeleteSqlBuilder == null)
                {
                    _DeleteSqlBuilder = new StringBuilder();
                }
                _DeleteSqlBuilder.Append(sql);
            }
        }


        /// <summary>
        /// append update sql
        /// </summary>
        /// <param name="sql">sql string</param>
        public void AppendUpdateSql(string sql)
        {
            if (!string.IsNullOrEmpty(sql))
            {
                if (_UpdateSqlBuilder == null)
                {
                    _UpdateSqlBuilder = new StringBuilder();
                }
                _UpdateSqlBuilder.Append(sql);
            }
        }
       
        /// <summary>
        /// append select sql
        /// </summary>
        /// <param name="sql">sql string</param>
        public void AppendSelectSql(string sql)
        {
            if (!string.IsNullOrEmpty(sql))
            {
                if (_SelectSqlBuilder == null)
                {
                    _SelectSqlBuilder = new StringBuilder();
                }
                _SelectSqlBuilder.Append(sql);
            }
        }

        /// <summary>
        /// append select sql
        /// </summary>
        /// <param name="sql">sql string</param>
        public void AppendCalculateSql(string sql)
        {
            if (!string.IsNullOrEmpty(sql))
            {
                if (_CalculateSqlBuilder == null)
                {
                    _CalculateSqlBuilder = new StringBuilder();
                }
                _CalculateSqlBuilder.Append(sql);
            }
        }


        /// <summary>
        /// append where sql
        /// </summary>
        /// <param name="sql"></param>
        public void AppendWhereSql(string sql)
        {
            if (!string.IsNullOrEmpty(sql))
            {
                if (_WhereSqlBuilder == null)
                {
                    _WhereSqlBuilder = new StringBuilder();
                }

                _WhereSqlBuilder.Append(sql);
            }
        }

        /// <summary>
        /// append join sql
        /// </summary>
        /// <param name="sql"></param>
        public void AppendJoinSql(string sql)
        {
            if (!string.IsNullOrEmpty(sql))
            {
                if (_JoinSqlBuilder == null)
                {
                    _JoinSqlBuilder = new StringBuilder();
                }
                _JoinSqlBuilder.Append(sql);
            }
        }

        /// <summary>
        /// append order sql
        /// </summary>
        /// <param name="sql"></param>
        public void AppendOrderSql(string sql)
        {
            if (!string.IsNullOrEmpty(sql))
            {
                if (_OrderSqlBuilder == null)
                {
                    _OrderSqlBuilder = new StringBuilder();
                }
                _OrderSqlBuilder.Append(sql);
            }
        }

        /// <summary>
        /// append group sql
        /// </summary>
        /// <param name="sql"></param>
        public void AppendGroupSql(string sql)
        {
            if (!string.IsNullOrEmpty(sql))
            {
                if (_GroupSqlBuilder == null)
                {
                    _GroupSqlBuilder = new StringBuilder();
                }
                _GroupSqlBuilder.Append(sql);
            }
        }

        #endregion

        #region Dispose
        public void Dispose()
        {

        }
        #endregion

    }
}
