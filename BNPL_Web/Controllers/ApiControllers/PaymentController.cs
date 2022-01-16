using BNPL_Web.Common.ViewModels;
using BNPL_Web.DataAccessLayer.IServices;
using BNPL_Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestSharp;

namespace BNPL_Web.Controllers.ApiControllers
{
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
        [Route("SendReq")]
        public async Task<IActionResult> SendReq(CardPaymentRequest model)
        {
            //CardPaymentRequest paymentRequest = new CardPaymentRequest();
            //RequestSource source1 = new RequestSource();
            //source1.type = "token";
            //source1.token = "tok_x4rvacoh45gulkx4hbhddnaaey";

            //paymentRequest.source = source1;
            //paymentRequest.currency = "USD";
            //paymentRequest.amount = 0;
            try
            {
                var respnose = await service.SendRequestAsync(HttpMethod.Post, "https://api.sandbox.checkout.com/payments", model);

            }
            catch (Exception ex)
            {
                throw;
            }
            string data = "";
            return StatusCode(StatusCodes.Status100Continue, "");
        }
    }
}
