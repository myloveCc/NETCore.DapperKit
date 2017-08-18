using System;
using System.Collections.Generic;
using System.Text;
using NETCore.DapperKit.Extensions;

namespace NETCore.DapperKit.Tests.Model
{
    [Table("SysUserRole")]
    public class SysUserRole
    {
        [Key]
        public int Id { get; set; }

        public int UserId { get; set; }

        public int RoleId { get; set; }
    }
}
