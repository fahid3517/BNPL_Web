using BNPL_Web.Common.ViewModels;
using BNPL_Web.DataAccessLayer.IServices;
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
        [Route("Post")]
        [HttpPost]
        public IActionResult Post(RolesViewModel model)
        {
            var response = service.Add(model);
            return StatusCode((int)response.Status, response.obj);
        }
    }
}
