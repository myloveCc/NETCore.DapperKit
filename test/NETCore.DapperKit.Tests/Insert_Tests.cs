using System;
using Xunit;
using NETCore.DapperKit.Extensions;
using NETCore.DapperKit.ExpressionToSql.Extensions;
using NETCore.DapperKit.Tests.Model;

namespace NETCore.DapperKit.Tests
{
    public class Insert_Tests
    {
        private readonly IDapperKitProvider _Provider;

        public Insert_Tests()
        {
            _Provider = new DapperKitProvider(new Infrastructure.Internal.DapperKitOptions()
            {
                ConnectionString = "127.0.0.1",
                DatabaseType = Infrastructure.Internal.DatabaseType.SQLServer
            });
        }

        [Fact(DisplayName = "Insert new test")]
        public void Insert_New_Test()
        {

            var query = _Provider.DataSet<SysUser>().Insert(() => new SysUser() { Account = "admin", Password = "123456", IsAdmin = false, UserRoleName = "Test", CreateTime = Convert.ToDateTime("1987-01-28") });
            var sqlBuilder = query.SqlBuilder;

            var sql = sqlBuilder.GetSqlString();
            var paramVals = sqlBuilder.GetSqlParameters();

            Assert.NotEmpty(sql);
            Assert.Equal("INSERT INTO [SysUser] ([Account],[Password],[IsAdmin],[CreateTime]) VALUES (@param0,@param1,@param2,@param3);", sql);
            Assert.Equal(4, paramVals.Count);
        }

        [Fact(DisplayName = "Insert instance test")]
        public void Insert_Instance_Test()
        {
            var user = new SysUser()
            {
                Account = "admin",
                Password = "123456",
                IsAdmin = false,
                UserRoleName = "Test",
                CreateTime = Convert.ToDateTime("1987-01-28")
            };

            var query = _Provider.DataSet<SysUser>().Insert(() => user);
            var sqlBuilder = query.SqlBuilder;

            var sql = sqlBuilder.GetSqlString();
            var paramVals = sqlBuilder.GetSqlParameters();

            Assert.NotEmpty(sql);
            Assert.Equal("INSERT INTO [SysUser] ([Account],[Password],[IsAdmin],[CreateTime]) VALUES (@param0,@param1,@param2,@param3);", sql);
            Assert.Equal(4, paramVals.Count);
        }
    }
}
