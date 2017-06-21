using System;
using Xunit;
using NETCore.DapperKit.Extensions;
using NETCore.DapperKit.ExpressionToSql.Extensions;
using NETCore.DapperKit.Tests.Model;

namespace NETCore.DapperKit.Tests
{
    public class Select_Tests
    {
        private readonly IDapperKitProvider _DapperContext;

        public Select_Tests()
        {
            _DapperContext = new DapperKitProvider(new Infrastructure.Internal.DapperKitOptions()
            {
                ConnectionString = "127.0.0.1",
                DatabaseType = Infrastructure.Internal.DatabaseType.SQLServer
            });
        }

        [Fact(DisplayName = "Select sample test ")]
        public void Select_Sample_Test()
        {
         

            Assert.True(false);
        }

        [Fact(DisplayName = "Select multi table without join test ")]
        public void Select_MultiTable_WithoutJoin_Test()
        {
            var query = _DapperContext.DataSet<SysUser>().Select<SysRole,SysUserRole>();
            var sqlBuilder = query.SqlBuilder;
            Assert.True(false);
        }

        [Fact(DisplayName = "Select join table not in multi table test ")]
        public void Select_JoinTale_ExcludeMultiTable_Test()
        {

        }

    }
}
