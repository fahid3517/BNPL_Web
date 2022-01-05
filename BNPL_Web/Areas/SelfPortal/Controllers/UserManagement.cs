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
        public IActionResult Registor()
        {
            return View();
        }
    }
}
