using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Web.Areas.SelfPortal.Controllers
{[Area("SelfPortal")]
    public class DashboardManagementController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
      
        public IActionResult EmployeeDashboardMain()
        {
            return View();
        }

        public IActionResult EmployeeAttendanceHistory()
        {
            return View();
        }
    }
}
