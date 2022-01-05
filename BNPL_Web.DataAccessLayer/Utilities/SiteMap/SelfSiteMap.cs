
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BNPL_Web.Common.ViewModels.Authorization;

namespace Project.Utilities.SiteMap
{
    public static class SelfSiteMap
    {

        //public static string SiteMap(string UserName)
        //{
        //    List<AssignPrivilegesViewModel> privileges = AuthorizationUtility.Getuserivilege(UserName).ToList();
        //    string html = string.Empty;
        //    html = html + "<ul>";
        //    html = html + MenuUtility.MenuTitle("Portal");
        //    if (AuthorizationUtility.userHasPrivilegePortal(privileges, EnumPrivilegePortal.HR_PORTAL))
        //    {
        //        html = html + MenuUtility.Simple_li_List("/HR/Home/Index", "las la-users", "HR Portal");
        //    }
        //    html = html + MenuUtility.MenuTitle("Self Services");


        //    if (AuthorizationUtility.userHasPrivilege(privileges, EnumPrivilegesName.DASHBOARD))
        //    {
        //        html = html + MenuUtility.Simple_li_List("/Self/DashboardManagement/Index", "las la-dashboard", "Dashboard");
        //    }


        //    return html;
        //}
    }
}
