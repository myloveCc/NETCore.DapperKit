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
            var sql = sqlBuilder.GetSql();

            Assert.Equal("SELECT * FROM [SysUser] a;", sql);
        }

        [Fact(DisplayName = "Select one column test")]
        public void Select_Sample_OneColumn_Test()
        {
            var query = _DapperContext.DataSet<SysUser>().Select(m => m.Id);
            var sqlBuilder = query.SqlBuilder;
            var sql = sqlBuilder.GetSql();

            Assert.Equal("SELECT a.[Id] [Id] FROM [SysUser] a;", sql);
        }

        [Fact(DisplayName = "Select muitl column test")]
        public void Select_Sample_MultiColumns_Test()
        {
            var query = _DapperContext.DataSet<SysUser>().Select(m => new { m.Id, m.Account, m.Password });

            var sqlBuilder = query.SqlBuilder;
            var sql = sqlBuilder.GetSql();

            Assert.Equal("SELECT a.[Id] [Id],a.[Account] [Account],a.[Password] [Password] FROM [SysUser] a;", sql);
        }

        [Fact(DisplayName = "Select one column with one where test")]
        public void Select_OneColumn_OneWhere_Test()
        {
            var query = _DapperContext.DataSet<SysUser>().Select(m => m.Account).Where(m => m.Id == 1);

            var sqlBuilder = query.SqlBuilder;
            var sql = sqlBuilder.GetSql();
            var sqlParams = sqlBuilder.GetSqlParams();

            Assert.Equal("SELECT a.[Account] [Account] FROM [SysUser] a WHERE a.[Id] = @param0;", sql);
            Assert.Equal(1, sqlParams.Count);
            Assert.Equal(1, sqlParams["@param0"]);
        }

        [Fact(DisplayName = "Select multi column with one where test")]
        public void Select_MultiColumn_OneWhere_Test()
        {
            var query = _DapperContext.DataSet<SysUser>().Select(m => new { m.Id, m.Account, m.Password }).Where(m => m.Id == 1);

            var sqlBuilder = query.SqlBuilder;
            var sql = sqlBuilder.GetSql();
            var sqlParams = sqlBuilder.GetSqlParams();

            Assert.Equal("SELECT a.[Id] [Id],a.[Account] [Account],a.[Password] [Password] FROM [SysUser] a WHERE a.[Id] = @param0;", sql);
            Assert.Equal(1, sqlParams.Count);
            Assert.Equal(1, sqlParams["@param0"]);
        }

        [Fact(DisplayName = "Select one column with multi where test")]
        public void Select_OneColumn_MultiWhere_Test()
        {
            var query = _DapperContext.DataSet<SysUser>().Select(m => m.Account).Where(m => m.Id == 3 && m.IsAdmin);

            var sqlBuilder = query.SqlBuilder;
            var sql = sqlBuilder.GetSql();
            var sqlParams = sqlBuilder.GetSqlParams();

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
            var sql = sqlBuilder.GetSql();
            var sqlParams = sqlBuilder.GetSqlParams();

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
            var sql = sqlBuilder.GetSql();
            var sqlParams = sqlBuilder.GetSqlParams();

            Assert.Equal("SELECT * FROM [SysUser] a WHERE a.[IsAdmin] = @param0;", sql);
            Assert.Equal(1, sqlParams.Count);
            Assert.Equal(0, sqlParams["@param0"]);
        }

        [Fact(DisplayName = "Select one column !m.IsAdmin Test")]
        public void Select_OneColumn_IsNotAmdin_Test()
        {
            var query = _DapperContext.DataSet<SysUser>().Select(m => m.Account).Where(m => !m.IsAdmin);

            var sqlBuilder = query.SqlBuilder;
            var sql = sqlBuilder.GetSql();
            var sqlParams = sqlBuilder.GetSqlParams();

            Assert.Equal("SELECT a.[Account] [Account] FROM [SysUser] a WHERE a.[IsAdmin] = @param0;", sql);
            Assert.Equal(1, sqlParams.Count);
            Assert.Equal(0, sqlParams["@param0"]);
        }

        [Fact(DisplayName = "Select multi column !m.IsAdmin Test")]
        public void Select_MnultiColumn_IsNotAmdin_Test()
        {
            var query = _DapperContext.DataSet<SysUser>().Select(m => new { UserId = m.Id, LoginName = m.Account, LoginPwd = m.Password }).Where(m => !m.IsAdmin);

            var sqlBuilder = query.SqlBuilder;
            var sql = sqlBuilder.GetSql();
            var sqlParams = sqlBuilder.GetSqlParams();

            Assert.Equal("SELECT a.[Id] [UserId],a.[Account] [LoginName],a.[Password] [LoginPwd] FROM [SysUser] a WHERE a.[IsAdmin] = @param0;", sql);
            Assert.Equal(1, sqlParams.Count);
            Assert.Equal(0, sqlParams["@param0"]);
        }

        [Fact(DisplayName = "Select model test")]
        public void Select_Model_Test()
        {
            var query = _DapperContext.DataSet<SysUser>().Select(m => new SysUser { Id = m.Id, Account = m.Account, Password = m.Password });

            var sqlBuilder = query.SqlBuilder;
            var sql = sqlBuilder.GetSql();
            var sqlParams = sqlBuilder.GetSqlParams();

            Assert.Equal("SELECT a.[Id] [Id],a.[Account] [Account],a.[Password] [Password] FROM [SysUser] a;", sql);
        }

        [Fact(DisplayName = "Select dto test")]
        public void Select_DTO_Test()
        {
            var query = _DapperContext.DataSet<SysUser>().Select(m => new UserDTO { UserId = m.Id, LoginName = m.Account, LoginPwd = m.Password });

            var sqlBuilder = query.SqlBuilder;
            var sql = sqlBuilder.GetSql();
            var sqlParams = sqlBuilder.GetSqlParams();

            Assert.Equal("SELECT a.[Id] [UserId],a.[Account] [LoginName],a.[Password] [LoginPwd] FROM [SysUser] a;", sql);
        }

        [Fact(DisplayName = "Select model one where test")]
        public void Select_Model_OneWhere_Test()
        {
            var query = _DapperContext.DataSet<SysUser>().Select(m => new SysUser { Id = m.Id, Account = m.Account, Password = m.Password }).Where(m => m.Id == 3);

            var sqlBuilder = query.SqlBuilder;
            var sql = sqlBuilder.GetSql();
            var sqlParams = sqlBuilder.GetSqlParams();

            Assert.Equal("SELECT a.[Id] [Id],a.[Account] [Account],a.[Password] [Password] FROM [SysUser] a WHERE a.[Id] = @param0;", sql);
            Assert.Equal(1, sqlParams.Count);
            Assert.Equal(3, sqlParams["@param0"]);
        }

        [Fact(DisplayName = "Select dto multi where test")]
        public void Select_DTO_MultiWhere_Test()
        {
            var query = _DapperContext.DataSet<SysUser>().Select(m => new UserDTO { UserId = m.Id, LoginName = m.Account, LoginPwd = m.Password }).Where(m => m.Id == 3 && !m.IsAdmin);

            var sqlBuilder = query.SqlBuilder;
            var sql = sqlBuilder.GetSql();
            var sqlParams = sqlBuilder.GetSqlParams();

            Assert.Equal("SELECT a.[Id] [UserId],a.[Account] [LoginName],a.[Password] [LoginPwd] FROM [SysUser] a WHERE a.[Id] = @param0 AND a.[IsAdmin] = @param1;", sql);
            Assert.Equal(2, sqlParams.Count);
            Assert.Equal(0, sqlParams["@param1"]);
        }

        [Fact(DisplayName = "Select where by object propety test")]
        public void Select_PraraObj_Test()
        {

            var paraObj = new SysUser() { Account = "admin", IsAdmin = true };

            var query = _DapperContext.DataSet<SysUser>().Select().Where(m => m.Account == paraObj.Account && m.IsAdmin == paraObj.IsAdmin);

            var sqlBuilder = query.SqlBuilder;
            var sql = sqlBuilder.GetSql();
            var sqlParams = sqlBuilder.GetSqlParams();

            Assert.Equal("SELECT * FROM [SysUser] a WHERE a.[Account] = @param0 AND a.[IsAdmin] = @param1;", sql);
            Assert.Equal(2, sqlParams.Count);
            Assert.Equal(1, sqlParams["@param1"]);
        }


        [Fact(DisplayName = "Select page data test")]
        public void Select_Page_Test()
        {
            var query = _DapperContext.DataSet<SysUser>().Select(m => new UserDTO { UserId = m.Id, LoginName = m.Account, LoginPwd = m.Password }).Where(m => m.Id == 3 && !m.IsAdmin).OrderBy(m => m.CreateTime).Skip(1).Take(20);

            var sqlBuilder = query.SqlBuilder;
            var sql = sqlBuilder.GetSql();
            var sqlParams = sqlBuilder.GetSqlParams();

            Assert.Equal("SELECT [UserId],[LoginName],[LoginPwd] FROM ( SELECT a.[Id] [UserId],a.[Account] [LoginName],a.[Password] [LoginPwd],ROW_NUMBER() OVER ( ORDER BY a.[CreateTime] ASC ) AS [RowNumber] FROM [SysUser] a WHERE a.[Id] = @param0 AND a.[IsAdmin] = @param1 ) DapperKit_Temp_PageTable WHERE DapperKit_Temp_PageTable.RowNumber>1 AND DapperKit_Temp_PageTable.RowNumber<=21;", sql);
            Assert.Equal(2, sqlParams.Count);
            Assert.Equal(0, sqlParams["@param1"]);
        }




    }
}
