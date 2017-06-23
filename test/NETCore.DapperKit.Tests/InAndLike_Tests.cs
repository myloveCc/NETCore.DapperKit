using System;
using Xunit;
using NETCore.DapperKit.Extensions;
using NETCore.DapperKit.ExpressionToSql.Extensions;
using NETCore.DapperKit.Tests.Model;
using System.Collections.Generic;

namespace NETCore.DapperKit.Tests
{
    public class InAndLike_Tests
    {
        private readonly IDapperKitProvider _DapperContext;

        public InAndLike_Tests()
        {
            _DapperContext = new DapperKitProvider(new Infrastructure.Internal.DapperKitOptions()
            {
                ConnectionString = "127.0.0.1",
                DatabaseType = Infrastructure.Internal.DatabaseType.SQLServer
            });
        }

        [Fact(DisplayName = "Select in new int list test")]
        public void Select_InNewList_Test()
        {
            var query = _DapperContext.DataSet<SysUser>().Select().Where(m => m.Id.In(new List<int> { 1, 2, 3 }));
            var sqlBuilder = query.SqlBuilder;

            var sql = sqlBuilder.GetSqlString();

            Assert.Equal("SELECT * FROM [SysUser] a WHERE a.[Id] IN (1,2,3);", sql);
        }


        [Fact(DisplayName = "Select where in new int list test")]
        public void Select_Where_InNewList_Test()
        {
            var query = _DapperContext.DataSet<SysUser>().Select().Where(m => m.Id.In(new List<int> { 1, 2, 3 }) && m.IsAdmin);
            var sqlBuilder = query.SqlBuilder;

            var sql = sqlBuilder.GetSqlString();
            var sqlParams = sqlBuilder.GetSqlParameters();

            Assert.Equal("SELECT * FROM [SysUser] a WHERE a.[Id] IN (1,2,3) AND a.[IsAdmin] = @param0;", sql);
            Assert.Equal(1, sqlParams.Count);
            Assert.Equal(1, sqlParams["@param0"]);
        }

        [Fact(DisplayName = "Select in int list test")]
        public void Select_Where_InList_Test()
        {
            var ids = new List<int> { 1, 2, 3 };

            var query = _DapperContext.DataSet<SysUser>().Select().Where(m => m.Id.In(ids));
            var sqlBuilder = query.SqlBuilder;

            var sql = sqlBuilder.GetSqlString();

            Assert.Equal("SELECT * FROM [SysUser] a WHERE a.[Id] IN (1,2,3);", sql);
        }

        [Fact(DisplayName = "Select in new string list test")]
        public void Select_InNewStringList_Test()
        {
            var query = _DapperContext.DataSet<SysUser>().Select().Where(m => m.Account.In(new List<string> { "admin", "test", "dev" }));
            var sqlBuilder = query.SqlBuilder;

            var sql = sqlBuilder.GetSqlString();

            Assert.Equal("SELECT * FROM [SysUser] a WHERE a.[Account] IN ('admin','test','dev');", sql);
        }

        [Fact(DisplayName = "Select in list string test")]
        public void Select_InStringList_Test()
        {
            var accounts = new List<string> { "admin", "test", "dev" };

            var query = _DapperContext.DataSet<SysUser>().Select().Where(m => m.Account.In(accounts));
            var sqlBuilder = query.SqlBuilder;

            var sql = sqlBuilder.GetSqlString();

            Assert.Equal("SELECT * FROM [SysUser] a WHERE a.[Account] IN ('admin','test','dev');", sql);
        }

        [Fact(DisplayName = "Select contains test")]
        public void Select_Contains_Test()
        {
            var query = _DapperContext.DataSet<SysUser>().Select().Where(m => m.Account.Contains("admin"));
            var sqlBuilder = query.SqlBuilder;

            var sql = sqlBuilder.GetSqlString();

            Assert.Equal("SELECT * FROM [SysUser] a WHERE a.[Account] LIKE '%admin%';", sql);
        }

        [Fact(DisplayName = "Select contains test 1")]
        public void Select_Contains1_Test()
        {
            var containStr = "admin";
            var query = _DapperContext.DataSet<SysUser>().Select().Where(m => m.Account.Contains(containStr));
            var sqlBuilder = query.SqlBuilder;

            var sql = sqlBuilder.GetSqlString();

            Assert.Equal("SELECT * FROM [SysUser] a WHERE a.[Account] LIKE '%admin%';", sql);
        }

