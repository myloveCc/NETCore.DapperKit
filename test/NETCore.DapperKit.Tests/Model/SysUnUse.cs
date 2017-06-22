using NETCore.DapperKit.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace NETCore.DapperKit.Tests.Model
{
    [Table("SysUnUse")]
    public class SysUnUse
    {
        [Key]
        public int Id { get; set; }
    }
}
