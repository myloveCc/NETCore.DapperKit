using System;
using Xunit;
using NETCore.DapperKit.Extensions;
using NETCore.DapperKit.ExpressionToSql.Extensions;

namespace NETCore.DapperKit.Tests
{
    public class Insert_Tests
    {
        private readonly IDapperKitProvider _Provider;

        public Insert_Tests()
        {
            _Provider = new DapperKitProvider(new Infrastructure.Internal.DapperKitOptions()
            {
                ConnectionString = "127.0.0.1",
                DatabaseType = Infrastructure.Internal.DatabaseType.SQLServer
            });
        }

        [Fact]
        public void Test1()
        {
            var sqlBuilder = _Provider.DataSet<SysUser>().Insert(() => new SysUser() { Account = "admin", Password = "123456", CreateTime = DateTime.Now }).SqlBuilder;

            var sql = sqlBuilder.GetSqlString();
        }



    }

    [Table("SysUser")]
    public class SysUser
    {
        [Key]
        public int Id { get; set; }

        public string Account { get; set; }

        public string Password { get; set; }

        public DateTime? CreateTime { get; set; }
    }
}
