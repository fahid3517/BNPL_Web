using BNPL_Web.Common.ViewModels.Authorization;
using BNPL_Web.Common.ViewModels.Common;
using BNPL_Web.DatabaseModels.DbImplementation;
using BNPL_Web.DatabaseModels.DTOs;
using BNPL_Web.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace BNPL_Web.Authorizations
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class AdminAuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        private string rightId = Guid.Empty.ToString();
        public AdminAuthorizeAttribute(string rightId)
        {
            this.rightId = rightId.ToString();
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            AdminAuthToken tokenData = null;
            string token = string.Empty;

            token = (context.HttpContext.Request.Headers.Any(x => x.Key == "Authorization")) ? context.HttpContext.Request.Headers.Where(x => x.Key == "Authorization").FirstOrDefault().Value.SingleOrDefault().Replace("Bearer ", "") : "";
            if (token == string.Empty)
            {
                context.HttpContext.Response.StatusCode = 401;
                context.Result = new JsonResult(new Response<bool> { IsError = true, Message = Error.MissingAuthorization, Data = false });
                return;
            }

            if (!string.IsNullOrEmpty(token))
            {
                try
                {
                    var configuration = (IConfiguration)context.HttpContext.RequestServices.GetService(typeof(IConfiguration));
                    var keyByteArray = Encoding.ASCII.GetBytes(configuration.GetValue<String>("Tokens:Key"));
                    var signinKey = new SymmetricSecurityKey(keyByteArray);

                    SecurityToken validatedToken;
                    var handeler = new JwtSecurityTokenHandler();
                    //var we = handeler.ValidateToken(token, new TokenValidationParameters
                    //{
                    //    IssuerSigningKey = signinKey,
                    //    ValidAudience = "Audience",
                    //    ValidIssuer = "Issuer",
                    //    ValidateIssuerSigningKey = true,
                    //    ValidateLifetime = true,
                    //    ClockSkew = TimeSpan.FromMinutes(0)
                    //}, out validatedToken);

                    var temp = handeler.ReadJwtToken(token);
                    tokenData = JsonConvert.DeserializeObject<AdminAuthToken>(temp.Claims.FirstOrDefault(x => x.Type.Equals("token"))?.Value);

                    if (tokenData != null)
                    {
                        var unitofwork = (UnitOfWork)context.HttpContext.RequestServices.GetService(typeof(IUnitOfWork));

                        AspNetRole privilegeDb = unitofwork.AspNetRole.Get(a => a.Privilege == rightId.ToString());

                        //Get Role of user
                        var aspnet = unitofwork.AspNetUser.Get(x => x.Id == tokenData.UserId);
                        var UserRole = unitofwork.UserProfile.Get(x => x.UserId == aspnet.Id);
                        var RoleClaims = unitofwork.AspNetProfileRole.GetMany(x => x.RoleId == Guid.Parse(UserRole.ProfileId.ToString()));
                        if (aspnet != null)
                        {
                            foreach (var emp_privilege in RoleClaims)
                            {
                                if (emp_privilege.ProfileId.Equals(privilegeDb.Id))
                                {
                                    return;
                                }
                            }
                        }

                    }

                }
                catch (Exception ex)
                {
                    context.HttpContext.Response.StatusCode = 401;
                    context.Result = new JsonResult(new Response<bool> { IsError = true, Message = Error.AccessDenied, Data = false });
                    return;
                }

                //if (!tokenData.RoleId.ToString().ToLower().Equals("5bb20bf7-30fe-473f-8967-478f1f18c5d0"))
                //{
                //    var accountService = (IAccountService)context.HttpContext.RequestServices.GetService(typeof(IAccountService));

                //    var isAuthenticated = accountService.IsAdminUserAllowed(tokenData.UserId, tokenData.RoleId, rightId);

                //    if (!isAuthenticated)
                //    {
                //        context.HttpContext.Response.StatusCode = 401;
                //        context.Result = new JsonResult(new Response<bool> { IsError = true, Message = Error.AccessDenied, Data = false });
                //        return;
                //    }
                //}

            }
        }
    }
}
