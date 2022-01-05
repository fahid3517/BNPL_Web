﻿using BNPL_Web.Common.ViewModels;
using BNPL_Web.Common.ViewModels.Authorization;
using BNPL_Web.CustomAnnotation;
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
        public IActionResult Add(string  role)
        {
            RolesViewModel roles = new RolesViewModel();
            roles.Name = role;
            var response = service.Add(roles);
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
    }
}
