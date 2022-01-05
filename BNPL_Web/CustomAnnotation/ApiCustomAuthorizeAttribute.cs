using BNPL_Web.DatabaseModels.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

using Project.Utilities;
using System;
using System.Net;
using System.Threading.Tasks;

namespace BNPL_Web.CustomAnnotation
{
    public class ApiCustomAuthorizeAttribute : AuthorizeAttribute, IAuthorizationFilter, IAuthorizationMiddlewareResultHandler
    {
        public string? privilege { get; set; }
        public ApiCustomAuthorizeAttribute(string? value = null)
        {
            if (value != null)
                privilege = value;
        }
        public async Task HandleAsync(RequestDelegate next, HttpContext context, AuthorizationPolicy policy, PolicyAuthorizationResult authorizeResult)
        {
            await next.Invoke(context);
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (IsIdentityAuthorized(context.HttpContext))
            {
                return;
            }
            else if (IsLoginAuthorized(context.HttpContext))
            {
                return;
            }
            else
            {
                context.Result = new UnauthorizedResult();
                return;
            };
        }
       
        private bool IsIdentityAuthorized(HttpContext actionContext)
        {

            if (string.IsNullOrEmpty(privilege))
                return actionContext.User.Identity.IsAuthenticated;
            else
            {
                if (actionContext.User.Identity.IsAuthenticated)
                {
                    if (AuthorizationUtility.userHasPrivilege(actionContext.User.Identity.Name, privilege))
                    {
                        return true;
                    }

                    return false;
                }

                return false;
            }
        }
        private bool IsLoginAuthorized(HttpContext actionContext)
        {
            try
            {
                Credentials credentials = AuthorizationUtility.GetUserCredentialsFromAuthorizationHeader(actionContext);
                if (credentials == null)
                {
                    return false;
                }

                var isValidUser = AuthorizationUtility.IsValidaspnetUser(credentials.UserName, credentials.Password);
                if (!isValidUser.Result)
                {
                    return false;
                }
                if (!string.IsNullOrEmpty(privilege))
                {
                    if (!AuthorizationUtility.userHasPrivilege(credentials.UserName, privilege))
                    {
                        return false;
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
    }
}
