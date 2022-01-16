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
        public async Task<IActionResult> SendReq(/*CardPaymentRequest model,string Cardnumber,string Cvv,DateTime ExpireDate*/)
        {
            CardPaymentRequest paymentRequest = new CardPaymentRequest();
            RequestSource source1 = new RequestSource();
            source1.type = "token";
            source1.token = "tok_as7kgchlfqte5owcfcw4gx2ya4";

            paymentRequest.source = source1;
            paymentRequest.currency = "USD";
            paymentRequest.amount = 0;
            string cardnumber = "";
            string Cvv = "";
            DateTime dater = DateTime.Now;
            try
            {
                var respnose = await service.SendRequestAsync(HttpMethod.Post, paymentRequest, cardnumber, dater);

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