        [Fact(DisplayName = "Select where contains test")]
        public void Select_Where_Contains_Test()
        {
            var query = _DapperContext.DataSet<SysUser>().Select().Where(m => m.Account.Contains("admin") && m.IsAdmin);
            var sqlBuilder = query.SqlBuilder;

            var sql = sqlBuilder.GetSqlString();
            var sqlParams = sqlBuilder.GetSqlParameters();

            Assert.Equal("SELECT * FROM [SysUser] a WHERE a.[Account] LIKE '%admin%' AND a.[IsAdmin] = @param0;", sql);
            Assert.Equal(1, sqlParams.Count);
            Assert.Equal(1, sqlParams["@param0"]);
        }

        [Fact(DisplayName = "Select like test")]
        public void Select_Like_Test()
        {
            var query = _DapperContext.DataSet<SysUser>().Select().Where(m => m.Account.Like("admin"));
            var sqlBuilder = query.SqlBuilder;

            var sql = sqlBuilder.GetSqlString();

            Assert.Equal("SELECT * FROM [SysUser] a WHERE a.[Account] LIKE '%admin%';", sql);
        }

        [Fact(DisplayName = "Select where like test")]
        public void Select_Where_Like_Test()
        {
            var query = _DapperContext.DataSet<SysUser>().Select().Where(m => m.Account.Like("admin") && m.IsAdmin);
            var sqlBuilder = query.SqlBuilder;

            var sql = sqlBuilder.GetSqlString();
            var sqlParams = sqlBuilder.GetSqlParameters();

            Assert.Equal("SELECT * FROM [SysUser] a WHERE a.[Account] LIKE '%admin%' AND a.[IsAdmin] = @param0;", sql);
            Assert.Equal(1, sqlParams.Count);
            Assert.Equal(1, sqlParams["@param0"]);
        }

        [Fact(DisplayName = "Select like left test")]
        public void Select_LikeLeft_Test()
        {
            var query = _DapperContext.DataSet<SysUser>().Select().Where(m => m.Account.LikeLeft("admin"));
            var sqlBuilder = query.SqlBuilder;

            var sql = sqlBuilder.GetSqlString();

            Assert.Equal("SELECT * FROM [SysUser] a WHERE a.[Account] LIKE '%admin';", sql);
        }

        [Fact(DisplayName = "Select where like left test")]
        public void Select_Where_LikeLeft_Test()
        {
            var query = _DapperContext.DataSet<SysUser>().Select().Where(m => m.Account.LikeLeft("admin") && m.IsAdmin);
            var sqlBuilder = query.SqlBuilder;

            var sql = sqlBuilder.GetSqlString();
            var sqlParams = sqlBuilder.GetSqlParameters();

            Assert.Equal("SELECT * FROM [SysUser] a WHERE a.[Account] LIKE '%admin' AND a.[IsAdmin] = @param0;", sql);
            Assert.Equal(1, sqlParams.Count);
            Assert.Equal(1, sqlParams["@param0"]);
        }

        [Fact(DisplayName = "Select like left test")]
        public void Select_LikeRight_Test()
        {
            var query = _DapperContext.DataSet<SysUser>().Select().Where(m => m.Account.LikeRight("admin"));
            var sqlBuilder = query.SqlBuilder;

            var sql = sqlBuilder.GetSqlString();

            Assert.Equal("SELECT * FROM [SysUser] a WHERE a.[Account] LIKE 'admin%';", sql);
        }

        [Fact(DisplayName = "Select where like right test")]
        public void Select_Where_LikeRight_Test()
        {
            var query = _DapperContext.DataSet<SysUser>().Select().Where(m => m.Account.LikeRight("admin") && m.IsAdmin);
            var sqlBuilder = query.SqlBuilder;

            var sql = sqlBuilder.GetSqlString();
            var sqlParams = sqlBuilder.GetSqlParameters();

            Assert.Equal("SELECT * FROM [SysUser] a WHERE a.[Account] LIKE 'admin%' AND a.[IsAdmin] = @param0;", sql);
            Assert.Equal(1, sqlParams.Count);
            Assert.Equal(1, sqlParams["@param0"]);
        }

    }
}
