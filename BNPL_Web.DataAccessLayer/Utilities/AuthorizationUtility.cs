
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

using System.Net.Http;
using Project.DatabaseModel.DbImplementation;
using BNPL_Web.Common.ViewModels.Authorization;

using BNPL_Web.DatabaseModels.DTOs;
using BNPL_Web.DatabaseModels.DbImplementation;
using BNPL_Web.DatabaseModels.Authentication;
using BNPL_Web.Authentications;

namespace Project.Utilities
{
    public static class AuthorizationUtility
    {
        public static bool userHasPrivilege(string Id, string privilege)
        {
            if (!String.IsNullOrEmpty(Id))
            {
                HttpContextAccessor context = new HttpContextAccessor();
                var unitofwork = (UnitOfWork)context.HttpContext.RequestServices.GetService(typeof(IUnitOfWork));

               
                AspNetRole privilegeDb = unitofwork.AspNetRole.Get(a => a.Privilege == privilege);

                //Get Role of user
                var aspnet = unitofwork.AspNetUser.Get(x => x.Id == Id);
                var UserRole = unitofwork.UserProfile.Get(x => x.UserId == aspnet.Id);
                var RoleClaims = unitofwork.AspNetProfileRole.GetMany(x => x.RoleId ==Guid.Parse(UserRole.ProfileId.ToString()));
                if (aspnet != null)
                {
                    foreach (var emp_privilege in RoleClaims)
                    {
                        if (emp_privilege.ProfileId.Equals(privilegeDb.Id))
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
        //public static IEnumerable<AssignPrivilegesViewModel> Getuserivilege(string userName)
        //{
        //    HttpContextAccessor context = new HttpContextAccessor();
        //    var unitofwork = (UnitOfWork)context.HttpContext.RequestServices.GetService(typeof(IUnitOfWork));
        //    string RoleId = null;
        //    var user = unitofwork.AspNetUser.Get(x => x.UserName == userName, "AspNetUserRoles");

        //    if (user.AspNetUserRoles != null && user.AspNetUserRoles.Count() > 0)
        //    {
        //        RoleId = user.AspNetUserRoles.FirstOrDefault().RoleId;

        //    }

        //    IEnumerable<AssignPrivilegesViewModel> _data = unitofwork.RolePrivilegeRepository.GetMany(p => p.RoleId == RoleId, "Privilege").Select(p => new AssignPrivilegesViewModel()
        //    {
        //        RoleId = p.RoleId,
        //        Name = p.Privilege.Privilege,
        //        PrivilegeId = p.PrivilegeId,
        //        Category = p.Privilege.Category,
        //        Portal = p.Privilege.Portal
        //    });
        //    return _data;
        //}
        public static bool userHasPrivilege(List<AssignPrivilegesViewModel> privileges, string privilege)
        {
            bool hasPrivilege = false;
            if (privileges.Count > 0)
            {
                foreach (var prev in privileges)
                {
                    if (prev.Name == privilege)
                    {
                        hasPrivilege = true;
                        break;
                    }
                }
            }
            return hasPrivilege;
        }

        public static bool userHasPrivilegeCategory(List<AssignPrivilegesViewModel> privileges, string category)
        {
            bool hasCategory = false;
            if (privileges.Count > 0)
            {
                foreach (var prev in privileges)
                {
                    if (prev.Category == category)
                    {
                        hasCategory = true;
                        break;
                    }
                }
            }
            return hasCategory;
        }

        public static bool userHasPrivilegePortal(List<AssignPrivilegesViewModel> privileges, string portal)
        {
            bool hasPortal = false;
            if (privileges.Count > 0)
            {
                foreach (var prev in privileges)
                {
                    if (prev.Portal == portal)
                    {
                        hasPortal = true;
                        break;
                    }
                }
            }
            return hasPortal;
        }
        public static IEnumerable<AssignPrivilegesViewModel> Getuserivilege(string userName)
        {
            IEnumerable<AssignPrivilegesViewModel> assignPrivilegesViewModels = new List<AssignPrivilegesViewModel>();
            HttpContextAccessor context = new HttpContextAccessor();
            var unitofwork = (UnitOfWork)context.HttpContext.RequestServices.GetService(typeof(IUnitOfWork));
            string RoleId = "";
            var user = unitofwork.AspNetUser.Get(x => x.UserName == userName);

            var privilage = unitofwork.UserProfile.Get(x => x.UserId == user.Id);

            if (privilage != null)
            {
                RoleId = privilage.ProfileId;
            }
            IEnumerable<AssignPrivilegesViewModel> _data = unitofwork.AspNetProfileRole.GetMany(p => p.RoleId ==Guid.Parse(RoleId.ToString()), "Profile").Select(p => new AssignPrivilegesViewModel()
            {
                RoleId = p.RoleId,
                Name = p.Profile.Privilege,
                PrivilegeId = p.Profile.Id,
                Category = p.Profile.Category,
                Portal = p.Profile.Portal
            });
            //int counbt = _data.Count();
            return assignPrivilegesViewModels;
        }
        public static Credentials GetUserCredentialsFromAuthorizationHeader(HttpContext request)
        {
            try
            {
                var authHeader = request.Request.Headers["Authorization"].ToString();
                if (!string.IsNullOrEmpty(authHeader))
                {
                    var token = authHeader.Substring("Basic ".Length).Trim();
                    Credentials credentials = new Credentials();
                    string decodedCredentials = Encoding.ASCII.GetString(Convert.FromBase64String(token));

                    string[] credentialsArray = decodedCredentials.Split(':');

                    if (!(credentialsArray.Length < 2 || string.IsNullOrEmpty(credentialsArray[0]) || string.IsNullOrEmpty(credentialsArray[1])))
                    {
                        credentials = new Credentials(credentialsArray[0], credentialsArray[1]);
                    }
                    return credentials;
                }
                else
                {
                    return null;
                }
            }

            catch (Exception ex)
            {

                return null;
            }

        }

        public async static Task<bool> IsValidaspnetUser(string userName, string password)
        {
            HttpContextAccessor context = new HttpContextAccessor();

            try
            {
                UserManager<ApplicationUser> userManager = (UserManager<ApplicationUser>)context.HttpContext.RequestServices.GetService(typeof(UserManager<ApplicationUser>));

                var user = await userManager.FindByNameAsync(userName);
                if (user == null)
                {
                    return false;
                }
                else
                {
                    return await userManager.CheckPasswordAsync(user, password);
                };
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static string getUserRoleNamebyRoleId(string roleId)
        {
            //HttpContextAccessor context = new HttpContextAccessor();
            //var unitofwork = (UnitOfWork)context.HttpContext.RequestServices.GetService(typeof(IUnitOfWork));

            //if (!string.IsNullOrEmpty(roleId))
            //{
            //    var role = unitofwork.AspNetRole.Get(p => p.Id == roleId);
            //    if (role != null)
            //        return role.Name;
            //    else
            //        return null;
            //}
            //return null;
            return "";
        }
    }
    //public static bool LoginLog(string username)
    //{
    //    AspNetUserRepository UserRepository = new AspNetUserRepository();
    //    EmployeeDetailRepository _EmployeeRepository = new EmployeeDetailRepository();
    //    LoginLogRepository _LoginLogRepository = new LoginLogRepository();
    //    AspNetUser AspUser = UserRepository.Get(s => s.UserName == username);

    //    DB_EMPLOYEE dbEmployee = _EmployeeRepository.Get(p => p.UserId == AspUser.Id);
    //    if (dbEmployee == null)
    //    {
    //        return false;
    //    }

    //    var Date = DateTimeUtility.DateTimeNowInPKTimeZone();

    //    DB_LOGIN_LOG dB_LOGIN_LOG = new DB_LOGIN_LOG();
    //    dB_LOGIN_LOG.EmployeeId = dbEmployee.Id;
    //    dB_LOGIN_LOG.Name = username;
    //    dB_LOGIN_LOG.Date = Date;
    //    dB_LOGIN_LOG.CreatedBy = username;
    //    dB_LOGIN_LOG.CreatedOn = DateTimeUtility.DateTimeNowInPKTimeZone();
    //    _LoginLogRepository.Add(dB_LOGIN_LOG);
    //    _LoginLogRepository.Commit();

    //    return true;

    //}
}
