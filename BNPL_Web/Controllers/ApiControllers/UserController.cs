
using BNPL_Web.Common.Enums;
using BNPL_Web.Common.ViewModels;
using BNPL_Web.Common.ViewModels.Authorization;
using BNPL_Web.Common.ViewModels.Common;
using BNPL_Web.DataAccessLayer.Helpers;
using BNPL_Web.DataAccessLayer.IServices;
using BNPL_Web.DatabaseModels.DbImplementation;
using BNPL_Web.DatabaseModels.Models;
using BNPL_Web.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using Project.DataAccessLayer.Utilities;
using Project.Utilities;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace BNPL_Web.Controllers.ApiControllers
{
    [Route("api/User")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService UserService;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWork _unitOfWork;
        private readonly BNPL_Context _DB;
        public UserController(IServiceProvider provider, IConfiguration configuration, SignInManager<ApplicationUser> signInManager, IUnitOfWork unitOfWork, BNPL_Context DB)
        {
            UserService = (IUserService)provider.GetService(typeof(IUserService));
            _configuration = configuration;
            _signInManager = signInManager;
            _unitOfWork = unitOfWork;
            _DB = DB;
        }


        [HttpPost]
        [Route("Post")]
        public IActionResult Post(UserViewModel model)
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
                int OTP = GenerateRandomNo();

            }
            return StatusCode((int)response.Status, response.obj);
        }
        [HttpPost]
        [Route("SendOTP")]

        public IActionResult SendOTP(string? UserId, string? Mobile)
        {
            int OTP = GenerateRandomNo();
            if (SendSMS(OTP, Mobile))
            {
                var response1 = UserService.AddOtp(OTP, Mobile, UserId);
                return StatusCode((int)response1.Status, response1.obj);
            }
            return StatusCode((int)HttpStatusCode.BadRequest, "");
        }
        [HttpPost]
        [Route("VerifyOtp")]

        public IActionResult VerifyOtp(string? UserId, string? Number, string? OTP)
        {


            var response1 = UserService.VerifyOtp(UserId, Number, OTP);
            return StatusCode((int)response1.Status, response1.obj);
        }


        public int GenerateRandomNo()
        {
            int _min = 1000;
            int _max = 9999;
            Random _rdm = new Random();
            return _rdm.Next(_min, _max);
        }

        public bool SendSMS(int OtpCode, string ContactNumber)
        {

            var To = ContactNumber;
            var text = "Hye Verification Code is" + OtpCode;

            var commandText = "INSERT INTO InsertSms (Body, ToAddress, FromAddress, ChannelID,StatusID, DataCoding, CustomField1, CustomField2) VALUES (@body, @to, @FromAddress, @ChannelID,'SCHEDULED',0,@CustomField1, @CustomField2);";
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("OTPConnection:DefaultConnection")))
            {
                SqlCommand command = new SqlCommand(commandText, connection);
                command.Parameters.AddWithValue("@to", To);

                command.Parameters.AddWithValue("@body", text);

                command.Parameters.AddWithValue("@FromAddress", AppConfigs.SMS_DevSmsService_FromAddress);



                command.Parameters.AddWithValue("@ChannelId", AppConfigs.SMS_DevSmsService_ChannelId);
                var configvalue1 = this._configuration.GetValue<String>("SMS_DevSmsService_CustomField2_en");
                command.Parameters.AddWithValue("@CustomField1", this._configuration.GetValue<String>("SMS_DevSmsService_CustomField2_en"));

                command.Parameters.AddWithValue("@CustomField2", this._configuration.GetValue<String>("SMS_DevSmsService_CustomField2_en"));
                connection.Open();
                command.ExecuteNonQuery();
                return true;
            }
            return false;
        }
        [HttpPost]
        [Route("BackOfficeUserProfile")]

        public IActionResult BackOfficeUserProfile(UserViewModel model)
        {

            string TokenCookie = Request.Cookies["Key"];

            if (TokenCookie != null)
            {

            }

            FunctionResult result = IdentityHelper.BackOfficeUser(model);
            if (!result.success)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, result.message);
            }
            model.UserId = result.message;
            string Response = "Sucessfully Added";
            var response = UserService.AddBackOfficeUserProfile(model);
            return StatusCode((int)response.Status, response.Message);
        }
        [HttpPost]
        [Route("SystemUserProfile")]
        public async Task<IActionResult> SystemUserProfile(SystemUserModel model)
        {
            FunctionResult result = await IdentityHelper.SystemcreateUser(model);
            if (!result.success)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, result.message);
            }
            model.UserId = result.Respoinsemessage;

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
        public async Task<ActionResult> Login(AdminLoginViewModel model)
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
