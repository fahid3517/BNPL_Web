
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

        public static FunctionResult createUser(UserViewModel user)
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
             
                string HashPassword=  GetM5Hash(user.Password);

                appuser.PasswordHash = HashPassword;
                unitofwork.AspNetUser.Add(appuser);
                unitofwork.AspNetUser.Commit();


               // IdentityResult result = await userManager.CreateAsync(appuser, user.Password);
                if (appuser!=null)
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

                string HashPassword = GetM5Hash(user.Password);

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
        public static string GetM5Hash(string input)
        {
            using (MD5 md5Hash = MD5.Create())
            {
                var bytes = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

                return Convert.ToBase64String(bytes);
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


        //public async static Task<bool> IsValidaspnetUser(string userName, string password)
        //{
        //    HttpContextAccessor context = new HttpContextAccessor();

        //    try
        //    {
        //        UserManager<ApplicationUser> userManager = (UserManager<ApplicationUser>)context.HttpContext.RequestServices.GetService(typeof(UserManager<ApplicationUser>));

        //        var user = await userManager.FindByNameAsync(userName);
        //        if (user == null)
        //        {
        //            return false;
        //        }
        //        else
        //        {
        //            return await userManager.CheckPasswordAsync(user, password);
        //        };
        //    }
        //    catch (Exception ex)
        //    {
        //        return false;
        //    }
        //}
        //public static string GetUserId(string userName)
        //{
        //    HttpContextAccessor context = new HttpContextAccessor();

        //    try
        //    {

        //        UserManager<ApplicationUser> userManager = (UserManager<ApplicationUser>)context.HttpContext.RequestServices.GetService(typeof(UserManager<ApplicationUser>));

        //        var user = userManager.FindByNameAsync(userName).Result;
        //        if (user == null)
        //        {
        //            return null;
        //        }
        //        return user.Id;
        //    }
        //    catch (Exception ex)
        //    {
        //        return null;
        //    }
        //}

        //public async static Task<FunctionResult> ChangePassword(string userName, string password)
        //{
        //    var errorMessage = "";
        //    HttpContextAccessor context = new HttpContextAccessor();


        //    UserManager<ApplicationUser> userManager = (UserManager<ApplicationUser>)context.HttpContext.RequestServices.GetService(typeof(UserManager<ApplicationUser>));

        //    var user = await userManager.FindByNameAsync(userName);
        //    if (user == null)
        //    {
        //        return new FunctionResult { success = false, message = "No Such User Found!" };
        //    }
        //    await userManager.RemovePasswordAsync(user);
        //    var result = await userManager.AddPasswordAsync(user, password);

        //    if (result.Succeeded)
        //    {
        //        return new FunctionResult { success = true, message = "Password Reset Successfuly!" };
        //    }
        //    foreach (var err in result.Errors)
        //    {
        //        errorMessage += err;
        //    }
        //    return new FunctionResult { success = false, message = errorMessage };
        //}

        //public async static Task<FunctionResult> UpdateUser(EmployeeViewModel user)
        //{
        //    HttpContextAccessor context = new HttpContextAccessor();
        //    UserManager<ApplicationUser> userManager = (UserManager<ApplicationUser>)context.HttpContext.RequestServices.GetService(typeof(UserManager<ApplicationUser>));
        //    var updateUser = await userManager.FindByNameAsync(user.UserName);
        //    try
        //    {
        //        if (updateUser == null)
        //        {
        //            return new FunctionResult { success = false, message = "UserName Is Not Exist!" };
        //        }

        //        updateUser.Email = user.Email;
        //        updateUser.PhoneNumber = user.Mobile;
        //        var roles = await userManager.GetRolesAsync(updateUser);

        //        var OldroleName = roles.FirstOrDefault();
        //        userManager.RemoveFromRoleAsync(updateUser, OldroleName).Wait();

        //        var Role = AuthorizationUtility.getUserRoleNamebyRoleId(user.RoleId);
        //        var result = await userManager.AddToRoleAsync(updateUser, Role);
        //        if (result.Succeeded)
        //        {
        //            return new FunctionResult { success = true, message = "Success!" };
        //        }
        //        else
        //        {
        //            String error = "";

        //            foreach(var err in result.Errors)
        //            {
        //                error += err;
        //            }
        //            return new FunctionResult { success = false, message = error };
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return new FunctionResult { success = false, message = ex.Message };
        //    }
        //}
    }
}
