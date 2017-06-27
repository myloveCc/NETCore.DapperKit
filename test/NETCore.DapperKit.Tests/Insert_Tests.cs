using System;
using Xunit;
using NETCore.DapperKit.Extensions;
using NETCore.DapperKit.ExpressionVisitor.Extensions;
using NETCore.DapperKit.Tests.Model;

namespace NETCore.DapperKit.Tests
{
    public class Insert_Tests
    {
        private readonly IDapperContext _DapperContext;

        public Insert_Tests()
        {
            _DapperContext = new DapperContext(new DapperKitOptions()
            {
                ConnectionString = "127.0.0.1",
                DatabaseType = DatabaseType.SQLServer
            });
        }

        [Fact(DisplayName = "Insert new test")]
        public void Insert_New_Test()
        {

            var query = _DapperContext.DbSet<SysUser>().Insert(() => new SysUser() { Account = "admin", Password = "123456", IsAdmin = false, UserRoleName = "Test", CreateTime = Convert.ToDateTime("1987-01-28") });
            var sqlBuilder = query.SqlBuilder;

            var sql = sqlBuilder.GetSql();
            var paramVals = sqlBuilder.GetSqlParams();

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

            var query = _DapperContext.DbSet<SysUser>().Insert(() => user);
            var sqlBuilder = query.SqlBuilder;

            var sql = sqlBuilder.GetSql();
            var paramVals = sqlBuilder.GetSqlParams();

            Assert.NotEmpty(sql);
            Assert.Equal("INSERT INTO [SysUser] ([Account],[Password],[IsAdmin],[CreateTime]) VALUES (@param0,@param1,@param2,@param3);", sql);
            Assert.Equal(4, paramVals.Count);
        }
    }
}
