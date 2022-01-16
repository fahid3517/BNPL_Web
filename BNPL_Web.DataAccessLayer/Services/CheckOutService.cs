using BNPL_Web.Common.ViewModels;
using BNPL_Web.Common.ViewModels.Common;
using BNPL_Web.DataAccessLayer.IServices;
using BNPL_Web.DatabaseModels.DbImplementation;
using BNPL_Web.DatabaseModels.DTOs;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace BNPL_Web.DataAccessLayer.Services
{
    public class CheckOutService : ICheckOut
    {
        private readonly HttpClient _httpClient;
        private readonly IUnitOfWork unitOfWork;
        public CheckOutService(HttpClient _httpClient, IUnitOfWork unitOfWork)
        {
            this._httpClient = _httpClient;
            this.unitOfWork = unitOfWork;
        }
        public async Task<ResponseViewModel> SendRequestAsync(HttpMethod httpMethod, CardPaymentRequest requestBody, string CardNumber, DateTime ExpireDate)
        {
            ResponseViewModel response = new ResponseViewModel();
            try
            {
                #region InitialLogs
                var logs = new LogsCheckout();
                logs.CivilId = "0c34ae5d-fbaa-4e3e-bc26-fc883a72ed9e";
                logs.CreatedAt = DateTime.Now;
                logs.DataObj = requestBody.ToString();
                logs.Type = "Card Verification Request";

                unitOfWork.LogsCheckout.Add(logs);
                unitOfWork.LogsCheckout.Commit();

                #endregion
                using (var client = new HttpRequestMessage())
                {
                    client.Headers.TryAddWithoutValidation("Authorization", "sk_test_adc5580d-5f3c-4e27-90f4-5de80c404629");
                    client.Method = HttpMethod.Post;
                    client.RequestUri = new Uri("https://api.sandbox.checkout.com/payments");
                    client.Content = new StringContent(System.Text.Json.JsonSerializer.Serialize(requestBody), System.Text.Encoding.UTF8,
                            MediaTypeNames.Application.Json);
                    var data = await _httpClient.SendAsync(client);
                    var check = await data.Content.ReadAsStringAsync();
                    CardPaymentResponse ActualData = JsonConvert.DeserializeObject<CardPaymentResponse>(check);

                    #region CustomerPaymentCards
                    var customerCards = new CustomerPaymentCards();

                    customerCards.CheckoutCustomerId = ActualData.customer.id;
                    customerCards.CivilId = "0c34ae5d-fbaa-4e3e-bc26-fc883a72ed9e";
                    customerCards.CardNumber = CardNumber;
                    customerCards.ExpiryDate = ExpireDate;
                    customerCards.Token = requestBody.source.token;
                    customerCards.IsDefault = true;

                    unitOfWork.CustomerPaymentCards.Add(customerCards);
                    #endregion

                    #region CustomerPaymentTansactions
                    var Payment = new CustomerPaymentTansactions();
                    Payment.CheckoutCustomerId = ActualData.customer.id;
                    Payment.CheckoutPaymentId = ActualData.id;
                    Payment.CheckoutActionId = ActualData.action_id;
                    Payment.ResponseCode = ActualData.response_code;
                    Payment.ResponseSummary = ActualData.response_summary;
                    Payment.Approved = ActualData.approved;
                    Payment.AuthCode = ActualData.auth_code;
                    Payment.Amount = ActualData.amount;
                    Payment.Currency = ActualData.currency;
                    Payment.Status = ActualData.status;

                    unitOfWork.CustomerPaymentTansactions.Add(Payment);
                    #endregion

                    #region FinalLog
                    var finallog = new LogsCheckout();
                    finallog.CivilId = "0c34ae5d-fbaa-4e3e-bc26-fc883a72ed9e";
                    finallog.CreatedAt = DateTime.Now;
                    finallog.DataObj = requestBody.ToString();
                    finallog.Type = "Card Verification Request";
                    finallog.Success = true;
                    finallog.DataObj = ActualData.ToString();

                    unitOfWork.LogsCheckout.Add(logs);
                    #endregion

                    unitOfWork.CustomerPaymentCards.Commit();

                    response.Status = HttpStatusCode.OK;
                    response.Message = "Sucessfully";
                    response.obj = "Sucessfully";
                    return response;
                }
            }
            catch (Exception ex)
            {
                #region ExceptionLog
                var Exceptiolog = new LogsCheckout();
                Exceptiolog.CivilId = "0c34ae5d-fbaa-4e3e-bc26-fc883a72ed9e";
                Exceptiolog.CreatedAt = DateTime.Now;
                Exceptiolog.DataObj = requestBody.ToString();
                Exceptiolog.Type = "Card Verification Request";
                Exceptiolog.Success = true;
                Exceptiolog.DataObj = ex.GetBaseException().Message;

                unitOfWork.LogsCheckout.Add(Exceptiolog);
                #endregion
                response.Status = HttpStatusCode.InternalServerError;
                response.obj = ex.Message;
                response.Message = ex.Message;
                return response;
            }
        }
    }
}
