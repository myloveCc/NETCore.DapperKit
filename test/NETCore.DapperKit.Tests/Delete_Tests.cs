using System;
using Xunit;
using NETCore.DapperKit.Extensions;
using NETCore.DapperKit.ExpressionVisitor.Extensions;
using NETCore.DapperKit.Tests.Model;

namespace NETCore.DapperKit.Tests
{
    public class Delete_Tests
    {
        private readonly IDapperContext _DapperContext;

        public Delete_Tests()
        {
            _DapperContext = new DapperContext(new DapperKitOptions()
            {
                ConnectionString = "127.0.0.1",
                DatabaseType = DatabaseType.SQLServer
            });
        }

        [Fact(DisplayName = "Delete all test")]
        public void Delete_All_Test()
        {
            var query = _DapperContext.DbSet<SysUser>().Delete();
            var sqlBuilder = query.SqlBuilder;

            var sql = sqlBuilder.GetSql();

            Assert.Equal("DELETE [SysUser];", sql);
        }

        [Fact(DisplayName = "Delete where test")]
        public void Delete_Where_Test()
        {
            var query = _DapperContext.DbSet<SysUser>().Delete(m => m.Id == 1 && m.Account == "admin");
            var sqlBuilder = query.SqlBuilder;

            var sql = sqlBuilder.GetSql();

            Assert.Equal("DELETE [SysUser] WHERE [Id] = @param0 AND [Account] = @param1;", sql);
        }

        [Fact(DisplayName = "Delete with is admin test")]
        public void Delete_Where_IsAdmin_Test()
        {
            var query = _DapperContext.DbSet<SysUser>().Delete(m => m.IsAdmin && m.Id == 1 && m.Account == "admin");
            var sqlBuilder = query.SqlBuilder;

            var sql = sqlBuilder.GetSql();
            var sqlParams = sqlBuilder.GetSqlParams();

            Assert.Equal("DELETE [SysUser] WHERE [IsAdmin] = @param0 AND [Id] = @param1 AND [Account] = @param2;", sql);
            Assert.Equal(1, sqlParams["@param0"]);
        }

        [Fact(DisplayName = "Delete with is admin true test")]
        public void Delete_Where_IsAdmin_True_Test()
        {
            var query = _DapperContext.DbSet<SysUser>().Delete(m => m.IsAdmin == true && m.Id == 1 && m.Account == "admin");
            var sqlBuilder = query.SqlBuilder;

            var sql = sqlBuilder.GetSql();
            var sqlParams = sqlBuilder.GetSqlParams();

            Assert.Equal("DELETE [SysUser] WHERE [IsAdmin] = @param0 AND [Id] = @param1 AND [Account] = @param2;", sql);
            Assert.Equal(1, sqlParams["@param0"]);
        }


        [Fact(DisplayName = "Delete with is not admin test")]
        public void Delete_Where_IsNotAdmin_Test()
        {
            var query = _DapperContext.DbSet<SysUser>().Delete(m => !m.IsAdmin && m.Id == 1 && m.Account == "admin");
            var sqlBuilder = query.SqlBuilder;

            var sql = sqlBuilder.GetSql();
            var sqlParams = sqlBuilder.GetSqlParams();

            Assert.Equal("DELETE [SysUser] WHERE [IsAdmin] = @param0 AND [Id] = @param1 AND [Account] = @param2;", sql);
            Assert.Equal(0, sqlParams["@param0"]);
        }

        [Fact(DisplayName = "Delete with is admin false test")]
        public void Delete_Where_IsAdmin_False_Test()
        {
            var query = _DapperContext.DbSet<SysUser>().Delete(m => m.IsAdmin == false && m.Id == 1 && m.Account == "admin");
            var sqlBuilder = query.SqlBuilder;

            var sql = sqlBuilder.GetSql();
            var sqlParams = sqlBuilder.GetSqlParams();

            Assert.Equal("DELETE [SysUser] WHERE [IsAdmin] = @param0 AND [Id] = @param1 AND [Account] = @param2;", sql);
            Assert.Equal(0, sqlParams["@param0"]);
        }

        [Fact(DisplayName = "Delete with datetime converter test")]
        public void Delete_Where_DateTime_Converter_Test()
        {
            var query = _DapperContext.DbSet<SysUser>().Delete(m => m.CreateTime >= Convert.ToDateTime("1987-01-28"));
            var sqlBuilder = query.SqlBuilder;

            var sql = sqlBuilder.GetSql();
            var sqlParams = sqlBuilder.GetSqlParams();

            Assert.Equal("DELETE [SysUser] WHERE [CreateTime] >= @param0;", sql);
            Assert.Equal("1987-01-28", Convert.ToDateTime(sqlParams["@param0"]).ToString("yyyy-MM-dd"));
        }

        [Fact(DisplayName = "Delete with datetime value test ")]
        public void Delete_Where_DateTime_Value_Test()
        {
            var time = Convert.ToDateTime("1987-01-28");
            var query = _DapperContext.DbSet<SysUser>().Delete(m => m.CreateTime >= time);
            var sqlBuilder = query.SqlBuilder;

            var sql = sqlBuilder.GetSql();
            var sqlParams = sqlBuilder.GetSqlParams();

            Assert.Equal("DELETE [SysUser] WHERE [CreateTime] >= @param0;", sql);
            Assert.Equal("1987-01-28", Convert.ToDateTime(sqlParams["@param0"]).ToString("yyyy-MM-dd"));
        }

        [Fact(DisplayName = "Delete with datetime now test ")]
        public void Delete_Where_DateTime_Now_Test()
        {
            var query = _DapperContext.DbSet<SysUser>().Delete(m => m.CreateTime >= DateTime.Now);
            var sqlBuilder = query.SqlBuilder;

            var sql = sqlBuilder.GetSql();
            var sqlParams = sqlBuilder.GetSqlParams();

            Assert.Equal("DELETE [SysUser] WHERE [CreateTime] >= @param0;", sql);
            Assert.Equal(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),Convert.ToDateTime(sqlParams["@param0"]).ToString("yyyy-MM-dd HH:mm:ss"));
        }


        [Fact(DisplayName = "Delete with datetime null test")]
        public void Delete_Where_DateTime_Null_Test()
        {
            var query = _DapperContext.DbSet<SysUser>().Delete(m => m.CreateTime == null);
            var sqlBuilder = query.SqlBuilder;

            var sql = sqlBuilder.GetSql();
            var sqlParams = sqlBuilder.GetSqlParams();

            Assert.Equal("DELETE [SysUser] WHERE [CreateTime] IS @param0;", sql);
            Assert.Null(sqlParams["@param0"]);

        }
    }
}
