using Microsoft.AspNetCore.Mvc;

namespace BNPL_Web.Areas.SelfPortal.Controllers
{
    [Area("SelfPortal")]
    public class PaymentGateway : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
