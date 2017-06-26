using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NETCore.DapperKit.Extensions;

namespace NETCore.DapperKit.Web.Model
{
    [Table("SysUser")]
    public class SysUser
    {
        /// <summary>
        /// primary key 
        /// </summary>\
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// account
        /// </summary>
        public string LoginName { get; set; }
        /// <summary>
        /// password
        /// </summary>
        public string LoginPwd { get; set; }

        public string UserName { get; set; }

        public string UserRoleNo { get; set; }

        public bool IsAdmin { get; set; }

        public DateTime CreateTime { get; set; }

        [Write(false)]
        public string RoleName { get; set; }
    }
}
