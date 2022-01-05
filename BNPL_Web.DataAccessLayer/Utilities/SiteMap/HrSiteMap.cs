using BNPL_Web.Common.ViewModels.Authorization;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Utilities.SiteMap
{
    public class HrSiteMap
    {
        //public static string SiteMap(string UserName)
        //{
        //    List<AssignPrivilegesViewModel> privileges = AuthorizationUtility.Getuserivilege(UserName).ToList();
        //    string html = string.Empty;
        //    html = html + "<ul>";
        //    html = html + MenuUtility.MenuTitle("Portal");
        //    if (AuthorizationUtility.userHasPrivilegePortal(privileges, EnumPrivilegePortal.SELF_SERVICE_PORTAL))
        //    {
        //        html = html + MenuUtility.Simple_li_List("/Self/Home/Index", "las la-universal-access", "Self Service Portal");
        //    }
        //    if (AuthorizationUtility.userHasPrivilegePortal(privileges, EnumPrivilegePortal.MASTER_PORTAL))
        //    {
        //        html = html + MenuUtility.Simple_li_List("/MasterData/Home/Index", "las la-bullseye", "Master Portal");
        //    }
        //    html = html + MenuUtility.MenuTitle("Main");
        //    /////                                         Organogram 
        //    ///
        //    if (AuthorizationUtility.userHasPrivilegeCategory(privileges, EnumPrivilegeCategory.ORGANOGRAM))
        //    {

        //        html = html + MenuUtility.Sub_Menu_li_ListStart("#", "las la-sitemap", "Company Organogram");
        //        if (AuthorizationUtility.userHasPrivilege(privileges, EnumPrivilegesName.EMPLOYEE_ORGANOGRAM))
        //        {
        //            html = html + MenuUtility.Sub_Menu_li_List_ul("/HR/OrganogramManagement/Index", "Employee Organogram");
        //        }
        //        if (AuthorizationUtility.userHasPrivilege(privileges, EnumPrivilegesName.DEPARTMENT_ORGANOGRAM))
        //        {
        //            html = html + MenuUtility.Sub_Menu_li_List_ul("/HR/DepartmentOrganogramManagement/Index", "Department Organogram");
        //        }
        //        html = html + MenuUtility.Sub_Menu_li_ListEnd();
        //    }
        //    //Role manegment
        //    if (AuthorizationUtility.userHasPrivilegeCategory(privileges, EnumPrivilegeCategory.ROLE_MANAGEMENT))
        //    {
        //        html = html + MenuUtility.Simple_li_List("/HR/RoleManagement/Manage", "las la-key", "Roles");
        //    }
        //    //Employee manegment
        //    html = html + MenuUtility.MenuTitle("Employee");
        //    if (AuthorizationUtility.userHasPrivilegeCategory(privileges, EnumPrivilegeCategory.EMPLOYEE_MANAGEMENT))
        //    {
        //        html = html + MenuUtility.Sub_Menu_li_ListStart("#", "las la-user-circle", "Profile");
        //        if (AuthorizationUtility.userHasPrivilege(privileges, EnumPrivilegesName.ADD_EMPLOYEE))
        //        {
        //            html = html + MenuUtility.Sub_Menu_li_List_ul("/HR/EmployeeManagement/EmployeeManagement", "Add Employee");
        //        }
        //        if (AuthorizationUtility.userHasPrivilege(privileges, EnumPrivilegesName.VIEW_EMPLOYEE))
        //        {
        //            html = html + MenuUtility.Sub_Menu_li_List_ul("/HR/EmployeeManagement/Manage", "View  Employee");
        //        }
        //        html = html + MenuUtility.Sub_Menu_li_ListEnd();
        //    }
        //    //Attendance manegment
        //    html = html + MenuUtility.MenuTitle("Attendance");
        //    if (AuthorizationUtility.userHasPrivilegeCategory(privileges, EnumPrivilegeCategory.ATTENDENCE_MANAGEMENT))
        //    {
        //        html = html + MenuUtility.Sub_Menu_li_ListStart("#", "las la-address-book", "Attendance");
        //        if (AuthorizationUtility.userHasPrivilege(privileges, EnumPrivilegesName.MARK_ATTENDANCE))
        //        {
        //            html = html + MenuUtility.Sub_Menu_li_List_ul("/HR/AttendanceManagement/Mark", "Mark Attendance");
        //        }
        //        if (AuthorizationUtility.userHasPrivilege(privileges, EnumPrivilegesName.VIEW_ATTENDANCE_POLICY))
        //        {
        //            html = html + MenuUtility.Sub_Menu_li_List_ul("/HR/AttendancePolicyManagement/Add", "Add Policy");
        //        }
        //        if (AuthorizationUtility.userHasPrivilege(privileges, EnumPrivilegesName.VIEW_ATTENDANCE_POLICY))
        //        {
        //            html = html + MenuUtility.Sub_Menu_li_List_ul("/HR/AttendancePolicyM  anagement/Manage", "View Policy");
        //        }
        //        if (AuthorizationUtility.userHasPrivilege(privileges, EnumPrivilegesName.ATTENDANCE_POLICY_CONFIGURATION))
        //        {
        //            html = html + MenuUtility.Sub_Menu_li_List_ul("/HR/AttendancePolicyConfigurationManagement/Index", "Attendance Configuration");
        //        }
        //        html = html + MenuUtility.Sub_Menu_li_ListEnd();
        //    }
        //    //Attendance manegment
        //    if (AuthorizationUtility.userHasPrivilege(privileges, EnumPrivilegeCategory.ATTENDANCE_SETTING))
        //    {
        //        html = html + MenuUtility.Simple_li_List("/HR/AttendanceSettingManagement/Index", "las la-cog", "Attendance Setting");
        //    }
        //    return html;
        //}
    }
}