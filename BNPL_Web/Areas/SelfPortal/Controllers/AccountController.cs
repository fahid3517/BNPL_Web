using BNPL_Web.Authentications;
using BNPL_Web.Common.ViewModels;
using BNPL_Web.Common.ViewModels.Authorization;
using BNPL_Web.DataAccessLayer.Helpers;
using BNPL_Web.DataAccessLayer.IServices;
using BNPL_Web.DatabaseModels.Authentication;
using BNPL_Web.DatabaseModels.DbImplementation;
using BNPL_Web.DatabaseModels.DTOs;
using BNPL_Web.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace BNPL_Web.Areas.SelfPortal.Controllers
{
    [Area("SelfPortal")]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWork unitOfWork;
        private readonly IUserService _userService;
        public AccountController(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager,
            IConfiguration configuration, SignInManager<ApplicationUser> signInManager, IUserService userService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _signInManager = signInManager;
            this.unitOfWork = unitOfWork;
            _userService = userService;
        }

        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string? ReturnUrl)
        {

            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home", new { area = "SelfPortal" });
            else
            {
                ViewBag.ReturnUrl = ReturnUrl;
                return View();
            }
        }
        // POST: /Account/Login
        private ActionResult RedirectToLocal(string? ReturnUrl)
        {
            if (Url.IsLocalUrl(ReturnUrl))
            {
                return Redirect(ReturnUrl);
            }
            return RedirectToAction("Index", "Home", new { area = "SelfPortal" });
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string? ReturnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            string tokenKey = _configuration.GetValue<string>("Tokens:Key");
            string HashPassword = "";
            using (MD5 md5Hash = MD5.Create())
            {
                var bytes = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(model.Password));

                HashPassword = Convert.ToBase64String(bytes);
            }
            //appuser.PasswordHash = HashPassword;
            var pass = HashPassword;

           

            // This doesn't count login failures towards lockout only two factor authentication
            // To enable password failures to trigger lockout, change to shouldLockout: true
            bool isActive = true;
            if (!isActive)
            {
                ModelState.AddModelError(nameof(model.Password), "Inactive user login attempt.");
                return View(model);
            }
           
            var CaustomerData = unitOfWork.CustomerProfile.Get(x => x.CivilId == model.CivilId);
           
            if (CaustomerData == null)
            {
                ModelState.AddModelError(nameof(model.Password), "Inactive user login attempt.");
                return View(model);
            }
            if (CaustomerData.ContractNumber == null)
            {
                ViewBag.Validation = "Invalid";
                ViewBag.UserId = CaustomerData.UserId;
                /// ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + title + "', '" + body + "');", true);


                ModelState.AddModelError(nameof(model.Password), "Inactive user login attempt.");
                return View(model);
            }


            var result = unitOfWork.AspNetUser.Get(x => x.Email == CaustomerData.Email && x.PasswordHash == pass);
            // var result = await _signInManager.PasswordSignInAsync(model.Username, pass, true,false);

            if (result != null)
            {
                var UserRole = unitOfWork.UserProfile.Get(x => x.UserId == result.Id);
                ///var authToken = new Encryption().GetToken( tokenKey);
                var authToken = new Encryption().GetToken(new AdminAuthToken { UserId = result.Id,/*RoleId= UserRole.ProfileId,*/ UserName = result.UserName }, result.Id, tokenKey);

                CookieOptions cookieOptions = new CookieOptions();
                cookieOptions.Secure = true;
                cookieOptions.Expires = DateTime.Now.AddHours(2);
                Response.Cookies.Append("Key", authToken, cookieOptions);


                var ApplicationUser = unitOfWork.AspNetUser.Get(x => x.Email == CaustomerData.Email);
                if (ApplicationUser != null)
                {
                    ApplicationUser.FirstLogin = DateTime.Now;
                    ApplicationUser.LastLogin = DateTime.Now;
                    ApplicationUser.SuccessFullLogin = DateTime.Now;

                    unitOfWork.AspNetUser.Update(ApplicationUser);
                    unitOfWork.AspNetUser.Commit();

                }
                #region SMS
                //const string accountSid ="AC007478458b6b5c1e6ddff7980bb55130";
                //const string authTok1en = "c089399c224231bf782f81a20e745e84";

                //TwilioClient.Init(accountSid, authTok1en);

                //var message = MessageResource.Create(
                //    body: "All in the game, yo",
                //    from: new Twilio.Types.PhoneNumber("+923320487984"),
                //    to: new Twilio.Types.PhoneNumber("+923069882611")
                //);
                // Find your Account Sid and Auth Token at twilio.com/console

                //TwilioClient.Init("AC007478458b6b5c1e6ddff7980bb55130", "c089399c224231bf782f81a20e745e84");
                //var to = "+923320487984";
                //var message = MessageResource.Create(
                //    to,
                //    from:"+15005550006",
                //    body: "Ahoy!");

                //// Retrieve the status code of the last response from the HTTP client
                //var statusCode = TwilioClient.GetRestClient().HttpClient
                //    .LastResponse.StatusCode;
                #endregion

              
                return RedirectToAction("Customer", "Home", new { area = "SelfPortal" });
            }
            else
                ModelState.AddModelError(nameof(model.Password), "Invalid login attempt.");
            return View(model);
        }
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public ActionResult AccessDenied()
        {
            return Unauthorized();
        }
     
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult BackOfficeLogin(AdminLoginViewModel model, string? ReturnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            string tokenKey = _configuration.GetValue<string>("Tokens:Key");
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
                return View(model);
            }
            var result = unitOfWork.AspNetUser.Get(x => x.Email == model.Email && x.PasswordHash == pass);
            // var result = await _signInManager.PasswordSignInAsync(model.Username, pass, true,false);


            if (result != null)
            {
                var UserRole = unitOfWork.UserProfile.Get(x => x.UserId == result.Id);
                ///var authToken = new Encryption().GetToken( tokenKey);
                var authToken = new Encryption().GetToken(new AdminAuthToken { UserId = result.Id, RoleId = UserRole.ProfileId, UserName = result.UserName }, result.Id, tokenKey);

                CookieOptions cookieOptions = new CookieOptions();
                cookieOptions.Secure = true;
                cookieOptions.Expires = DateTime.Now.AddHours(2);
                Response.Cookies.Append("Key", authToken, cookieOptions);


                var ApplicationUser = unitOfWork.AspNetUser.Get(x => x.Email == model.Email);
                if (ApplicationUser != null)
                {
                    ApplicationUser.FirstLogin = DateTime.Now;
                    ApplicationUser.LastLogin = DateTime.Now;
                    ApplicationUser.SuccessFullLogin = DateTime.Now;

                    unitOfWork.AspNetUser.Update(ApplicationUser);
                    unitOfWork.AspNetUser.Commit();

                }
                return RedirectToAction("Index", "Home", new { area = "SelfPortal" });
            }
            else
                ModelState.AddModelError(nameof(model.Password), "Invalid login attempt.");
            return View(model);
        }
        [AllowAnonymous]
        public ActionResult BackOfficeLogin(string? ReturnUrl)
        {

            //if (User.Identity.IsAuthenticated)
            //    return RedirectToAction("Index", "Home", new { area = "SelfPortal" });
            //else
            //{
            //    ViewBag.ReturnUrl = ReturnUrl;
            return View();
            //}
        }

    }
}
