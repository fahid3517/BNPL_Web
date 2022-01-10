using BNPL_Web.Common.Enums;
using Microsoft.AspNetCore.Mvc;
using Project.Utilities;
using Project.Web.CustomeAnnotations;

namespace BNPL_Web.Areas.SelfPortal.Controllers
{
    [Area("SelfPortal")]
    public class UserManagement : Controller
    {

        public IActionResult CustomerProfile()
        {
            return View();
        }
        //[MvcCustomAuthorizeAttribute(privilege = EnumPrivilegesName.ADD_BACK_USER_PROFILE)]
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
