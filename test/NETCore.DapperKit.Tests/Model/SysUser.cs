using System;
using System.Collections.Generic;
using System.Text;
using NETCore.DapperKit.Extensions;

namespace NETCore.DapperKit.Tests.Model
{

    [Table("SysUser")]
    public class SysUser
    {
        [Key]
        public int Id { get; set; }

        public string Account { get; set; }

        public string Password { get; set; }

        public bool IsAdmin { get; set; }

        public DateTime? CreateTime { get; set; }

        [Write(false)]
        public string UserRoleName { get; set; }
    }
}
