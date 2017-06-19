using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NETCore.DapperKit.Core;
using NETCore.DapperKit.Web.Model;

namespace NETCore.DapperKit.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDapperRepository _DapperRepository;
        public HomeController(IDapperRepository dapperRepository)
        {
            _DapperRepository = dapperRepository;
        }

        public IActionResult Index()
        {
            var model = new DapperKitModel()
            {
                Account = "dswq42",
                Password = "123456"
            };

            var result = _DapperRepository.Insert(model);

            return View();
        }

        public async Task<IActionResult> About()
        {
            ViewData["Message"] = "Your application description page.";

            var result = await _DapperRepository.GetAllAsync<DapperKitModel>();

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
