using BNPL_Web.Common.ViewModels.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;

namespace BNPL_Web.Areas.SelfPortal.Controllers
{
    [Area("SelfPortal")]
    public class RoleManagement : Controller
    {
        public IActionResult Manage()
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
            return View();
        }
    }
}
