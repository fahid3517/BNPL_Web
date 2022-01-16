
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using BNPL_Web.Common.ViewModels;
using BNPL_Web.Common.ViewModels.Authorization;
using BNPL_Web.DatabaseModels.Authentication;
using BNPL_Web.DatabaseModels.DbImplementation;
using BNPL_Web.DatabaseModels.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Project.Utilities;

namespace BNPL_Web.DataAccessLayer.Helpers
{
    public static class IdentityHelper
    {

        public static FunctionResult createUser(CustomerViewModel user)
        {
            try
            {

                HttpContextAccessor context = new HttpContextAccessor();
                UserManager<ApplicationUser> userManager = (UserManager<ApplicationUser>)context.HttpContext.RequestServices.GetService(typeof(UserManager<ApplicationUser>));
                var unitofwork = (UnitOfWork)context.HttpContext.RequestServices.GetService(typeof(IUnitOfWork));


                var appuser = new AspNetUser();
                appuser.UserName = user.CivilId.Trim();
                appuser.Email = user.Email;
                appuser.CreatedBy = user.UserName;
                appuser.CreatedAt = DateTime.Now;
                appuser.IsDisable = false;
                appuser.Id = Guid.NewGuid().ToString();
                string HashPassword = "";
                using (MD5 md5Hash = MD5.Create())
                {
                    var bytes = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(user.Password));

                    HashPassword = Convert.ToBase64String(bytes);
                }
                appuser.PasswordHash = HashPassword;
                unitofwork.AspNetUser.Add(appuser);
                unitofwork.AspNetUser.Commit();


                // IdentityResult result = await userManager.CreateAsync(appuser, user.Password);
                if (appuser != null)
                {


                    var usesrProfile = new UserProfile();
                    usesrProfile.UserId = appuser.Id;
                    usesrProfile.ProfileId = "DFDFFA39-3048-447A-F78C-08D9D408F6DC";
                    unitofwork.UserProfile.Add(usesrProfile);
                    unitofwork.UserProfile.Commit();

                    return new FunctionResult { success = true, message = appuser.Id };
                }
                return new FunctionResult { success = false, message = "Not Registered Successfully" };
            }
            catch (Exception ex)
            {
                return new FunctionResult { success = false, message = ex.Message };
            }
        }
        public static FunctionResult BackOfficeUser(UserViewModel user)
        {
            try
            {

                HttpContextAccessor context = new HttpContextAccessor();
                UserManager<ApplicationUser> userManager = (UserManager<ApplicationUser>)context.HttpContext.RequestServices.GetService(typeof(UserManager<ApplicationUser>));
                var unitofwork = (UnitOfWork)context.HttpContext.RequestServices.GetService(typeof(IUnitOfWork));


                var appuser = new AspNetUser();
                appuser.UserName = user.UserName.Trim();
                appuser.PhoneNumber = user.PhoneNumber;
                appuser.Email = user.Email;
                appuser.CreatedBy = user.UserName;
                appuser.CreatedAt = DateTime.Now;
                appuser.IsDisable = false;
                appuser.Id = Guid.NewGuid().ToString();

                string HashPassword = "";
                using (MD5 md5Hash = MD5.Create())
                {
                    var bytes = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(user.Password));

                    HashPassword = Convert.ToBase64String(bytes);
                }
                appuser.PasswordHash = HashPassword;

                appuser.PasswordHash = HashPassword;
                unitofwork.AspNetUser.Add(appuser);
                unitofwork.AspNetUser.Commit();


                // IdentityResult result = await userManager.CreateAsync(appuser, user.Password);
                if (appuser != null)
                {


                    var usesrProfile = new UserProfile();
                    usesrProfile.UserId = appuser.Id;
                    usesrProfile.ProfileId = user.RoleId;
                    unitofwork.UserProfile.Add(usesrProfile);
                    unitofwork.UserProfile.Commit();

                    return new FunctionResult { success = true, message = appuser.Id };
                }
                return new FunctionResult { success = false, message = "Not Registered Successfully" };
            }
            catch (Exception ex)
            {
                return new FunctionResult { success = false, message = ex.Message };
            }
        }
        public async static Task<FunctionResult> SystemcreateUser(SystemUserModel user)
        {
            HttpContextAccessor context = new HttpContextAccessor();
            UserManager<ApplicationUser> userManager = (UserManager<ApplicationUser>)context.HttpContext.RequestServices.GetService(typeof(UserManager<ApplicationUser>));
            ApplicationUser appuser = new ApplicationUser();
            appuser.UserName = user.UserName.Trim();
            appuser.PhoneNumber = user.PhoneNumber;
            appuser.Email = user.Email;
            appuser.CreatedBy = user.UserName;
            appuser.CreatedAt = DateTime.Now;
            appuser.IsDisable = false;


            IdentityResult result = await userManager.CreateAsync(appuser, user.Password);

            try
            {
                if (result.Succeeded)
                {
                    var role = AuthorizationUtility.getUserRoleNamebyRoleId(user.RoleId);
                    await userManager.AddToRoleAsync(appuser, role.ToString());
                    return new FunctionResult { success = true, message = appuser.Id };
                }
                else
                {
                    String error = "";
                    foreach (var err in result.Errors)
                    {
                        //if (err.Description.Contains("taken"))
                        //{
                        //    //canUpdate = true;
                        //    break;
                        //}
                        error += err.Description + " ";
                    }

                    return new FunctionResult { success = false, message = error };
                }
            }
            catch (Exception ex)
            {
                return new FunctionResult { success = false, message = ex.Message };
            }
        }
    }
}
