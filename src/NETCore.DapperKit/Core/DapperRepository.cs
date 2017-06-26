using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using NETCore.DapperKit.Shared;
using NETCore.DapperKit.Extensions;
using System.Linq.Expressions;

namespace NETCore.DapperKit.Core
{
    public class DapperRepository : IDapperRepository
    {
        private IDapperKitProvider _DapperKitProvider;
        public DapperRepository(IDapperKitProvider provider)
        {
            _DapperKitProvider = provider;
        }

        #region Sync

        /// <summary>
        /// Insert entity
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns> success return row numbers ,else return 0</returns>
        public long Insert<T>(T entity) where T : class
        {
            Check.Argument.IsNotNull(entity, nameof(entity));
            using (var conn = _DapperKitProvider.DbConnection)
            {
                return conn.Insert(entityToInsert: entity, commandTimeout: _DapperKitProvider.Options.CommandTimeout);
            }
        }

        /// <summary>
        /// Insert entity collection
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entities">entity collection</param>
        /// <returns> success return row numbers ,else return 0</returns>
        public long Insert<T>(IEnumerable<T> entities) where T : class
        {
            Check.Argument.IsNotNull(entities, nameof(entities));
            using (var conn = _DapperKitProvider.DbConnection)
            {
                //add transaction
                using (var tran = conn.BeginTransaction())
                {
                    return conn.Insert(entityToInsert: entities, transaction: tran, commandTimeout: _DapperKitProvider.Options.CommandTimeout);
                }
            }
        }

        /// <summary>
        /// Update entity
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity">entity obj</param>
        /// <returns>success return true else return false</returns>
        public bool Update<T>(T entity) where T : class
        {
            Check.Argument.IsNotNull(entity, nameof(entity));
            using (var conn = _DapperKitProvider.DbConnection)
            {
                return conn.Update(entityToUpdate: entity, commandTimeout: _DapperKitProvider.Options.CommandTimeout);
            }
        }

        /// <summary>
        /// Update entity collection
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entities">entity obj collection</param>
        /// <returns>success return true else return false</returns>
        public bool Update<T>(IEnumerable<T> entities) where T : class
        {
            Check.Argument.IsNotNull(entities, nameof(entities));
            using (var conn = _DapperKitProvider.DbConnection)
            {
                //add transaction
                using (var tran = conn.BeginTransaction())
                {
                    return conn.Update(entityToUpdate: entities, transaction: tran, commandTimeout: _DapperKitProvider.Options.CommandTimeout);
                }
            }
        }

        /// <summary>
        /// update with sql and param
        /// </summary>
        /// <param name="sql">update sql string</param>
        /// <param name="param">update params</param>
        /// <param name="type"><see cref="CommandType"/></param>
        /// <returns>success return true else return false</returns>
        public bool Update(string sql, DynamicParameters param = null, CommandType? type = null)
        {
            Check.Argument.IsNotEmpty(sql, nameof(sql));
            using (var conn = _DapperKitProvider.DbConnection)
            {
                return conn.Execute(sql: sql, param: param, commandTimeout: _DapperKitProvider.Options.CommandTimeout, commandType: type) > 0;
            }
        }

        /// <summary>
        /// delete entity
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Delete<T>(T entity) where T : class
        {
            Check.Argument.IsNotNull(entity, nameof(entity));
            using (var conn = _DapperKitProvider.DbConnection)
            {
                return conn.Delete(entityToDelete: entity, commandTimeout: _DapperKitProvider.Options.CommandTimeout);
            }
        }

        /// <summary>
        /// delte entity collection
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entities">entity obj collection</param>
        /// <returns>success return true else return false</returns>
        public bool Delete<T>(IEnumerable<T> entities) where T : class
        {
            Check.Argument.IsNotNull(entities, nameof(entities));
            using (var conn = _DapperKitProvider.DbConnection)
            {
                //add transaction
                using (var tran = conn.BeginTransaction())
                {
                    return conn.Delete(entityToDelete: entities, transaction: tran, commandTimeout: _DapperKitProvider.Options.CommandTimeout);
                }
            }
        }

        /// <summary>
        /// delete all data
        /// </summary>
        /// <returns></returns>
        public bool DeleteAll<T>() where T : class
        {
            using (var conn = _DapperKitProvider.DbConnection)
            {
                //add transaction
                using (var tran = conn.BeginTransaction())
                {
                    return conn.DeleteAll<T>(transaction: tran, commandTimeout: _DapperKitProvider.Options.CommandTimeout);
                }
            }
        }

        /// <summary>
        /// Get all datas
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IEnumerable<T> GetAll<T>() where T : class
        {
            using (var conn = _DapperKitProvider.DbConnection)
            {
                return conn.GetAll<T>(commandTimeout: _DapperKitProvider.Options.CommandTimeout);
            }
        }

