using BNPL_Web.Common.ViewModels;
using BNPL_Web.Common.ViewModels.Authorization;
using BNPL_Web.DataAccessLayer.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BNPL_Web.Controllers.ApiControllers
{
    [Route("api/Roles")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRoleService service;
        public RolesController(IServiceProvider provider)
        {
            service = (IRoleService)provider.GetService(typeof(IRoleService));

        }
        [HttpPost]
        [Route("Add")]
        public IActionResult Add(RolesViewModel role)
        {
            
            var response = service.Add(role);
            return StatusCode((int)response.Status, response.obj);
        }
        [Route("GetAllPrivilegesAndRole")]
        [HttpGet]
        public IActionResult GetAllPrivilegesAndRole()
        {
            var response = service.GetAllPrivilegeAndRole();
            return StatusCode((int)response.Status, response.obj);
        }
        [Route("AssignPrivilegeToRole")]
        [HttpPost]
        public IActionResult AssignPrivilegeToRole(AssignPrivilegesViewModel[] value)
        {
            var response = service.AssignViewsToRole(value);
            return StatusCode((int)response.Status, response.obj);
        }

        [Route("GetAssignPrivilegeByRoleId")]
        [HttpGet]
        public IActionResult GetAssignPrivilegeByRoleId(string Id)
        {
            var response = service.GetAssignPrivilegeByRoleId(Id);
            return StatusCode((int)response.Status, response.obj);
        }
        [Route("Put")]
        [HttpPost]
        public IActionResult Put(RolesViewModel value)
        {
            var response = service.Update(value);
            return StatusCode((int)response.Status, response.obj);
        }
        [Route("Delete")]
        [HttpDelete]
        public IActionResult Delete(Guid Id)
        {
            var response = service.Delete(Id);
            return StatusCode((int)response.Status, response.obj);
        }
        [Route("GetAllRole")]
        [HttpGet]
        public IActionResult GetAllRole()
        {
            var response = service.GetAllRole();
            return StatusCode((int)response.Status, response.obj);
        }

    }
}
