using System;
using System.Collections.Generic;
using System.Text;
using NETCore.DapperKit.Extensions;

namespace NETCore.DapperKit.Tests.Model
{
    [Table("SysRole")]
    public class SysRole
    {
        [Key]
        public int Id { get; set; }

        public string RoleName { get; set; }

        public DateTime CreateTime { get; set; }
    }
}
