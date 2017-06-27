using System;
using Xunit;
using NETCore.DapperKit.Extensions;
using NETCore.DapperKit.ExpressionVisitor.Extensions;
using NETCore.DapperKit.Tests.Model;
using System.Collections.Generic;

namespace NETCore.DapperKit.Tests
{
    public class Join_Tests
    {
        private readonly IDapperContext _DapperContext;

        public Join_Tests()
        {
            _DapperContext = new DapperContext(new DapperKitOptions()
            {
                ConnectionString = "127.0.0.1",
                DatabaseType = DatabaseType.SQLServer
            });
        }

        [Fact(DisplayName = "Select multi table with join test ")]
        public void Select_MultiTable_WithJoin_Test()
        {
            var query = _DapperContext.DbSet<SysUser>()
                                      .Select<SysRole, SysUserRole>((user, role, userRole) => new { Id = user.Id, Account = user.Account, UserRoleName = role.RoleName })
                                      .InnerJoin<SysUserRole>((u, r) => u.Id == r.UserId)
                                      .InnerJoin<SysUserRole, SysRole>((ur, r) => ur.RoleId == r.Id);
            var sqlBuilder = query.SqlBuilder;

            var sql = sqlBuilder.GetSql();

            Assert.Equal("SELECT a.[Id] [Id],a.[Account] [Account],b.[RoleName] [UserRoleName] FROM [SysUser] a INNER JOIN [SysUserRole] c ON a.[Id] = c.[UserId] INNER JOIN [SysRole] b ON c.[RoleId] = b.[Id];", sql);
        }

        [Fact(DisplayName = "Select join table not in multi table test ")]
        public void Select_JoinTale_ExcludeMultiTable_Test()
        {

            var test = new Action(() =>
            {
                var query = _DapperContext.DbSet<SysUser>()
                                     .Select<SysRole, SysUserRole>((user, role, userRole) => new { Id = user.Id, Account = user.Account, UserRoleName = role.RoleName })
                                     .InnerJoin<SysUnUse>((u, r) => u.Id == r.Id);
            });

            Assert.Throws<Exception>(test);
        }

    }
}
