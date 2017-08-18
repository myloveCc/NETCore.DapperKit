using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NETCore.DapperKit.Web.ViewModels
{
    public class LoginViewModel
    {
        /// <summary>
        /// 账户
        /// </summary>
        [Required(ErrorMessage = "账号不能为空")]
        [RegularExpression("^[a-zA-Z0-9]{5,16}$", ErrorMessage = "请输入5-16个字符，只能包含英文，数字。")]
        public string Account { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [Required(ErrorMessage = "密码不能为空")]
        //[RegularExpression("^([A-Z]|[a-z]|[0-9]|[~!@#$%^&*()-_+}{:]){6,20}$", ErrorMessage = "请输入6-20位字母，数字，字符的密码。")]
        public string Password { get; set; }
    }
}
