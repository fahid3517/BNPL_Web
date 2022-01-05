using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Project.Utilities;
using System.Net;
using System.Threading.Tasks;
using System.Web;

namespace Project.Web.CustomeAnnotations
{
    public class MvcCustomAuthorizeAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        public string privilege { get; set; }
        public MvcCustomAuthorizeAttribute(string value = null)
        {
            if (value != null)
                privilege = value;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (string.IsNullOrEmpty(privilege))
            {
                if (!context.HttpContext.User.Identity.IsAuthenticated)
                {
                    context.Result = new ForbidResult();
                }
                return;
            }
            else if (context.HttpContext.User.Identity.IsAuthenticated && AuthorizationUtility.userHasPrivilege(context.HttpContext.User.Identity.Name, privilege))
            {
                return;
            }
            else
            {
                context.Result = new ForbidResult();
            }
            
        }
    }
}
