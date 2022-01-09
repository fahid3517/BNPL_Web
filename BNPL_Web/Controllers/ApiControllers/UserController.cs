using BNPL_Web.Common.ViewModels;
using BNPL_Web.Common.ViewModels.Authorization;
using BNPL_Web.Common.ViewModels.Common;
using BNPL_Web.DataAccessLayer.Helpers;
using BNPL_Web.DataAccessLayer.IServices;
using BNPL_Web.DatabaseModels.DbImplementation;
using BNPL_Web.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;
        public UserController(IServiceProvider provider, IConfiguration configuration, SignInManager<ApplicationUser> signInManager)
        {
            UserService = (IUserService)provider.GetService(typeof(IUserService));
            _configuration = configuration;
            _signInManager = signInManager;
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

        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            try
            {
                var response = new AdminLoginResponse();
                string tokenKey = _configuration.GetValue<string>("Tokens:Key");

            if (!ModelState.IsValid)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ModelState.Values.SelectMany(v => v.Errors.Select(z => z.ErrorMessage)));
            }

            // This doesn't count login failures towards lockout only two factor authentication
            // To enable password failures to trigger lockout, change to shouldLockout: true
            bool isActive = true;
            if (!isActive)
            {
                ModelState.AddModelError(nameof(model.Password), "Inactive user login attempt.");
                return StatusCode(StatusCodes.Status403Forbidden, ModelState.Values.SelectMany(v => v.Errors.Select(z => z.ErrorMessage)));
            }
            var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, model.RememberMe, lockoutOnFailure: false);

            var authToken = new Encryption().GetToken(tokenKey);
                response = new AdminLoginResponse
                {
                    AccessToken = authToken,
                };

                if (result.Succeeded)
                   
                return Ok(response);
            else
                return StatusCode(StatusCodes.Status400BadRequest, ModelState.Values.SelectMany(v => v.Errors.Select(z => z.ErrorMessage)));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
