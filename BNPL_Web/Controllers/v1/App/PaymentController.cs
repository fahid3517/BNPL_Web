using BNPL_Web.Common.ViewModels;
using BNPL_Web.DataAccessLayer.IServices;
using BNPL_Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestSharp;

namespace BNPL_Web.Controllers.v1.App
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    //[Route("api/Payment")]
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
        public async Task<IActionResult> CustomerCardVerification(CardVerificationRequest model1)
        {

            CardPaymentRequest model = new CardPaymentRequest();
            RequestSource source1 = new RequestSource();
            source1.type = "token";
            source1.token = model1.Token;
            model.source=source1;
            model.currency = "USD";
            model.amount = 0;
            try
            {
                var respnose = await service.CardVerificationRequestAsync(HttpMethod.Post, model, model1.CivilId, model1.CardNumber, DateTime.Now);
                return StatusCode((int)respnose.Status, respnose.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
           
        }
        [HttpGet]
        [Route("GetAllCustomerCard")]
        public IActionResult GetAllCustomerCard(string CivilId)
        {
            try
            {
                var respnose = service.GetAllCustomerCard(CivilId);
                return StatusCode((int)respnose.Status, respnose.obj);
            }
            catch (Exception ex)
            {
                var response = ex.Message;
                return StatusCode(StatusCodes.Status400BadRequest, response);
            }
            
        }
        [HttpPost]
        [Route("CustomerPayment")]
        public IActionResult CustomerPayment(CustomerPaymentRequest model)
        {
            try
            {
                var respnose = service.CutomerPayment(model.Cardnumber, (long)model.Amount,model.CivilId,model.Currency);
                return StatusCode((int)respnose.Status, respnose.Result);
            }
            catch (Exception ex)
            {
                var response = ex.Message;
                return StatusCode(StatusCodes.Status400BadRequest, response);
            }
        }
    }
}
