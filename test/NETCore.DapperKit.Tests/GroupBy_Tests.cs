using System;
using Xunit;
using NETCore.DapperKit.Extensions;
using NETCore.DapperKit.ExpressionToSql.Extensions;
using NETCore.DapperKit.Tests.Model;
using System.Collections.Generic;


namespace NETCore.DapperKit.Tests
{
    public class GroupBy_Tests
    {

        private readonly IDapperKitProvider _DapperContext;

        public GroupBy_Tests()
        {
            _DapperContext = new DapperKitProvider(new Infrastructure.Internal.DapperKitOptions()
            {
                ConnectionString = "127.0.0.1",
                DatabaseType = Infrastructure.Internal.DatabaseType.SQLServer
            });
        }

        [Fact(DisplayName = "Calculate group by test")]
        public void Calculate_Groupby_Test()
        {
            var query = _DapperContext.DataSet<SysUser>().Count(m => m.Id).GroupBy(m => m.IsAdmin);
            var sqlBuilder = query.SqlBuilder;

            var sql = sqlBuilder.GetSql();

            Assert.Equal("SELECT COUNT([Id]),[IsAdmin] FROM [SysUser] GROUP BY [IsAdmin];", sql);
        }

        [Fact(DisplayName = "Calculate group by alias test ")]
        public void Calculate_Groupby_Alias_Test()
        {
            var query = _DapperContext.DataSet<SysUser>().Count(m => new { UserCount = m.Id }).GroupBy(m => new { AmdinType = m.IsAdmin });
            var sqlBuilder = query.SqlBuilder;

            var sql = sqlBuilder.GetSql();

            Assert.Equal("SELECT COUNT([Id]) [UserCount],[IsAdmin] [AmdinType] FROM [SysUser] GROUP BY [IsAdmin];", sql);
        }

        [Fact(DisplayName = "Calculate group by and where test")]
        public void Select_Groupby_Where_Test()
        {
            var query = _DapperContext.DataSet<SysUser>().Count(m => new { UserCount = m.Id }).GroupBy(m => new { AmdinType = m.IsAdmin }).Where(m => m.Id == 1);
            var sqlBuilder = query.SqlBuilder;

            var sql = sqlBuilder.GetSql();
            var paras= sqlBuilder.GetSqlParams();

            Assert.Equal("SELECT COUNT([Id]) [UserCount],[IsAdmin] [AmdinType] FROM [SysUser] WHERE [Id] = @param0 GROUP BY [IsAdmin];", sql);
            Assert.Equal(1, paras.Count);
            Assert.Equal(1, paras["@param0"]);
        }
    }
}
