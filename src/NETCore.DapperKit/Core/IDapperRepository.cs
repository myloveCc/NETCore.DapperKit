using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace NETCore.DapperKit.Core
{
    public interface IDapperRepository
    {
        #region Sync

        long Insert<T>(T entity) where T : class;

        long Insert<T>(IEnumerable<T> entities) where T : class;

        bool Update<T>(T entity) where T : class;

        bool Update(string sql, DynamicParameters param = null, CommandType? type = null);

        bool Update<T>(IEnumerable<T> entities) where T : class;

        bool Delete<T>(T entity) where T : class;

        bool Delete<T>(IEnumerable<T> entities) where T : class;

        bool DeleteAll<T>() where T : class;

        T GetInfo<T>(dynamic id) where T : class;

        T GetInfo<T>(string sql, DynamicParameters param = null, CommandType? type = null) where T : class;

        IEnumerable<T> GetAll<T>() where T : class;

        bool Transation(Action<IDbConnection, IDbTransaction, int?> action);

        #endregion

        #region Async
        Task<int> InsertAsync<T>(T entity) where T : class;

        Task<int> InsertAsync<T>(IEnumerable<T> entities) where T : class;

        Task<bool> UpdateAsync<T>(T entity) where T : class;

        Task<bool> UpdateAsync(string sql, DynamicParameters param = null, CommandType? type = null);

        Task<bool> UpdateAsync<T>(IEnumerable<T> entities) where T : class;

        Task<bool> DeleteAsync<T>(T entity) where T : class;

        Task<bool> DeleteAsync<T>(IEnumerable<T> entities) where T : class;

        Task<bool> DeleteAllAsync<T>() where T : class;

        Task<T> GetInfoAsync<T>(dynamic id) where T : class;

        Task<T> GetInfoAsync<T>(string sql, DynamicParameters param = null, CommandType? type = null) where T : class;

        Task<IEnumerable<T>> GetAllAsync<T>() where T : class;

        Task<bool> TransationAsync(Action<IDbConnection, IDbTransaction, int?> action);

        #endregion
    }
}
