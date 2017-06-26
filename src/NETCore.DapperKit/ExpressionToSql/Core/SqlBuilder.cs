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
        private List<string> _SelectPageAlaises;
        private List<string> _SelectAlaises;
        private List<string> _CalculateAlaises;
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
        private bool _IsSelectMultiTable = false;
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

        #region Sql

        /// <summary>
        /// set sql command type
        /// </summary>
        /// <param name="type"><see cref="SqlCommandType"/></param>
        public void SetSqlCommandType(SqlCommandType type)
        {
            _SqlCommandType = type;
        }

        /// <summary>
        /// get sql string
        /// </summary>
        /// <returns></returns>
        public string GetSql()
        {
            if (_SqlCommandType == SqlCommandType.Insert)
            {
                return GetInsertSql();
            }

            if (_SqlCommandType == SqlCommandType.Delete)
            {
                return GetDeleteSql();
            }

            if (_SqlCommandType == SqlCommandType.Update)
            {
                return GetUpdateSql();
            }

            if (_SqlCommandType == SqlCommandType.Select)
            {
                if (SkipNum <= 0)
                {
                    return GetSelectSql();
                }
                else
                {
                    return GetPageSelectSql();
                }
            }

            if (_SqlCommandType == SqlCommandType.Calculate)
            {
                return GetCalculateSql();
            }

            return string.Empty;
        }

        private string GetInsertSql()
        {
            if (_InsertSqlBuilder != null)
            {
                return $"{_InsertSqlBuilder.ToString().TrimEnd()};";
            }
            else
            {
                throw new Exception("Insert sql is empty");
            }
        }

        private string GetDeleteSql()
        {
            var deleteBuilder = new StringBuilder();

            deleteBuilder.Append($"{_DeleteSqlBuilder.ToString()}");

            if (_WhereSqlBuilder != null && _WhereSqlBuilder.Length > 0)
            {
                deleteBuilder.Append(_WhereSqlBuilder.ToString());
            }

            return $"{deleteBuilder.ToString().TrimEnd()};";
        }

        private string GetUpdateSql()
        {
            var updateBuilder = new StringBuilder();

            updateBuilder.Append($"{_UpdateSqlBuilder.ToString()}");

            if (_WhereSqlBuilder != null && _WhereSqlBuilder.Length > 0)
            {
                updateBuilder.Append(_WhereSqlBuilder.ToString());
            }
            return $"{updateBuilder.ToString().TrimEnd()};";
        }

        private string GetSelectSql()
        {
            var selectBuilder = new StringBuilder();

            selectBuilder.AppendFormat(_SelectSqlBuilder.ToString(), string.Join(",", _SelectAlaises).TrimEnd());

            if (_IsSelectMultiTable && (_JoinSqlBuilder == null || _JoinSqlBuilder.Length == 0))
            {
                throw new Exception("select multi data table must include join sql");
            }

            if (_JoinSqlBuilder != null && _JoinSqlBuilder.Length > 0)
            {
                selectBuilder.Append(_JoinSqlBuilder.ToString());
            }

            if (_WhereSqlBuilder != null && _WhereSqlBuilder.Length > 0)
            {
                selectBuilder.Append(_WhereSqlBuilder.ToString());
            }

            if (_OrderSqlBuilder != null && _OrderSqlBuilder.Length > 0)
            {
                selectBuilder.Append(_OrderSqlBuilder.ToString());
            }

            if (_GroupSqlBuilder != null && _GroupSqlBuilder.Length > 0)
            {
                selectBuilder.Append(_GroupSqlBuilder.ToString());
            }

            return $"{selectBuilder.ToString().TrimEnd()};";
        }

        private string GetPageSelectSql()
        {
            var selectBuilder = new StringBuilder();

            selectBuilder.Append($"SELECT {string.Join(",", _SelectPageAlaises).TrimEnd()} FROM ");
            selectBuilder.Append("( ");
            selectBuilder.AppendFormat(_SelectSqlBuilder.ToString(), $"{string.Join(",", _SelectAlaises).TrimEnd()},ROW_NUMBER() OVER ( { _OrderSqlBuilder.ToString()}) AS [RowNumber]");
            if (_IsSelectMultiTable && (_JoinSqlBuilder == null || _JoinSqlBuilder.Length == 0))
            {
                throw new Exception("select multi data table must include join sql");
            }

            if (_JoinSqlBuilder != null && _JoinSqlBuilder.Length > 0)
            {
                selectBuilder.Append(_JoinSqlBuilder.ToString());
            }

            if (_WhereSqlBuilder != null && _WhereSqlBuilder.Length > 0)
            {
                selectBuilder.Append(_WhereSqlBuilder.ToString());
            }

            selectBuilder.Append($") DapperKit_Temp_PageTable ");

            selectBuilder.Append($"WHERE DapperKit_Temp_PageTable.RowNumber>{SkipNum} AND DapperKit_Temp_PageTable.RowNumber<={SkipNum + TakeNum};");

            return selectBuilder.ToString();
        }

        private string GetCalculateSql()
        {
            var calculateBuilder = new StringBuilder();

            calculateBuilder.AppendFormat(string.Format(_CalculateSqlBuilder.ToString(), string.Join(",", _CalculateAlaises).TrimEnd()));

            if (_WhereSqlBuilder != null && _WhereSqlBuilder.Length > 0)
            {
                calculateBuilder.Append(_WhereSqlBuilder.ToString());
            }

            if (_GroupSqlBuilder != null && _GroupSqlBuilder.Length > 0)
            {
                calculateBuilder.Append(_GroupSqlBuilder.ToString());
            }

            return $"{calculateBuilder.ToString().TrimEnd()};";
        }

        #endregion

        #region Select multi table

        public void SetSelectMultiTable()
        {
            _IsSelectMultiTable = true;
        }

        #endregion

        #region SqlParameter

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
        public Dictionary<string, object> GetSqlParams()
        {
            return _SqlParameters;
        }
        #endregion

        #region TableAlias

        /// <summary>
        /// set table alias 
        /// </summary>
        /// <param name="tableName">table name</param>
        public string GetTableAlias(string tableName)
        {
            if (_TableNames == null)
            {
                return "";
            }

            if (_TableNames.Keys.Contains(tableName))
            {
                return _TableNames[tableName];
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

                if (_WhereSqlBuilder.Length == 0)
                {
                    _WhereSqlBuilder.Append("WHERE ");
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

                if (_OrderSqlBuilder.Length == 0)
                {
                    _OrderSqlBuilder.Append($"ORDER BY {sql}");
                }
                else
                {
                    _OrderSqlBuilder.Append($",{sql}");
                }
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
                if (_GroupSqlBuilder.Length == 0)
                {
                    _GroupSqlBuilder.Append($"GROUP BY {sql}");
                }
                else
                {
                    _GroupSqlBuilder.Append($",{sql}");
                }
            }
        }

        /// <summary>
        /// add select column
        /// </summary>
        /// <param name="columnAlias"></param>
        public void AddSelectColumn(string columnAlias)
        {
            if (_SelectAlaises == null)
            {
                _SelectAlaises = new List<string>();
            }
            _SelectAlaises.Add(columnAlias);
        }

        /// <summary>
        /// add page column
        /// </summary>
        /// <param name="pageColumnAlias"></param>
        public void AddSelectPageColumn(string pageColumnAlias)
        {
            if (_SelectPageAlaises == null)
            {
                _SelectPageAlaises = new List<string>();
            }
            _SelectPageAlaises.Add(pageColumnAlias);
        }

        /// <summary>
        /// add calculate column
        /// </summary>
        /// <param name="columnAlias"></param>
        public void AddCalculateColumn(string columnAlias)
        {
            if (_CalculateAlaises == null)
            {
                _CalculateAlaises = new List<string>();
            }
            _CalculateAlaises.Add(columnAlias);
        }

        /// <summary>
        /// set skip num
        /// </summary>
        /// <param name="skipNum"></param>
        public void Skip(int skipNum)
        {
            if (skipNum <= 0)
            {
                throw new Exception("The skip value is must greate than 0");
            }

            SkipNum = skipNum;
        }

        /// <summary>
        /// set take num
        /// </summary>
        /// <param name="takeNum"></param>
        public void Take(int takeNum)
        {
            if (SkipNum > 0)
            {
                TakeNum = takeNum <= 0 ? 20 : takeNum;
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
