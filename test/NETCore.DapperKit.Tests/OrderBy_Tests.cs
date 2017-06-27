using System;
using Xunit;
using NETCore.DapperKit.Extensions;
using NETCore.DapperKit.ExpressionVisitor.Extensions;
using NETCore.DapperKit.Tests.Model;
using System.Collections.Generic;

namespace NETCore.DapperKit.Tests
{
    public class OrderBy_Tests
    {
        private readonly IDapperContext _DapperContext;

        public OrderBy_Tests()
        {
            _DapperContext = new DapperContext(new DapperKitOptions()
            {
                ConnectionString = "127.0.0.1",
                DatabaseType = DatabaseType.SQLServer
            });
        }


        [Fact(DisplayName = "Select order by test")]
        public void Select_Orderby_Test()
        {
            var query = _DapperContext.DbSet<SysUser>().Select().OrderBy(m => m.CreateTime);
            var sqlBuilder = query.SqlBuilder;

            var sql = sqlBuilder.GetSql();

            Assert.Equal("SELECT * FROM [SysUser] a ORDER BY a.[CreateTime] ASC;", sql);
        }

        [Fact(DisplayName = "Select order by multi test")]
        public void Select_Orderby_Multi_Test()
        {
            var query = _DapperContext.DbSet<SysUser>().Select().OrderBy(m => new { m.Id, m.CreateTime });
            var sqlBuilder = query.SqlBuilder;

            var sql = sqlBuilder.GetSql();

            Assert.Equal("SELECT * FROM [SysUser] a ORDER BY a.[Id] ASC ,a.[CreateTime] ASC;", sql);
        }

        [Fact(DisplayName = "Select order by and then test")]
        public void Select_Orderby_Then_Test()
        {
            var query = _DapperContext.DbSet<SysUser>().Select().OrderBy(m => m.Id).ThenBy(m => m.CreateTime);
            var sqlBuilder = query.SqlBuilder;

            var sql = sqlBuilder.GetSql();

            Assert.Equal("SELECT * FROM [SysUser] a ORDER BY a.[Id] ASC ,a.[CreateTime] ASC;", sql);

        }


        [Fact(DisplayName = "Select order by and then desc test")]
        public void Select_Orderby_ThenDesc_Test()
        {
            var query = _DapperContext.DbSet<SysUser>().Select().OrderBy(m => m.Id).ThenByDescending(m => m.CreateTime);
            var sqlBuilder = query.SqlBuilder;

            var sql = sqlBuilder.GetSql();

            Assert.Equal("SELECT * FROM [SysUser] a ORDER BY a.[Id] ASC ,a.[CreateTime] DESC;", sql);
        }

        [Fact(DisplayName = "Select order by desc then asc test")]
        public void Select_OrderbyDesc_Then_Test()
        {
            var query = _DapperContext.DbSet<SysUser>().Select().OrderByDescending(m => m.Id).ThenBy(m => m.CreateTime);
            var sqlBuilder = query.SqlBuilder;

            var sql = sqlBuilder.GetSql();

            Assert.Equal("SELECT * FROM [SysUser] a ORDER BY a.[Id] DESC ,a.[CreateTime] ASC;", sql);
        }


        [Fact(DisplayName = "Select order by desc then desc test")]
        public void Select_OrderbyDesc_ThenDesc_Test()
        {
            var query = _DapperContext.DbSet<SysUser>().Select(m => m.Id).OrderByDescending(m => m.Id).ThenByDescending(m => m.CreateTime);
            var sqlBuilder = query.SqlBuilder;

            var sql = sqlBuilder.GetSql();

            Assert.Equal("SELECT a.[Id] [Id] FROM [SysUser] a ORDER BY a.[Id] DESC ,a.[CreateTime] DESC;", sql);
        }

    }
}
