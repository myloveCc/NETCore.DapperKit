using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NETCore.DapperKit.Extensions;

namespace NETCore.DapperKit.Web.Model
{
    [Table("SysUserRole")]
    public class SysUserRole
    {
        [Key]
        public string No { get; set; }

        public string Name { get; set; }
    }
}
