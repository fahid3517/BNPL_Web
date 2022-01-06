using BNPL_Web.Common.ViewModels;
using BNPL_Web.Common.ViewModels.Authorization;
using BNPL_Web.Common.ViewModels.Common;
using BNPL_Web.DataAccessLayer.Helpers;
using BNPL_Web.DataAccessLayer.IServices;
using BNPL_Web.DatabaseModels.DbImplementation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.DataAccessLayer.Utilities;
using System.Net;

namespace BNPL_Web.Controllers.ApiControllers
{
    [Route("api/User")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService UserService;
        public UserController(IServiceProvider provider)
        {
            UserService = (IUserService)provider.GetService(typeof(IUserService));

        }

        [HttpPost]
        [Route("Post")]
        public async Task<IActionResult> Post(UserViewModel model)
        {
            FunctionResult result = await IdentityHelper.createUser(model);
            if (!result.success)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, result.message);
            }
            model.UserId = result.message;
           
            var response = UserService.Add(model);
            return StatusCode((int)response.Status, response.obj);
        }
        [HttpPost]
        [Route("BackOfficeUserProfile")]
        public async Task<IActionResult> BackOfficeUserProfile(UserViewModel model)
        {
            FunctionResult result = await IdentityHelper.createUser(model);
            if (!result.success)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, result.message);
            }
            model.UserId = result.message;

            var response = UserService.AddBackOfficeUserProfile(model);
            return StatusCode((int)response.Status, response.obj);
        }
        [HttpPost]
        [Route("SystemUserProfile")]
        public async Task<IActionResult> SystemUserProfile(UserViewModel model)
        {
            FunctionResult result = await IdentityHelper.createUser(model);
            if (!result.success)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, result.message);
            }
            model.UserId = result.message;

            var response = UserService.SystemUserProfile(model);
            return StatusCode((int)response.Status, response.obj);
        }
        [HttpPost]
        [Route("GetAll")]
        public IActionResult GetAll()
        {
            PaginationSearchModel _PaginationSearchModel = new PaginationSearchModel();
            bool jsonContent = Request.HasFormContentType;

            if (jsonContent)
            {
                IFormCollection coll = Request.ReadFormAsync().Result;
                _PaginationSearchModel.PageStart = Int32.Parse(coll["start"]);
                _PaginationSearchModel.PageSize = Int32.Parse(coll["length"]);
                _PaginationSearchModel.Draw = Int32.Parse(coll["draw"]);
                _PaginationSearchModel.Search = coll["search[value]"];
                _PaginationSearchModel.sorting = Int32.Parse(coll["order[0][column]"]);
                _PaginationSearchModel.direction = coll["order[0][dir]"];


            }
            try
            {
                var response = UserService.GetPaginatedRecords(_PaginationSearchModel);
                return Ok(response);

            }
            catch (Exception ex)
            {

                return StatusCode((int)HttpStatusCode.BadRequest, ex.Message);
            }
        }
    }
}
