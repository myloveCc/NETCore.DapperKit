using System;
using Xunit;
using NETCore.DapperKit.Extensions;
using NETCore.DapperKit.ExpressionToSql.Extensions;
using NETCore.DapperKit.Tests.Model;

namespace NETCore.DapperKit.Tests
{
    public class Update_Tests
    {
        private readonly IDapperKitProvider _DapperContext;

        public Update_Tests()
        {
            _DapperContext = new DapperKitProvider(new Infrastructure.Internal.DapperKitOptions()
            {
                ConnectionString = "127.0.0.1",
                DatabaseType = Infrastructure.Internal.DatabaseType.SQLServer
            });
        }

        [Fact(DisplayName = "Update all test")]
        public void Update_All_Test()
        {
            var query = _DapperContext.DataSet<SysUser>().Update(() => new SysUser() { Account = "AllUpdate", Password = "123456", IsAdmin = true });
            var sqlBuilder = query.SqlBuilder;

            var sql = sqlBuilder.GetSql();
            var sqlParams = sqlBuilder.GetSqlParams();

            Assert.NotEmpty(sql);
            Assert.Equal("UPDATE [SysUser] SET [Account] = @param0,[Password] = @param1,[IsAdmin] = @param2;", sql);
            Assert.NotNull(sqlParams);
            Assert.Equal(3, sqlParams.Count);
            Assert.Equal(1, sqlParams["@param2"]);
        }

        [Fact(DisplayName = "Update exclude not table column test")]
        public void Update_Exclude_UnColumn_Test()
        {
            var query = _DapperContext.DataSet<SysUser>().Update(() => new SysUser() { Account = "AllUpdate", Password = "123456", IsAdmin = true, UserRoleName = "Test" });
            var sqlBuilder = query.SqlBuilder;

            var sql = sqlBuilder.GetSql();
            var sqlParams = sqlBuilder.GetSqlParams();

            Assert.NotEmpty(sql);
            Assert.Equal("UPDATE [SysUser] SET [Account] = @param0,[Password] = @param1,[IsAdmin] = @param2;", sql);
            Assert.NotNull(sqlParams);
            Assert.Equal(3, sqlParams.Count);
            Assert.Equal(1, sqlParams["@param2"]);
        }

        [Fact(DisplayName = "Update exclude key column test")]
        public void Update_Exclude_KeyColumn_Test()
        {
            var query = _DapperContext.DataSet<SysUser>().Update(() => new SysUser() { Id = 1, Account = "AllUpdate", Password = "123456", IsAdmin = true });
            var sqlBuilder = query.SqlBuilder;

            var sql = sqlBuilder.GetSql();
            var sqlParams = sqlBuilder.GetSqlParams();

            Assert.NotEmpty(sql);
            Assert.Equal("UPDATE [SysUser] SET [Account] = @param0,[Password] = @param1,[IsAdmin] = @param2;", sql);
            Assert.NotNull(sqlParams);
            Assert.Equal(3, sqlParams.Count);
            Assert.Equal(1, sqlParams["@param2"]);
        }

        [Fact(DisplayName = "Update new object where test")]
        public void Update_New_Where_Test()
        {
            var query = _DapperContext.DataSet<SysUser>().Update(() => new SysUser() { Id = 1, Account = "AllUpdate", Password = "123456", IsAdmin = true }).Where(m => m.Id == 1);
            var sqlBuilder = query.SqlBuilder;

            var sql = sqlBuilder.GetSql();
            var sqlParams = sqlBuilder.GetSqlParams();

            Assert.NotEmpty(sql);
            Assert.Equal("UPDATE [SysUser] SET [Account] = @param0,[Password] = @param1,[IsAdmin] = @param2 WHERE [Id] = @param3;", sql);
            Assert.NotNull(sqlParams);
            Assert.Equal(4, sqlParams.Count);
            Assert.Equal(1, sqlParams["@param2"]);
        }
    }
}
