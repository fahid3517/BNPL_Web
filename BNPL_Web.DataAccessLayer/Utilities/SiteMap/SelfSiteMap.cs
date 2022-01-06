
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BNPL_Web.Common.Enums;
using BNPL_Web.Common.ViewModels.Authorization;

namespace Project.Utilities.SiteMap
{
    public static class SelfSiteMap
    {

        public static string SiteMap(string UserName)
        {
            List<AssignPrivilegesViewModel> privileges = AuthorizationUtility.Getuserivilege(UserName).ToList();
            string html = string.Empty;
            html = html + "<ul>";
            html = html + MenuUtility.MenuTitle("Main");

            //User manegment
            if (AuthorizationUtility.userHasPrivilegeCategory(privileges, EnumPrivilegeCategory.USER_MANAGEMENT))
            {
                html = html + MenuUtility.Sub_Menu_li_ListStart("#", "las la-user-circle", "Profile");
                if (AuthorizationUtility.userHasPrivilege(privileges, EnumPrivilegesName.ADD_USER))
                {
                    html = html + MenuUtility.Sub_Menu_li_List_ul("/HR/EmployeeManagement/EmployeeManagement", "Add Employee");
                }

                html = html + MenuUtility.Sub_Menu_li_ListEnd();
            }
            return html;
        }
    }
}
