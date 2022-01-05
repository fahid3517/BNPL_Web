using Microsoft.AspNetCore.Mvc;

namespace BNPL_Web.Areas.SelfPortal.Controllers
{
    [Area("SelfPortal")]
    public class RoleManagement : Controller
    {
        public IActionResult Manage()
        {
            return View();
        }
    }
}
