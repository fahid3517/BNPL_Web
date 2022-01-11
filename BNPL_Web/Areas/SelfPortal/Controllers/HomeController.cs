using BNPL_Web.Common.ViewModels.Authorization;
using BNPL_Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace BNPL_Web.Areas.SelfPortal.Controllers
{
    [Area("SelfPortal")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        public IActionResult Index()
        {
            string TokenCookie = Request.Cookies["Key"];

            if (TokenCookie!=null)
            {
                var handeler = new JwtSecurityTokenHandler();
                var temp1 = handeler.ReadJwtToken(TokenCookie);
                var tokenData = JsonConvert.DeserializeObject<AdminAuthToken>(temp1.Claims.FirstOrDefault(x => x.Type.Equals("token"))?.Value);


                ViewBag.UserName = tokenData.UserName;
                return View();
            }
            return RedirectToAction("Login", "Account");

        }
        public IActionResult Customer()
        {
            string TokenCookie = Request.Cookies["Key"];

            if (TokenCookie != null)
            {
                var handeler = new JwtSecurityTokenHandler();
                var temp1 = handeler.ReadJwtToken(TokenCookie);
                var tokenData = JsonConvert.DeserializeObject<AdminAuthToken>(temp1.Claims.FirstOrDefault(x => x.Type.Equals("token"))?.Value);


                ViewBag.UserName = tokenData.UserName;
                return View();
            }
            return RedirectToAction("Login", "Account");

        }
        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Profile()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
