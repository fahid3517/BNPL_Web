using BNPL_Web.Common.ViewModels;
using BNPL_Web.Common.ViewModels.Authorization;
using BNPL_Web.DataAccessLayer.Helpers;
using BNPL_Web.DataAccessLayer.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.DataAccessLayer.Utilities;
using System.Net;

namespace BNPL_Web.Controllers.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService service;
        public UserController(IServiceProvider provider)
        {
            service = (IUserService)provider.GetService(typeof(IUserService));

        }


        [HttpPost]
        [Route("Post")]
        public async Task<IActionResult> Post(UserViewModel model)
        {
           // var validate = CommonUtility.ValidateEmployee(model);
            //if (!validate.success)
            //{
            //    return StatusCode((int)HttpStatusCode.BadRequest, validate.message);
            //}

            FunctionResult result = await IdentityHelper.createUser(model);
            if (!result.success)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, result.message);
            }
            //model.UserName = result.message;
            //model.ChangedOn = DateTime.Now;
            //model.ChangedBy = User.Identity.Name;
            //var response = _EmployeeService.Add(model);
            //return StatusCode((int)response.Status, response.obj);
            return Ok();
        }
    }
}
