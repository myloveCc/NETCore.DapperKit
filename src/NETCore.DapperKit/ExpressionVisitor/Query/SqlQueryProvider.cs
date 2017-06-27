using NETCore.DapperKit.ExpressionVisitor.Core;
using NETCore.DapperKit.ExpressionVisitor.Internal;
using NETCore.DapperKit.Shared;
using System;
using System.Data;
using System.Linq.Expressions;
using NETCore.DapperKit.Extensions;
using NETCore.DapperKit.ExpressionVisitor.Query.Interface;
using NETCore.DapperKit.ExpressionVisitor.SqlVisitor;

namespace NETCore.DapperKit.ExpressionVisitor.Query
{
    public class SqlQueryProvider<T> : ISqlQueryProvider<T>, IDisposable where T : class
    {
        private readonly ISqlBuilder _SqlBuilder;
        private readonly IDapperContext _DapperKitProvider;
        private readonly string _MainTableName;

        public SqlQueryProvider(IDapperContext provider)
        {
            _DapperKitProvider = provider;
            _SqlBuilder = new SqlBuilder(_DapperKitProvider.Options.DatabaseType);
            _MainTableName = typeof(T).GetDapperTableName(_SqlBuilder._SqlFormater);
        }

        /// <summary>
        /// insert
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public IInsertQueryAble<T> Insert(Expression<Func<T>> expression)
        {
            Check.Argument.IsNotNull(expression, nameof(expression));

            _SqlBuilder.SetSqlCommandType(SqlCommandType.Insert);
            _SqlBuilder.AppendInsertSql($"INSERT INTO {_MainTableName} ");

            SqlVistorProvider.Insert(expression.Body, _SqlBuilder);

            return new InsertQueryAble<T>(_SqlBuilder, _DapperKitProvider);
        }

        /// <summary>
        /// delete
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public IDeleteQueryAble<T> Delete(Expression<Func<T, bool>> expression = null)
        {
            _SqlBuilder.AppendDeleteSql($"DELETE {_MainTableName} ");
            _SqlBuilder.SetSqlCommandType(SqlCommandType.Delete);

            if (expression != null)
            {
                SqlVistorProvider.Delete(expression.Body, _SqlBuilder);
            }
            return new DeleteQueryAble<T>(_SqlBuilder, _DapperKitProvider);
        }

        /// <summary>
        /// update
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public IUpdateQueryAble<T> Update(Expression<Func<T>> expression)
        {
            Check.Argument.IsNotNull(expression, nameof(expression));
            _SqlBuilder.SetSqlCommandType(SqlCommandType.Update);
            _SqlBuilder.AppendUpdateSql($"UPDATE {_MainTableName} SET ");

            SqlVistorProvider.Update(expression.Body, _SqlBuilder);

            return new UpdateQueryAble<T>(_SqlBuilder, _DapperKitProvider);
        }


        public ISelectQueryAble<T> Select(Expression<Func<T, object>> expression = null)
        {
            return SelectParser(expression, expression == null ? null : expression.Body);
        }

        public ISelectQueryAble<T> Select<T2>(Expression<Func<T, T2, object>> expression = null)
        {
            return SelectParser(expression, expression == null ? null : expression.Body, typeof(T2));
        }

        public ISelectQueryAble<T> Select<T2, T3>(Expression<Func<T, T2, T3, object>> expression = null)
        {
            return SelectParser(expression, expression == null ? null : expression.Body, typeof(T2), typeof(T3));
        }

        public ISelectQueryAble<T> Select<T2, T3, T4>(Expression<Func<T, T2, T3, T4, object>> expression = null)
        {
            return SelectParser(expression, expression == null ? null : expression.Body, typeof(T2), typeof(T3), typeof(T4));
        }

        public ISelectQueryAble<T> Select<T2, T3, T4, T5>(Expression<Func<T, T2, T3, T4, T5, object>> expression = null)
        {
            return SelectParser(expression, expression == null ? null : expression.Body, typeof(T2), typeof(T3), typeof(T4), typeof(T5));
        }

        public ISelectQueryAble<T> Select<T2, T3, T4, T5, T6>(Expression<Func<T, T2, T3, T4, T5, T6, object>> expression = null)
        {
            return SelectParser(expression, expression == null ? null : expression.Body, typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6));
        }