        /// <summary>
        /// Get data by id
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id">table primiary key id</param>
        /// <returns></returns>
        public T GetInfo<T>(object id) where T : class
        {
            Check.Argument.IsNotNull(id, nameof(id));

            using (var conn = _DapperKitProvider.DbConnection)
            {
                return conn.Get<T>(id: id, commandTimeout: _DapperKitProvider.Options.CommandTimeout);
            }
        }

        /// <summary>
        /// Get data with sql and param
        /// </summary>
        /// <typeparam name="T">type of entity</typeparam>
        /// <param name="sql">sql string</param>
        /// <param name="param">sql param</param>
        /// <param name="type"><see cref="CommandType"/></param>
        /// <returns></returns>
        public T GetInfo<T>(string sql, DynamicParameters param = null, CommandType? type = null) where T : class
        {
            Check.Argument.IsNotEmpty(sql, nameof(sql));

            using (var conn = _DapperKitProvider.DbConnection)
            {
                return conn.QueryFirstOrDefault<T>(sql, param, commandTimeout: _DapperKitProvider.Options.CommandTimeout, commandType: type);
            }
        }

        /// <summary>
        /// Get data collection with sql and param
        /// </summary>
        /// <typeparam name="T">type of entity</typeparam>
        /// <param name="sql">sql string</param>
        /// <param name="param">sql param</param>
        /// <param name="type"><see cref="CommandType"/></param>
        /// <returns></returns>
        public IEnumerable<T> GetList<T>(string sql, DynamicParameters param = null, CommandType? type = null) where T : class
        {
            Check.Argument.IsNotEmpty(sql, nameof(sql));

            using (var conn = _DapperKitProvider.DbConnection)
            {
                return conn.Query<T>(sql, param, commandTimeout: _DapperKitProvider.Options.CommandTimeout, commandType: type);
            }
        }

        /// <summary>
        /// Transation 
        /// </summary>
        /// <param name="action"><see cref="Action{T1, T2, T3}"/></param>
        /// <returns></returns>
        public bool Transation(Action<IDbConnection, IDbTransaction, int?> action)
        {
            using (IDbConnection conn = _DapperKitProvider.DbConnection)
            {
                using (IDbTransaction tran = conn.BeginTransaction())
                {
                    try
                    {
                        var commandTimeOut = _DapperKitProvider.Options.CommandTimeout;

                        action(conn, tran, commandTimeOut);
                        //tran commit
                        tran.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        //rollback if any exception
                        tran.Rollback();
                        return false;
                    }
                }
            }
        }

        #endregion

        #region Async

        /// <summary>
        /// Insert entity async
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Task<int> InsertAsync<T>(T entity) where T : class
        {
            Check.Argument.IsNotNull(entity, nameof(entity));
            using (var conn = _DapperKitProvider.DbConnection)
            {
                return conn.InsertAsync(entityToInsert: entity, commandTimeout: _DapperKitProvider.Options.CommandTimeout);
            }
        }

        /// <summary>
        /// Insert entity collection async
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entities">entity collection</param>
        /// <returns> success return row numbers ,else return 0</returns>
        public Task<int> InsertAsync<T>(IEnumerable<T> entities) where T : class
        {
            Check.Argument.IsNotNull(entities, nameof(entities));
            using (var conn = _DapperKitProvider.DbConnection)
            {
                //add transaction
                using (var tran = conn.BeginTransaction())
                {
                    return conn.InsertAsync(entityToInsert: entities, transaction: tran, commandTimeout: _DapperKitProvider.Options.CommandTimeout);
                }
            }
        }

        /// <summary>
        /// Update entity async
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity">entity obj</param>
        /// <returns>success return true else return false</returns>
        public Task<bool> UpdateAsync<T>(T entity) where T : class
        {
            Check.Argument.IsNotNull(entity, nameof(entity));
            using (var conn = _DapperKitProvider.DbConnection)
            {
                return conn.UpdateAsync(entityToUpdate: entity, commandTimeout: _DapperKitProvider.Options.CommandTimeout);
            }
        }

        /// <summary>
        /// Update entity collection async
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entities">entity obj collection</param>
        /// <returns>success return true else return false</returns>
        public Task<bool> UpdateAsync<T>(IEnumerable<T> entities) where T : class
        {
            Check.Argument.IsNotNull(entities, nameof(entities));

            using (var conn = _DapperKitProvider.DbConnection)
            {
                //add transaction
                using (var tran = conn.BeginTransaction())
                {
                    return conn.UpdateAsync(entityToUpdate: entities, transaction: tran, commandTimeout: _DapperKitProvider.Options.CommandTimeout);
                }
            }
        }

