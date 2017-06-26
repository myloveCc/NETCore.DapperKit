using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NETCore.DapperKit.Extensions;
using NETCore.DapperKit.Web.ViewModels;
using NETCore.DapperKit.Web.Model;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NETCore.DapperKit.Web.Controllers
{
    public class AccountController : Controller
    {

        private readonly IDapperKitProvider _DapperKit;

        public AccountController(IDapperKitProvider dapperKitProvider)
        {
            _DapperKit = dapperKitProvider;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }


        /// <summary>
        /// 登录方法
        /// </summary>
        /// <param name="model">UserViewModel实体信息</param>
        /// <param name="returnUrl">返回Url地址</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (!ModelState.IsValid) return View(model);

            try
            {
                var users = await _DapperKit.DataSet<SysUser>()
                                        .Select<SysUserRole>((u, r) => new SysUser() { Id = u.Id, IsAdmin = u.IsAdmin, LoginName = u.LoginName, LoginPwd = u.LoginPwd, CreateTime = u.CreateTime, UserName = u.UserName, UserRoleNo = u.UserRoleNo, RoleName = r.Name })
                                        .Join<SysUserRole>((u, r) => u.UserRoleNo == r.No)
                                        .Where(m => m.LoginName == model.Account && m.LoginPwd == model.Password)
                                        .ToListAsync<SysUser>();


                if (users != null && users.First().LoginName == model.Account)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception ex)
            {

            }

            return RedirectToAction("Index");
        }

    }
}
