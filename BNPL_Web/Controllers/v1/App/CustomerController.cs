using BNPL_Web.Common.ViewModels;
using BNPL_Web.Common.ViewModels.Authorization;
using BNPL_Web.DataAccessLayer.Helpers;
using BNPL_Web.DataAccessLayer.IServices;
using BNPL_Web.DatabaseModels.DbImplementation;
using BNPL_Web.DatabaseModels.Models;
using BNPL_Web.Notification.Interface;
using BNPL_Web.Notification.Models;
using BNPL_Web.smsService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;
using System.Net;
using BNPL_Web.Helpers;

namespace BNPL_Web.Controllers.v1.App
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {

        private readonly IUserService UserService;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWork _unitOfWork;
        private readonly BNPL_Context _DB;
        private readonly ISmsService _smsservice;
        public CustomerController(IServiceProvider provider, IConfiguration configuration, SignInManager<ApplicationUser> signInManager, IUnitOfWork unitOfWork, BNPL_Context DB,
            ISmsService smsservice)
        {
            UserService = (IUserService)provider.GetService(typeof(IUserService));
            _configuration = configuration;
            _signInManager = signInManager;
            _unitOfWork = unitOfWork;
            _DB = DB;
            _smsservice = smsservice;
        }


        [HttpPost]
        [Route("CustomerRegister")]
        public IActionResult Post(CustomerViewModel model)
        {
            FunctionResult result = IdentityHelper.createUser(model);
            if (!result.success)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, result.message);
            }
            model.UserId = result.message;

            var response = UserService.Add(model);
            if (response.Status == HttpStatusCode.OK)
            {
                int OTP = _smsservice.GenerateRandomNo();

            }
            return StatusCode((int)response.Status, response.obj);
        }
        [HttpPost]
        [Route("OtpSend")]

        public IActionResult OtpSend(string? CivilID, string? Mobile)
        {
            int OTP = _smsservice.GenerateRandomNo();
            if (_smsservice.SendSMS(OTP, Mobile))
            {
                var response1 = UserService.AddOtp(OTP, Mobile, CivilID);
                return StatusCode((int)response1.Status, response1.obj);
            }
            return StatusCode((int)HttpStatusCode.BadRequest, "");
        }
        [HttpPost]
        [Route("OtpVerify")]

        public IActionResult OtpVerify(string? CivilID, string? Number, string? OTP)
        {


            var response1 = UserService.VerifyOtp(CivilID, Number, OTP);
            return StatusCode((int)response1.Status, response1.obj);
        }
        [HttpPost]
        [Route("CustomerLogin")]
        public async Task<ActionResult> CustomerLogin(AdminLoginViewModel model)
        {
            try
            {
                var authToken = "";
                var response = new AdminLoginResponse();
                string tokenKey = _configuration.GetValue<string>("Tokens:Key");

                if (!ModelState.IsValid)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, ModelState.Values.SelectMany(v => v.Errors.Select(z => z.ErrorMessage)));
                }
                string HashPassword = "";
                using (MD5 md5Hash = MD5.Create())
                {
                    var bytes = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(model.Password));

                    HashPassword = Convert.ToBase64String(bytes);
                }
                var pass = HashPassword;
                // This doesn't count login failures towards lockout only two factor authentication
                // To enable password failures to trigger lockout, change to shouldLockout: true
                bool isActive = true;
                if (!isActive)
                {
                    ModelState.AddModelError(nameof(model.Password), "Inactive user login attempt.");
                    return StatusCode(StatusCodes.Status403Forbidden, ModelState.Values.SelectMany(v => v.Errors.Select(z => z.ErrorMessage)));
                }
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {

                    var user = _unitOfWork.AspNetUser.Get(x => x.UserName == model.Email);
                    if (user != null)
                    {
                        var role = ""; /*_DB.UserRoles.Where(x => x.UserId == user.Id).FirstOrDefault();*/
                        if (role != null)
                        {
                            authToken = new Encryption().GetToken(new AdminAuthToken { UserId = user.Id, RoleId = "" }, user.Id, tokenKey);
                            response = new AdminLoginResponse
                            {
                                AccessToken = authToken,
                            };

                            return Ok(response);
                        }
                        else
                        {
                            return StatusCode(StatusCodes.Status400BadRequest, ModelState.Values.SelectMany(v => v.Errors.Select(z => z.ErrorMessage)));
                        }

                    }

                }


                return StatusCode(StatusCodes.Status400BadRequest, ModelState.Values.SelectMany(v => v.Errors.Select(z => z.ErrorMessage)));

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
