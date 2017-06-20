using NETCore.DapperKit.ExpressionToSql.Core;
using NETCore.DapperKit.ExpressionToSql.Internal;
using NETCore.DapperKit.Shared;
using System;
using System.Data;
using System.Linq.Expressions;
using NETCore.DapperKit.Extensions;
using NETCore.DapperKit.ExpressionToSql.Query.Interface;
using NETCore.DapperKit.ExpressionToSql.SqlVisitor;

namespace NETCore.DapperKit.ExpressionToSql.Query
{
    public class SqlQueryProvider<T> : ISqlQueryProvider<T>, IDisposable where T : class
    {
        private readonly ISqlBuilder _SqlBuilder;
        private readonly IDapperKitProvider _DapperKitProvider;
        private readonly string _MainTableName;

        public SqlQueryProvider(IDapperKitProvider provider)
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

            return new IsertQueryAble<T>(_SqlBuilder, _DapperKitProvider);
        }

        /// <summary>
        /// delete
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public IDeleteQueryAble<T> Delete(Expression<Func<T, bool>> expression)
        {
            _SqlBuilder.AppendDeleteSql($"DELETE {_MainTableName} ");
            _SqlBuilder.SetSqlCommandType(SqlCommandType.Delete);
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
            _SqlBuilder.AppendUpdateSql($"UPDATE {_MainTableName} SET");

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
            _SqlBuilder.AppendSelectSql("SELECT {0} FROM " + _MainTableName + " " + _SqlBuilder.GetTableAlias(_MainTableName));

            if (expression != null)
            {
                foreach (var type in types)
                {
                    string tableName = type.GetDapperTableName(_SqlBuilder._SqlFormater);
                    _SqlBuilder.SetTableAlias(tableName);
                }

                //TODO
            }
            return new SelectQueryAble<T>(_SqlBuilder, _DapperKitProvider);
        }

        public ICalculateQueryAble<T> Count(Expression<Func<T, object>> expression)
        {
            _SqlBuilder.SetSqlCommandType(SqlCommandType.Calculate);
            Check.Argument.IsNotNull(expression, nameof(expression));

            throw new NotImplementedException();
        }

        public ICalculateQueryAble<T> Avg(Expression<Func<T, object>> expression)
        {
            _SqlBuilder.SetSqlCommandType(SqlCommandType.Calculate);
            Check.Argument.IsNotNull(expression, nameof(expression));

            throw new NotImplementedException();
        }

        public ICalculateQueryAble<T> Max(Expression<Func<T, object>> expression)
        {
            _SqlBuilder.SetSqlCommandType(SqlCommandType.Calculate);
            Check.Argument.IsNotNull(expression, nameof(expression));
            throw new NotImplementedException();
        }

        public ICalculateQueryAble<T> Min(Expression<Func<T, object>> expression)
        {
            _SqlBuilder.SetSqlCommandType(SqlCommandType.Calculate);
            Check.Argument.IsNotNull(expression, nameof(expression));
            throw new NotImplementedException();
        }

        public ICalculateQueryAble<T> Sum(Expression<Func<T, object>> expression)
        {
            _SqlBuilder.SetSqlCommandType(SqlCommandType.Calculate);
            Check.Argument.IsNotNull(expression, nameof(expression));
            throw new NotImplementedException();
        }

        /// <summary>
        /// dispose
        /// </summary>
        public void Dispose()
        {
            throw new NotImplementedException();
        }


    }
}