        /// <summary>
        /// update with sql and param async
        /// </summary>
        /// <param name="sql">update sql string</param>
        /// <param name="param">update params</param>
        /// <param name="type"><see cref="CommandType"/></param>
        /// <returns>success return true else return false</returns>
        public async Task<bool> UpdateAsync(string sql, DynamicParameters param = null, CommandType? type = default(CommandType?))
        {
            Check.Argument.IsNotEmpty(sql, nameof(sql));

            using (var conn = _DapperKitProvider.DbConnection)
            {
                var rows = await conn.ExecuteAsync(sql: sql, param: param, commandTimeout: _DapperKitProvider.Options.CommandTimeout, commandType: type);
                return rows > 0;
            }
        }

        /// <summary>
        /// delete entity async
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Task<bool> DeleteAsync<T>(T entity) where T : class
        {
            Check.Argument.IsNotNull(entity, nameof(entity));
            using (var conn = _DapperKitProvider.DbConnection)
            {
                return conn.DeleteAsync(entityToDelete: entity, commandTimeout: _DapperKitProvider.Options.CommandTimeout);
            }
        }

        /// <summary>
        /// delte entity collection async
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entities">entity obj collection</param>
        /// <returns>success return true else return false</returns>
        public Task<bool> DeleteAsync<T>(IEnumerable<T> entities) where T : class
        {
            Check.Argument.IsNotNull(entities, nameof(entities));

            using (var conn = _DapperKitProvider.DbConnection)
            {
                //add transaction
                using (var tran = conn.BeginTransaction())
                {
                    return conn.DeleteAsync(entityToDelete: entities, transaction: tran, commandTimeout: _DapperKitProvider.Options.CommandTimeout);
                }
            }
        }

        /// <summary>
        /// delete all data
        /// </summary>
        /// <returns></returns>
        public Task<bool> DeleteAllAsync<T>() where T : class
        {
            using (var conn = _DapperKitProvider.DbConnection)
            {
                //add transaction
                using (var tran = conn.BeginTransaction())
                {
                    return conn.DeleteAllAsync<T>(transaction: tran, commandTimeout: _DapperKitProvider.Options.CommandTimeout);
                }
            }
        }

        /// <summary>
        /// Get data by id
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id">table primiary key id</param>
        /// <returns></returns>
        public Task<T> GetInfoAsync<T>(object id) where T : class
        {
            Check.Argument.IsNotNull(id, nameof(id));

            using (var conn = _DapperKitProvider.DbConnection)
            {
                return conn.GetAsync<T>(id: id, commandTimeout: _DapperKitProvider.Options.CommandTimeout);
            }
        }

        /// <summary>
        /// Get data with sql and param
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql">sql string</param>
        /// <param name="param">sql param</param>
        /// <param name="type"><see cref="CommandType"/></param>
        /// <returns></returns>
        public Task<T> GetInfoAsync<T>(string sql, DynamicParameters param = null, CommandType? type = null) where T : class
        {
            Check.Argument.IsNotEmpty(sql, nameof(sql));

            using (var conn = _DapperKitProvider.DbConnection)
            {
                return conn.QueryFirstOrDefaultAsync<T>(sql, param, commandTimeout: _DapperKitProvider.Options.CommandTimeout, commandType: type);
            }
        }

        /// <summary>
        /// Get data collection with sql and param
        /// </summary>
        /// <typeparam name="T">type of entity</typeparam>
        /// <param name="sql">sql string</param>
        /// <param name="param">sql param</param>
        /// <param name="type"><see cref="CommandType"/></param>
        /// <returns></returns>
        public Task<IEnumerable<T>> GetListAsync<T>(string sql, DynamicParameters param = null, CommandType? type = null) where T : class
        {
            Check.Argument.IsNotEmpty(sql, nameof(sql));

            using (var conn = _DapperKitProvider.DbConnection)
            {
                return conn.QueryAsync<T>(sql, param, commandTimeout: _DapperKitProvider.Options.CommandTimeout, commandType: type);
            }
        }

        /// <summary>
        /// Get all datas
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public Task<IEnumerable<T>> GetAllAsync<T>() where T : class
        {
            using (var conn = _DapperKitProvider.DbConnection)
            {
                return conn.GetAllAsync<T>(commandTimeout: _DapperKitProvider.Options.CommandTimeout);
            }
        }


        /// <summary>
        /// Transation 
        /// </summary>
        /// <param name="action"><see cref="Action{T1, T2, T3}"/></param>
        /// <returns></returns>
        public Task<bool> TransationAsync(Action<IDbConnection, IDbTransaction, int?> action)
        {
            return Task.Factory.StartNew(() =>
            {
                using (IDbConnection conn = _DapperKitProvider.DbConnection)
                {
                    using (IDbTransaction tran = conn.BeginTransaction())
                    {
                        try
                        {
                            var commandTimeOut = _DapperKitProvider.Options.CommandTimeout;

                            action(conn, tran, commandTimeOut);
                            //tran commit
                            tran.Commit();
                            return true;
                        }
                        catch (Exception ex)
                        {
                            //rollback if any exception
                            tran.Rollback();
                            return false;
                        }
                    }
                }
            });
        }
        #endregion

    }
}
