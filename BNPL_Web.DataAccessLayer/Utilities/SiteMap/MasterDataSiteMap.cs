
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Utilities.SiteMap
{
    public class MasterDataSiteMap
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
        //    html = html + MenuUtility.Simple_li_List("/HR/Home/Index", "las la-home", "HR Portal");

        //    html = html + MenuUtility.MenuTitle("Master Data");
        //    //Country Management
        //    if (AuthorizationUtility.userHasPrivilegeCategory(privileges, EnumPrivilegeCategory.COUNTRY_MANAGEMENT))
        //    {
        //        html = html + MenuUtility.Sub_Menu_li_ListStart("#", "la la-globe", "Country");
        //        if (AuthorizationUtility.userHasPrivilege(privileges, EnumPrivilegesName.ADD_COUNTRY))
        //        {
        //            html = html + MenuUtility.Sub_Menu_li_List_ul("/MasterData/CountryManagement/Add", "Add Country");
        //        }
        //        if (AuthorizationUtility.userHasPrivilege(privileges, EnumPrivilegesName.VIEW_COUNTRY))
        //        {
        //            html = html + MenuUtility.Sub_Menu_li_List_ul("/MasterData/CountryManagement/Manage", "View Country");

        //        }
        //        html = html + MenuUtility.Sub_Menu_li_ListEnd();
        //    }
        //    //End
        //    //City Management
        //    if (AuthorizationUtility.userHasPrivilegeCategory(privileges, EnumPrivilegeCategory.CITY_MANAGEMENT))
        //    {
        //        html = html + MenuUtility.Sub_Menu_li_ListStart("#", "la la-city", "City");
        //        if (AuthorizationUtility.userHasPrivilege(privileges, EnumPrivilegesName.ADD_CITY))
        //        {
        //            html = html + MenuUtility.Sub_Menu_li_List_ul("/MasterData/CityManagement/Add", "Add City");
        //        }
        //        if (AuthorizationUtility.userHasPrivilege(privileges, EnumPrivilegesName.VIEW_CITY))
        //        {
        //            html = html + MenuUtility.Sub_Menu_li_List_ul("/MasterData/CityManagement/Manage", "View City");
        //        }
        //        html = html + MenuUtility.Sub_Menu_li_ListEnd();
        //    }
        //    //End
        //    //Location Management
        //    if (AuthorizationUtility.userHasPrivilegeCategory(privileges, EnumPrivilegeCategory.LOCATION_MANAGEMENT))
        //    {
        //        html = html + MenuUtility.Sub_Menu_li_ListStart("#", "la la-map-marker", "Location");
        //        if (AuthorizationUtility.userHasPrivilege(privileges, EnumPrivilegesName.ADD_LOCATION))
        //        {
        //            html = html + MenuUtility.Sub_Menu_li_List_ul("/MasterData/LocationManagement/Add", "Add Location");
        //        }
        //        if (AuthorizationUtility.userHasPrivilege(privileges, EnumPrivilegesName.VIEW_LOCATION))
        //        {
        //            html = html + MenuUtility.Sub_Menu_li_List_ul("/MasterData/LocationManagement/Manage", "View Location");
        //        }
        //        html = html + MenuUtility.Sub_Menu_li_ListEnd();
        //    }
        //    //End
        //    //BusinessUnit Management
        //    if (AuthorizationUtility.userHasPrivilegeCategory(privileges, EnumPrivilegeCategory.BUSINESS_UNIT_MANAGEMENT))
        //    {
        //        html = html + MenuUtility.Sub_Menu_li_ListStart("#", "la la-hands-helping", "Business Unit");

        //        if (AuthorizationUtility.userHasPrivilege(privileges, EnumPrivilegesName.ADD_BUSINESS_UNIT))
        //        {
        //            html = html + MenuUtility.Sub_Menu_li_List_ul("/MasterData/BusinessUnitManagement/Add", "Add Business Unit");
        //        }
        //        if (AuthorizationUtility.userHasPrivilege(privileges, EnumPrivilegesName.VIEW_BUSINESS_UNIT))
        //        {
        //            html = html + MenuUtility.Sub_Menu_li_List_ul("/MasterData/BusinessUnitManagement/Manage", "View Business Unit");
        //        }
        //        html = html + MenuUtility.Sub_Menu_li_ListEnd();
        //    }
        //    //End

        //    //Division Management
        //    if (AuthorizationUtility.userHasPrivilegeCategory(privileges, EnumPrivilegeCategory.DIVISION_MANAGEMENT))
        //    {
        //        html = html + MenuUtility.Sub_Menu_li_ListStart("#", "la la-hotel", "Division");
        //        if (AuthorizationUtility.userHasPrivilege(privileges, EnumPrivilegesName.ADD_DIVISION))
        //        {
        //            html = html + MenuUtility.Sub_Menu_li_List_ul("/MasterData/DivisionManagement/Add", "Add Division");
        //        }
        //        if (AuthorizationUtility.userHasPrivilege(privileges, EnumPrivilegesName.VIEW_DIVISION))
        //        {
        //            html = html + MenuUtility.Sub_Menu_li_List_ul("/MasterData/DivisionManagement/Manage", "View Division");
        //        }
        //        html = html + MenuUtility.Sub_Menu_li_ListEnd();
        //    }
        //    //End

        //    //Department Management
        //    if (AuthorizationUtility.userHasPrivilegeCategory(privileges, EnumPrivilegeCategory.DEPARTMENT_MANAGEMENT))
        //    {
        //        html = html + MenuUtility.Sub_Menu_li_ListStart("#", "la la-sitemap", "Department");
        //        if (AuthorizationUtility.userHasPrivilege(privileges, EnumPrivilegesName.ADD_DEPARTMENT))
        //        {
        //            html = html + MenuUtility.Sub_Menu_li_List_ul("/MasterData/DepartmentManagement/Add", "Add Department");
        //        }
        //        if (AuthorizationUtility.userHasPrivilege(privileges, EnumPrivilegesName.VIEW_DEPARTMENT))
        //        {
        //            html = html + MenuUtility.Sub_Menu_li_List_ul("/MasterData/DepartmentManagement/Manage", "View Department");
        //        }
        //        html = html + MenuUtility.Sub_Menu_li_ListEnd();
        //    }
        //    //End
        //    //Sub Department Management
        //    if (AuthorizationUtility.userHasPrivilegeCategory(privileges, EnumPrivilegeCategory.SUB_DEPARTMENT_MANAGEMENT))
        //    {
        //        html = html + MenuUtility.Sub_Menu_li_ListStart("#", "la la-network-wired", "Sub Department");
        //        if (AuthorizationUtility.userHasPrivilege(privileges, EnumPrivilegesName.ADD_SUB_DEPARTMENT))
        //        {
        //            html = html + MenuUtility.Sub_Menu_li_List_ul("/MasterData/SubDepartmentManagement/Add", "Add Sub Department");
        //        }
        //        if (AuthorizationUtility.userHasPrivilege(privileges, EnumPrivilegesName.VIEW_SUB_DEPARTMENT))
        //        {
        //            html = html + MenuUtility.Sub_Menu_li_List_ul("/MasterData/SubDepartmentManagement/Manage", "View Sub Department");
        //        }
        //        html = html + MenuUtility.Sub_Menu_li_ListEnd();
        //    }
        //    //End
        //    //Section Management
        //    if (AuthorizationUtility.userHasPrivilegeCategory(privileges, EnumPrivilegeCategory.SECTION_MANAGEMENT))
        //    {
        //        html = html + MenuUtility.Sub_Menu_li_ListStart("#", "la la-bars", "Section");
        //        if (AuthorizationUtility.userHasPrivilege(privileges, EnumPrivilegesName.ADD_SECTION))
        //        {
        //            html = html + MenuUtility.Sub_Menu_li_List_ul("/MasterData/SectionManagement/Add", "Add Section");
        //        }
        //        if (AuthorizationUtility.userHasPrivilege(privileges, EnumPrivilegesName.VIEW_SECTION))
        //        {
        //            html = html + MenuUtility.Sub_Menu_li_List_ul("/MasterData/SectionManagement/Manage", "View Section");
        //        }
        //        html = html + MenuUtility.Sub_Menu_li_ListEnd();
        //    }
        //    //End
        //    //Unit Management
        //    if (AuthorizationUtility.userHasPrivilegeCategory(privileges, EnumPrivilegeCategory.Unit_MANAGEMENT))
        //    {
        //        html = html + MenuUtility.Sub_Menu_li_ListStart("#", "la la-unlink", "Unit");
            
        //    if (AuthorizationUtility.userHasPrivilege(privileges, EnumPrivilegesName.ADD_Unit))
        //    {
        //        html = html + MenuUtility.Sub_Menu_li_List_ul("/MasterData/UnitManagement/Add", "Add Unit");
        //    }
        //    if (AuthorizationUtility.userHasPrivilege(privileges, EnumPrivilegesName.VIEW_Unit))
        //    {
        //        html = html + MenuUtility.Sub_Menu_li_List_ul("/MasterData/UnitManagement/Manage", "View Unit");
        //    }
        //    html = html + MenuUtility.Sub_Menu_li_ListEnd();
        //}
        //    //End

        //    //Designation Management
        //    if (AuthorizationUtility.userHasPrivilegeCategory(privileges, EnumPrivilegeCategory.DESIGNATION_MANAGEMENT))
        //    {
        //        html = html + MenuUtility.Sub_Menu_li_ListStart("#", "la la-user-tie", "Designation");
        //        if (AuthorizationUtility.userHasPrivilege(privileges, EnumPrivilegesName.ADD_DESIGNATION))
        //        {
        //            html = html + MenuUtility.Sub_Menu_li_List_ul("/MasterData/DesignationManagement/Add", "Add Designation");
        //        }
        //        if (AuthorizationUtility.userHasPrivilege(privileges, EnumPrivilegesName.VIEW_DESIGNATION))
        //        {
        //            html = html + MenuUtility.Sub_Menu_li_List_ul("/MasterData/DesignationManagement/Manage", "View Designation");
        //        }
        //        html = html + MenuUtility.Sub_Menu_li_ListEnd();
        //    }
        //    //End
        //    //Education Type Management
        //    if (AuthorizationUtility.userHasPrivilegeCategory(privileges, EnumPrivilegeCategory.EDUCATION_TYPE_MANAGEMENT))
        //    {
        //        html = html + MenuUtility.Sub_Menu_li_ListStart("#", "la la-school", "Education Type");
        //        if (AuthorizationUtility.userHasPrivilege(privileges, EnumPrivilegesName.ADD_EDUCATION_TYPE))
        //        {
        //            html = html + MenuUtility.Sub_Menu_li_List_ul("/MasterData/EducationTypeManagement/Add", "Add Education Type");
        //        }
        //        if (AuthorizationUtility.userHasPrivilege(privileges, EnumPrivilegesName.VIEW_EDUCATION_TYPE))
        //        {
        //            html = html + MenuUtility.Sub_Menu_li_List_ul("/MasterData/EducationTypeManagement/Manage", "View Education Type");
        //        }
        //        html = html + MenuUtility.Sub_Menu_li_ListEnd();
        //    }
        //    //End
        //    //Institute Management
        //    if (AuthorizationUtility.userHasPrivilegeCategory(privileges, EnumPrivilegeCategory.INSTITUTE_MANAGEMENT))
        //    {
        //        html = html + MenuUtility.Sub_Menu_li_ListStart("#", "la la-university", "Institute");
        //        if (AuthorizationUtility.userHasPrivilege(privileges, EnumPrivilegesName.ADD_INSTITUTE))
        //        {
        //            html = html + MenuUtility.Sub_Menu_li_List_ul("/MasterData/InstituteManagement/Add", "Add Institute");
        //        }
        //        if (AuthorizationUtility.userHasPrivilege(privileges, EnumPrivilegesName.VIEW_INSTITUTE))
        //        {
        //            html = html + MenuUtility.Sub_Menu_li_List_ul("/MasterData/InstituteManagement/Manage", "View Institute");
        //        }
        //        html = html + MenuUtility.Sub_Menu_li_ListEnd();
        //    }
        //    //End
        //    //Qualification Management
        //    if (AuthorizationUtility.userHasPrivilegeCategory(privileges, EnumPrivilegeCategory.QUALIFICATION_MANAGEMENT))
        //    {
        //        html = html + MenuUtility.Sub_Menu_li_ListStart("#", "la la-university", "Qualification");
        //        if (AuthorizationUtility.userHasPrivilege(privileges, EnumPrivilegesName.ADD_QUALIFICATION))
        //        {
        //            html = html + MenuUtility.Sub_Menu_li_List_ul("/MasterData/QualificationManagement/Add", "Add Qualification");
        //        }
        //        if (AuthorizationUtility.userHasPrivilege(privileges, EnumPrivilegesName.VIEW_QUALIFICATION))
        //        {
        //            html = html + MenuUtility.Sub_Menu_li_List_ul("/MasterData/QualificationManagement/Manage", "View Qualification");
        //        }
        //        html = html + MenuUtility.Sub_Menu_li_ListEnd();
        //    }
        //    //End
        //    //Bank Management
        //    if (AuthorizationUtility.userHasPrivilegeCategory(privileges, EnumPrivilegeCategory.BANK_MANAGEMENT))
        //    {
        //        html = html + MenuUtility.Sub_Menu_li_ListStart("#", "la la-money-check", "Bank");
        //        if (AuthorizationUtility.userHasPrivilege(privileges, EnumPrivilegesName.ADD_BANK))
        //        {
        //            html = html + MenuUtility.Sub_Menu_li_List_ul("/MasterData/BankManagement/Add", "Add Bank");
        //        }
        //        if (AuthorizationUtility.userHasPrivilege(privileges, EnumPrivilegesName.VIEW_BANK))
        //        {
        //            html = html + MenuUtility.Sub_Menu_li_List_ul("/MasterData/BankManagement/Manage", "View Bank");
        //        }
        //        html = html + MenuUtility.Sub_Menu_li_ListEnd();
        //    }
        //    //End
        //    //Grade Management
        //    if (AuthorizationUtility.userHasPrivilegeCategory(privileges, EnumPrivilegeCategory.GRADE_MANAGEMENT))
        //    {
        //        html = html + MenuUtility.Sub_Menu_li_ListStart("#", "la la-sort-alpha-up-alt ", "Grade");
        //        if (AuthorizationUtility.userHasPrivilege(privileges, EnumPrivilegesName.ADD_GRADE))
        //        {
        //            html = html + MenuUtility.Sub_Menu_li_List_ul("/MasterData/GradeManagement/Add", "Add Grade");
        //        }
        //        if (AuthorizationUtility.userHasPrivilege(privileges, EnumPrivilegesName.VIEW_GRADE))
        //        {
        //            html = html + MenuUtility.Sub_Menu_li_List_ul("/MasterData/GradeManagement/Manage", "View Grde");
        //        }
        //        html = html + MenuUtility.Sub_Menu_li_ListEnd();
        //    }
        //    //End
        //    //Leave Typee Management
        //    if (AuthorizationUtility.userHasPrivilegeCategory(privileges, EnumPrivilegeCategory.LEAVE_TYPE_MANAGEMENT))
        //    {
        //        html = html + MenuUtility.Sub_Menu_li_ListStart("#", "la la-pen-square", "Leave Type");
        //        if (AuthorizationUtility.userHasPrivilege(privileges, EnumPrivilegesName.ADD_LEAVE_TYPE))
        //        {
        //            html = html + MenuUtility.Sub_Menu_li_List_ul("/MasterData/LeaveTypeManagement/Add", "Add Leave Type");
        //        }
        //        if (AuthorizationUtility.userHasPrivilege(privileges, EnumPrivilegesName.VIEW_LEAVE_TYPE))
        //        {
        //            html = html + MenuUtility.Sub_Menu_li_List_ul("/MasterData/LeaveTypeManagement/Manage", "View Leave Type");
        //        }
        //        html = html + MenuUtility.Sub_Menu_li_ListEnd();
        //    }
        //    //End

        //    //Level Management
        //    if (AuthorizationUtility.userHasPrivilegeCategory(privileges, EnumPrivilegeCategory.LEVEL_MANAGEMENT))
        //    {
        //        html = html + MenuUtility.Sub_Menu_li_ListStart("#", "la la-sliders-h", "Level");
        //        if (AuthorizationUtility.userHasPrivilege(privileges, EnumPrivilegesName.ADD_LEVEL))
        //        {
        //            html = html + MenuUtility.Sub_Menu_li_List_ul("/MasterData/LevelManagement/Add", "Add Level");
        //        }
        //        if (AuthorizationUtility.userHasPrivilege(privileges, EnumPrivilegesName.VIEW_LEVEL))
        //        {
        //            html = html + MenuUtility.Sub_Menu_li_List_ul("/MasterData/LevelManagement/Manage", "View Level");
        //        }
        //        html = html + MenuUtility.Sub_Menu_li_ListEnd();
        //    }
        //    //End


        //    //Category Management
        //    if (AuthorizationUtility.userHasPrivilegeCategory(privileges, EnumPrivilegeCategory.CATEGORY_MANAGEMENT))
        //    {
        //        html = html + MenuUtility.Sub_Menu_li_ListStart("#", "la la-border-all", "Category");
        //        if (AuthorizationUtility.userHasPrivilege(privileges, EnumPrivilegesName.ADD_CATEGORY))
        //        {
        //            html = html + MenuUtility.Sub_Menu_li_List_ul("/MasterData/CategoryManagement/Add", "Add Category");
        //        }
        //        if (AuthorizationUtility.userHasPrivilege(privileges, EnumPrivilegesName.VIEW_CATEGORY))
        //        {
        //            html = html + MenuUtility.Sub_Menu_li_List_ul("/MasterData/CategoryManagement/Manage", "View Category");
        //        }
        //        html = html + MenuUtility.Sub_Menu_li_ListEnd();
        //    }
        //    //End



        //    return html;
        //}
    }
}
