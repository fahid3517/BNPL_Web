using Microsoft.AspNetCore.Mvc;

namespace BNPL_Web.Areas.SelfPortal.Controllers
{
    [Area("SelfPortal")]
    public class UserManagement : Controller
    {
        public IActionResult CustomerProfile()
        {
            return View();
        }
        public IActionResult BackOfficeUserProfile()
        {
            return View();
        }
        public IActionResult SystemUser()
        {
            return View();
        }
        public IActionResult Manage()
        {
            return View();
        }
    }
}
