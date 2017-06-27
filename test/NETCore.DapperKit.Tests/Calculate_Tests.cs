using System;
using Xunit;
using NETCore.DapperKit.Tests.Model;


namespace NETCore.DapperKit.Tests
{
    public class Calculate_Tests
    {
        private readonly IDapperContext _DapperContext;

        public Calculate_Tests()
        {
            _DapperContext = new DapperContext(new DapperKitOptions()
            {
                ConnectionString = "127.0.0.1",
                DatabaseType = DatabaseType.SQLServer
            });
        }

        [Fact(DisplayName = "Select count test")]
        public void Select_Count_Test()
        {
            var query = _DapperContext.DbSet<SysUser>().Count();
            var sqlBuilder = query.SqlBuilder;

            var sql = sqlBuilder.GetSql();

            Assert.Equal("SELECT COUNT(*) FROM [SysUser];", sql);
        }

        [Fact(DisplayName = "Select count column test")]
        public void Select_Count_Column_Test()
        {
            var query = _DapperContext.DbSet<SysUser>().Count(m => m.Id);
            var sqlBuilder = query.SqlBuilder;

            var sql = sqlBuilder.GetSql();

            Assert.Equal("SELECT COUNT([Id]) FROM [SysUser];", sql);
        }

        [Fact(DisplayName = "Select count where test")]
        public void Select_Count_Where_Test()
        {
            var query = _DapperContext.DbSet<SysUser>().Count(m => m.Id).Where(m => m.Id >= 10);
            var sqlBuilder = query.SqlBuilder;

            var sql = sqlBuilder.GetSql();

            Assert.Equal("SELECT COUNT([Id]) FROM [SysUser] WHERE [Id] >= @param0;", sql);
        }

        [Fact(DisplayName = "Select avg column test")]
        public void Select_Avg_Column_Test()
        {
            var query = _DapperContext.DbSet<SysUser>().Avg(m => m.Id);
            var sqlBuilder = query.SqlBuilder;

            var sql = sqlBuilder.GetSql();

            Assert.Equal("SELECT AVG([Id]) FROM [SysUser];", sql);
        }

        [Fact(DisplayName = "Select avg where test")]
        public void Select_Avg_Where_Test()
        {
            var query = _DapperContext.DbSet<SysUser>().Avg(m => m.Id).Where(m => m.Id >= 10);
            var sqlBuilder = query.SqlBuilder;

            var sql = sqlBuilder.GetSql();

            Assert.Equal("SELECT AVG([Id]) FROM [SysUser] WHERE [Id] >= @param0;", sql);
        }


        [Fact(DisplayName = "Select sum column test")]
        public void Select_Sum_Column_Test()
        {
            var query = _DapperContext.DbSet<SysUser>().Sum(m => m.Id);
            var sqlBuilder = query.SqlBuilder;

            var sql = sqlBuilder.GetSql();

            Assert.Equal("SELECT SUM([Id]) FROM [SysUser];", sql);
        }

        [Fact(DisplayName = "Select sum where test")]
        public void Select_Sum_Where_Test()
        {
            var query = _DapperContext.DbSet<SysUser>().Sum(m => m.Id).Where(m => m.Id >= 10);
            var sqlBuilder = query.SqlBuilder;

            var sql = sqlBuilder.GetSql();

            Assert.Equal("SELECT SUM([Id]) FROM [SysUser] WHERE [Id] >= @param0;", sql);
        }

        [Fact(DisplayName = "Select min column test")]
        public void Select_Min_Column_Test()
        {
            var query = _DapperContext.DbSet<SysUser>().Min(m => m.Id);
            var sqlBuilder = query.SqlBuilder;

            var sql = sqlBuilder.GetSql();

            Assert.Equal("SELECT MIN([Id]) FROM [SysUser];", sql);
        }

        [Fact(DisplayName = "Select min where test")]
        public void Select_Min_Where_Test()
        {
            var query = _DapperContext.DbSet<SysUser>().Min(m => m.Id).Where(m => m.Id >= 10);
            var sqlBuilder = query.SqlBuilder;

            var sql = sqlBuilder.GetSql();

            Assert.Equal("SELECT MIN([Id]) FROM [SysUser] WHERE [Id] >= @param0;", sql);
        }
    }
}