        public ISelectQueryAble<T> Select<T2, T3, T4, T5, T6, T7>(Expression<Func<T, T2, T3, T4, T5, T6, T7, object>> expression = null)
        {
            return SelectParser(expression, expression == null ? null : expression.Body, typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7));
        }

        public ISelectQueryAble<T> Select<T2, T3, T4, T5, T6, T7, T8>(Expression<Func<T, T2, T3, T4, T5, T6, T7, T8, object>> expression = null)
        {
            return SelectParser(expression, expression == null ? null : expression.Body, typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8));
        }

        public ISelectQueryAble<T> Select<T2, T3, T4, T5, T6, T7, T8, T9>(Expression<Func<T, T2, T3, T4, T5, T6, T7, T8, T9, object>> expression = null)
        {
            return SelectParser(expression, expression == null ? null : expression.Body, typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9));
        }

        public ISelectQueryAble<T> Select<T2, T3, T4, T5, T6, T7, T8, T9, T10>(Expression<Func<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, object>> expression = null)
        {
            return SelectParser(expression, expression == null ? null : expression.Body, typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9), typeof(T10));
        }

        private ISelectQueryAble<T> SelectParser(Expression expression, Expression expressionBody, params Type[] types)
        {
            _SqlBuilder.SetSqlCommandType(SqlCommandType.Select);
            _SqlBuilder.SetTableAlias(_MainTableName);

            _SqlBuilder.AppendSelectSql($"SELECT {{0}} FROM {_MainTableName} {_SqlBuilder.GetTableAlias(_MainTableName)} ");

            var selectQueryAble = new SelectQueryAble<T>(_SqlBuilder, _DapperKitProvider);

            if (expression != null && expressionBody != null)
            {
                if (types != null && types.Length > 0)
                {
                    _SqlBuilder.SetSelectMultiTable();

                    foreach (var type in types)
                    {
                        string tableName = type.GetDapperTableName(_SqlBuilder._SqlFormater);
                        _SqlBuilder.SetTableAlias(tableName);

                        //add data table type to collection
                        selectQueryAble.TableTypeCollections.Add(type);
                    }
                }

                SqlVistorProvider.Select(expressionBody, _SqlBuilder);
            }
            else
            {
                _SqlBuilder.AddSelectColumn("* ");
                _SqlBuilder.AddSelectPageColumn("* ");
            }

            return selectQueryAble;
        }

        public ICalculateQueryAble<T> Count(Expression<Func<T, object>> expression = null)
        {
            _SqlBuilder.SetSqlCommandType(SqlCommandType.Calculate);

            _SqlBuilder.AppendCalculateSql($"SELECT {{0}} FROM {_MainTableName} ");
            if (expression != null)
            {

                SqlVistorProvider.Count(expression.Body, _SqlBuilder);
            }
            else
            {
                _SqlBuilder.AddCalculateColumn("COUNT(*) ");
            }

            return new CalculateQueryAble<T>(_SqlBuilder, _DapperKitProvider);
        }

        public ICalculateQueryAble<T> Avg(Expression<Func<T, object>> expression)
        {
            Check.Argument.IsNotNull(expression, nameof(expression));
            _SqlBuilder.SetSqlCommandType(SqlCommandType.Calculate);

            _SqlBuilder.AppendCalculateSql($"SELECT {{0}} FROM {_MainTableName} ");
            SqlVistorProvider.Avg(expression.Body, _SqlBuilder);

            return new CalculateQueryAble<T>(_SqlBuilder, _DapperKitProvider);
        }

        public ICalculateQueryAble<T> Max(Expression<Func<T, object>> expression)
        {
            Check.Argument.IsNotNull(expression, nameof(expression));
            _SqlBuilder.SetSqlCommandType(SqlCommandType.Calculate);

            _SqlBuilder.AppendCalculateSql($"SELECT {{0}} FROM {_MainTableName} ");
            SqlVistorProvider.Max(expression.Body, _SqlBuilder);

            return new CalculateQueryAble<T>(_SqlBuilder, _DapperKitProvider);
        }

        public ICalculateQueryAble<T> Min(Expression<Func<T, object>> expression)
        {
            Check.Argument.IsNotNull(expression, nameof(expression));
            _SqlBuilder.SetSqlCommandType(SqlCommandType.Calculate);

            _SqlBuilder.AppendCalculateSql($"SELECT {{0}} FROM {_MainTableName} ");
            SqlVistorProvider.Min(expression.Body, _SqlBuilder);
            return new CalculateQueryAble<T>(_SqlBuilder, _DapperKitProvider);
        }

        public ICalculateQueryAble<T> Sum(Expression<Func<T, object>> expression)
        {
            Check.Argument.IsNotNull(expression, nameof(expression));
            _SqlBuilder.SetSqlCommandType(SqlCommandType.Calculate);

            _SqlBuilder.AppendCalculateSql($"SELECT {{0}} FROM {_MainTableName} ");
            SqlVistorProvider.Sum(expression.Body, _SqlBuilder);

            return new CalculateQueryAble<T>(_SqlBuilder, _DapperKitProvider);
        }

        /// <summary>
        /// dispose
        /// </summary>
        public void Dispose()
        {

        }
    }
}
