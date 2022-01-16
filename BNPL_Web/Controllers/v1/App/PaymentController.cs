using BNPL_Web.Common.ViewModels;
using BNPL_Web.DataAccessLayer.IServices;
using BNPL_Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestSharp;

namespace BNPL_Web.Controllers.v1.App
{
    //[ApiVersion("1")]
    //[Route("api/v{version:apiVersion}/[controller]")]
    [Route("api/Payment")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly ICheckOut service;
        public PaymentController(IServiceProvider provider)
        {
            service = (ICheckOut)provider.GetService(typeof(ICheckOut));

        }
        [HttpPost]
        [Route("CustomerCardVerification")]
        public async Task<IActionResult> CustomerCardVerification(/*CardPaymentRequest model,string CivilId,string cardNumber,DateTime ExpireDate*/)
        {
            CardPaymentRequest model = new CardPaymentRequest();

            RequestSource source1 = new RequestSource();
            source1.type = "token";
            source1.token = "tok_jpv6ycwxp5wepbct7dpzvuzrrq";
            model.source=source1;
            model.currency = "USD";
            model.amount = 0;
            try
            {
                var respnose = await service.CardVerificationRequestAsync(HttpMethod.Post, model,"", "", DateTime.Now);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "");
            }
            return StatusCode(StatusCodes.Status201Created, "");
        }
        [HttpGet]
        [Route("GetAllCustomerCard")]
        public IActionResult GetAllCustomerCard(string CivilId)
        {
            try
            {
                var respnose = service.GetAllCustomerCard(CivilId);
            }
            catch (Exception ex)
            {
                var response = ex.Message;
                return StatusCode(StatusCodes.Status400BadRequest, response);
            }
            return StatusCode(StatusCodes.Status201Created, "");
        }
        [HttpPost]
        [Route("CustomerPayment")]
        public IActionResult CustomerPayment(string Cardnumber, long Amount,string CivilId)
        {
            try
            {
                var respnose = service.CutomerPayment(Cardnumber,Amount,CivilId);
            }
            catch (Exception ex)
            {
                var response = ex.Message;
                return StatusCode(StatusCodes.Status400BadRequest, response);
            }
            return StatusCode(StatusCodes.Status201Created, "");
        }
    }
}
