using Microsoft.AspNetCore.Mvc;

namespace BNPL_Web.Areas.SelfPortal.Controllers
{
    [Area("SelfPortal")]
    public class Payment : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
