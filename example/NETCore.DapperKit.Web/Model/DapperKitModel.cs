using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NETCore.DapperKit.Extensions;

namespace NETCore.DapperKit.Web.Model
{
    [Table("DapperKitModel")]
    public class DapperKitModel
    {
        /// <summary>
        /// primary key 
        /// </summary>\
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// account
        /// </summary>
        public string Account { get; set; }
        /// <summary>
        /// password
        /// </summary>
        public string Password { get; set; }
    }
}
