using System;
using Xunit;
using NETCore.DapperKit.Extensions;
using NETCore.DapperKit.ExpressionToSql.Extensions;
using NETCore.DapperKit.Tests.Model;
using System.Collections.Generic;

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
            var query = _DapperContext.DataSet<SysUser>().Select();

            var sqlBuilder = query.SqlBuilder;
            var sql = sqlBuilder.GetSqlString();

            Assert.Equal("SELECT * FROM [SysUser] a;", sql);
        }

        [Fact(DisplayName = "Select one column test")]
        public void Select_Sample_OneColumn_Test()
        {
            var query = _DapperContext.DataSet<SysUser>().Select(m => m.Id);
            var sqlBuilder = query.SqlBuilder;
            var sql = sqlBuilder.GetSqlString();

            Assert.Equal("SELECT a.[Id] [Id] FROM [SysUser] a;", sql);
        }

        [Fact(DisplayName = "Select muitl column test")]
        public void Select_Sample_MultiColumns_Test()
        {
            var query = _DapperContext.DataSet<SysUser>().Select(m => new { m.Id, m.Account, m.Password });

            var sqlBuilder = query.SqlBuilder;
            var sql = sqlBuilder.GetSqlString();

            Assert.Equal("SELECT a.[Id] [Id],a.[Account] [Account],a.[Password] [Password] FROM [SysUser] a;", sql);
        }

        [Fact(DisplayName = "Select one column with one where test")]
        public void Select_OneColumn_OneWhere_Test()
        {
            var query = _DapperContext.DataSet<SysUser>().Select(m => m.Account).Where(m => m.Id == 1);

            var sqlBuilder = query.SqlBuilder;
            var sql = sqlBuilder.GetSqlString();
            var sqlParams = sqlBuilder.GetSqlParameters();

            Assert.Equal("SELECT a.[Account] [Account] FROM [SysUser] a WHERE a.[Id] = @param0;", sql);
            Assert.Equal(1, sqlParams.Count);
            Assert.Equal(1, sqlParams["@param0"]);
        }

        [Fact(DisplayName = "Select multi column with one where test")]
        public void Select_MultiColumn_OneWhere_Test()
        {
            var query = _DapperContext.DataSet<SysUser>().Select(m => new { m.Id, m.Account, m.Password }).Where(m => m.Id == 1);

            var sqlBuilder = query.SqlBuilder;
            var sql = sqlBuilder.GetSqlString();
            var sqlParams = sqlBuilder.GetSqlParameters();

            Assert.Equal("SELECT a.[Id] [Id],a.[Account] [Account],a.[Password] [Password] FROM [SysUser] a WHERE a.[Id] = @param0;", sql);
            Assert.Equal(1, sqlParams.Count);
            Assert.Equal(1, sqlParams["@param0"]);
        }

        [Fact(DisplayName = "Select one column with multi where test")]
        public void Select_OneColumn_MultiWhere_Test()
        {
            var query = _DapperContext.DataSet<SysUser>().Select(m => m.Account).Where(m => m.Id == 3 && m.IsAdmin);

            var sqlBuilder = query.SqlBuilder;
            var sql = sqlBuilder.GetSqlString();
            var sqlParams = sqlBuilder.GetSqlParameters();

            Assert.Equal("SELECT a.[Account] [Account] FROM [SysUser] a WHERE a.[Id] = @param0 AND a.[IsAdmin] = @param1;", sql);
            Assert.Equal(2, sqlParams.Count);
            Assert.Equal(3, sqlParams["@param0"]);
            Assert.Equal(1, sqlParams["@param1"]);
        }

        [Fact(DisplayName = "Select multi column with multi where test")]
        public void Select_MultiColumn_MultiWhere_Test()
        {
            var query = _DapperContext.DataSet<SysUser>().Select(m => new { m.Id, m.Account, m.Password }).Where(m => m.Id == 3 && m.IsAdmin);

            var sqlBuilder = query.SqlBuilder;
            var sql = sqlBuilder.GetSqlString();
            var sqlParams = sqlBuilder.GetSqlParameters();

            Assert.Equal("SELECT a.[Id] [Id],a.[Account] [Account],a.[Password] [Password] FROM [SysUser] a WHERE a.[Id] = @param0 AND a.[IsAdmin] = @param1;", sql);
            Assert.Equal(2, sqlParams.Count);
            Assert.Equal(3, sqlParams["@param0"]);
            Assert.Equal(1, sqlParams["@param1"]);
        }

        [Fact(DisplayName = "Select !m.IsAdmin Test")]
        public void Select_IsNotAmdin_Test()
        {
            var query = _DapperContext.DataSet<SysUser>().Select().Where(m => !m.IsAdmin);

            var sqlBuilder = query.SqlBuilder;
            var sql = sqlBuilder.GetSqlString();
            var sqlParams = sqlBuilder.GetSqlParameters();

            Assert.Equal("SELECT * FROM [SysUser] a WHERE a.[IsAdmin] = @param0;", sql);
            Assert.Equal(1, sqlParams.Count);
            Assert.Equal(0, sqlParams["@param0"]);
        }

        [Fact(DisplayName = "Select one column !m.IsAdmin Test")]
        public void Select_OneColumn_IsNotAmdin_Test()
        {
            var query = _DapperContext.DataSet<SysUser>().Select(m => m.Account).Where(m => !m.IsAdmin);

            var sqlBuilder = query.SqlBuilder;
            var sql = sqlBuilder.GetSqlString();
            var sqlParams = sqlBuilder.GetSqlParameters();

            Assert.Equal("SELECT a.[Account] [Account] FROM [SysUser] a WHERE a.[IsAdmin] = @param0;", sql);
            Assert.Equal(1, sqlParams.Count);
            Assert.Equal(0, sqlParams["@param0"]);
        }

        [Fact(DisplayName = "Select multi column !m.IsAdmin Test")]
        public void Select_MnultiColumn_IsNotAmdin_Test()
        {
            var query = _DapperContext.DataSet<SysUser>().Select(m => new { UserId = m.Id, LoginName = m.Account, LoginPwd = m.Password }).Where(m => !m.IsAdmin);

            var sqlBuilder = query.SqlBuilder;
            var sql = sqlBuilder.GetSqlString();
            var sqlParams = sqlBuilder.GetSqlParameters();

            Assert.Equal("SELECT a.[Id] [UserId],a.[Account] [LoginName],a.[Password] [LoginPwd] FROM [SysUser] a WHERE a.[IsAdmin] = @param0;", sql);
            Assert.Equal(1, sqlParams.Count);
            Assert.Equal(0, sqlParams["@param0"]);
        }

        [Fact(DisplayName = "Select model test")]
        public void Select_Model_Test()
        {
            var query = _DapperContext.DataSet<SysUser>().Select(m => new SysUser { Id = m.Id, Account = m.Account, Password = m.Password });

            var sqlBuilder = query.SqlBuilder;
            var sql = sqlBuilder.GetSqlString();
            var sqlParams = sqlBuilder.GetSqlParameters();

            Assert.Equal("SELECT a.[Id] [Id],a.[Account] [Account],a.[Password] [Password] FROM [SysUser] a;", sql);
        }

        [Fact(DisplayName = "Select dto test")]
        public void Select_DTO_Test()
        {
            var query = _DapperContext.DataSet<SysUser>().Select(m => new UserDTO { UserId = m.Id, LoginName = m.Account, LoginPwd = m.Password });

            var sqlBuilder = query.SqlBuilder;
            var sql = sqlBuilder.GetSqlString();
            var sqlParams = sqlBuilder.GetSqlParameters();

            Assert.Equal("SELECT a.[Id] [UserId],a.[Account] [LoginName],a.[Password] [LoginPwd] FROM [SysUser] a;", sql);
        }

        [Fact(DisplayName = "Select model one where test")]
        public void Select_Model_OneWhere_Test()
        {
            var query = _DapperContext.DataSet<SysUser>().Select(m => new SysUser { Id = m.Id, Account = m.Account, Password = m.Password }).Where(m => m.Id == 3);

            var sqlBuilder = query.SqlBuilder;
            var sql = sqlBuilder.GetSqlString();
            var sqlParams = sqlBuilder.GetSqlParameters();

            Assert.Equal("SELECT a.[Id] [Id],a.[Account] [Account],a.[Password] [Password] FROM [SysUser] a WHERE a.[Id] = @param0;", sql);
            Assert.Equal(1, sqlParams.Count);
            Assert.Equal(3, sqlParams["@param0"]);
        }

        [Fact(DisplayName = "Select dto multi where test")]
        public void Select_DTO_MultiWhere_Test()
        {
            var query = _DapperContext.DataSet<SysUser>().Select(m => new UserDTO { UserId = m.Id, LoginName = m.Account, LoginPwd = m.Password }).Where(m => m.Id == 3 && !m.IsAdmin);

            var sqlBuilder = query.SqlBuilder;
            var sql = sqlBuilder.GetSqlString();
            var sqlParams = sqlBuilder.GetSqlParameters();

            Assert.Equal("SELECT a.[Id] [UserId],a.[Account] [LoginName],a.[Password] [LoginPwd] FROM [SysUser] a WHERE a.[Id] = @param0 AND a.[IsAdmin] = @param1;", sql);
            Assert.Equal(2, sqlParams.Count);
            Assert.Equal(0, sqlParams["@param1"]);
        }

        [Fact(DisplayName = "Select multi table with join test ")]
        public void Select_MultiTable_WithJoin_Test()
        {
            var query = _DapperContext.DataSet<SysUser>()
                                      .Select<SysRole, SysUserRole>((user, role, userRole) => new { Id = user.Id, Account = user.Account, UserRoleName = role.RoleName })
                                      .InnerJoin<SysUserRole>((u, r) => u.Id == r.UserId)
                                      .InnerJoin<SysUserRole, SysRole>((ur, r) => ur.RoleId == r.Id);
            var sqlBuilder = query.SqlBuilder;

            var sql = sqlBuilder.GetSqlString();

            Assert.Equal("SELECT a.[Id] [Id],a.[Account] [Account],b.[RoleName] [UserRoleName] FROM [SysUser] a INNER JOIN [SysUserRole] c ON a.[Id] = c.[UserId] INNER JOIN [SysRole] b ON c.[RoleId] = b.[Id];", sql);
        }

        [Fact(DisplayName = "Select join table not in multi table test ")]
        public void Select_JoinTale_ExcludeMultiTable_Test()
        {

            var test = new Action(() =>
            {
                var query = _DapperContext.DataSet<SysUser>()
                                     .Select<SysRole, SysUserRole>((user, role, userRole) => new { Id = user.Id, Account = user.Account, UserRoleName = role.RoleName })
                                     .InnerJoin<SysUnUse>((u, r) => u.Id == r.Id);
            });

            Assert.Throws<Exception>(test);
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

        [Fact(DisplayName = "Select order by test")]
        public void Select_Orderby_Test()
        {
            var query = _DapperContext.DataSet<SysUser>().Select().OrderBy(m => m.CreateTime);
            var sqlBuilder = query.SqlBuilder;

            var sql = sqlBuilder.GetSqlString();
            var sqlParams = sqlBuilder.GetSqlParameters();

            Assert.Equal("SELECT * FROM [SysUser] a ORDER BY a.[CreateTime] ASC;", sql);
            Assert.True(false);
        }

        [Fact(DisplayName = "Select order by one test")]
        public void Select_Orderby_One_Test()
        {
            Assert.True(false);
        }


        [Fact(DisplayName = "Select order by mutli test")]
        public void Select_Orderby_Multi_Test()
        {
            Assert.True(false);
        }

        [Fact(DisplayName = "Select order by and then test")]
        public void Select_Orderby_Then_Test()
        {
            Assert.True(false);
        }


        [Fact(DisplayName = "Select order by and then desc test")]
        public void Select_Orderby_ThenDesc_Test()
        {
            Assert.True(false);
        }

        [Fact(DisplayName = "Select order by desc then asc test")]
        public void Select_OrderbyDesc_Then_Test()
        {
            Assert.True(false);
        }


        [Fact(DisplayName = "Select order by desc then desc test")]
        public void Select_OrderbyDesc_ThenDesc_Test()
        {
            Assert.True(false);
        }


        [Fact(DisplayName = "Select group by test")]
        public void Select_Groupby_Test()
        {
            Assert.True(false);
        }

        [Fact(DisplayName = "Select group by and where test")]
        public void Select_Groupby_Where_Test()
        {
            var query = _DapperContext.DataSet<SysUser>().Select(m => new { IdCount = m.Id.Avg() });

            Assert.True(false);
        }
    }
}
