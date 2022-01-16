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
        public async Task<ResponseViewModel> CardVerificationRequestAsync(HttpMethod httpMethod, CardPaymentRequest requestBody, string CivilId, string CardNumber, DateTime ExpireDate)
        {
            ResponseViewModel response = new ResponseViewModel();
            try
            {
                #region InitialLogs
                var logs = new LogsCheckout();

                logs.CivilId = CivilId;
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
                            "application/json");
                    var data = await _httpClient.SendAsync(client);
                    if (data.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        response.Message = "UnAuthorized Request";
                        response.status = HttpStatusCode.Unauthorized;
                        return response;
                    }
                    if (data.StatusCode == HttpStatusCode.UnprocessableEntity)
                    {
                        response.Message = "UnProcessible Entity";
                        response.status = HttpStatusCode.UnprocessableEntity;
                        return response;
                    }
                    if (data.StatusCode == HttpStatusCode.TooManyRequests)
                    {
                        response.Message = "Too Many Request";
                        response.status = HttpStatusCode.TooManyRequests;
                        return response;
                    }
                    if (data.StatusCode == HttpStatusCode.BadGateway)
                    {
                        response.Message = "Bad Gateway Request";
                        response.status = HttpStatusCode.BadGateway;
                        return response;
                    }
                    var check = await data.Content.ReadAsStringAsync();
                  
                    CardPaymentResponse ActualData = JsonConvert.DeserializeObject<CardPaymentResponse>(check);

                    #region CustomerPaymentCards
                    var customerCards = new CustomerPaymentCards();

                    customerCards.CheckoutCustomerId = ActualData.customer.id;
                    customerCards.CivilId = CivilId;
                    customerCards.CardNumber = CardNumber;
                    customerCards.ExpiryDate = ExpireDate;
                    customerCards.Token = requestBody.source.token;
                    customerCards.IsDefault = true;

                    unitOfWork.CustomerPaymentCards.Add(customerCards);
                    unitOfWork.CustomerPaymentCards.Commit();
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
                    unitOfWork.CustomerPaymentTansactions.Commit();
                    #endregion

                    #region FinalLog
                    var finallog = new LogsCheckout();

                    finallog.CivilId = CivilId;
                    finallog.CreatedAt = DateTime.Now;
                    finallog.DataObj = check;
                    finallog.Type = "Card Verification Request";
                    unitOfWork.LogsCheckout.Add(finallog);
                    unitOfWork.LogsCheckout.Commit();
                    #endregion

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
                var check = ex.Message.ToString();
                Exceptiolog.CivilId = CivilId;
                Exceptiolog.CreatedAt = DateTime.Now;
                Exceptiolog.DataObj = check;
                Exceptiolog.Type = "Card Verification Request";
                Exceptiolog.Success = false;
                Exceptiolog.ExceptionObj = ex.GetBaseException().Message;

                unitOfWork.LogsCheckout.Add(Exceptiolog);
                unitOfWork.LogsCheckout.Commit();
                #endregion
                response.Status = HttpStatusCode.InternalServerError;
                response.obj = ex.Message;
                response.Message = ex.Message;
                return response;
            }
        }
        public ResponseViewModel GetAllCustomerCard(string CivilId)
        {
            ResponseViewModel response = new ResponseViewModel();
            try
            {
                List<GetCustomerCardViewModel> list = new List<GetCustomerCardViewModel>();
                var data = unitOfWork.CustomerPaymentCards.GetMany(x => x.CivilId == CivilId);
                foreach (var v in data)
                {
                    GetCustomerCardViewModel model = new GetCustomerCardViewModel();
                    model.CardNumber = v.CardNumber;
                    list.Add(model);
                }
                response.obj = list;
                response.Message = "Sucessfully Get All Cards";
                response.Status = HttpStatusCode.OK;

                return response;
            }
            catch (Exception ex)
            {
                response.Status = HttpStatusCode.InternalServerError;
                response.obj = ex.Message;
                response.Message = ex.Message;
                return response;
            }
        }
        public async Task<ResponseViewModel> CutomerPayment(string CardNumber, long Amount, string CivilId,string Currency)
        {
            ResponseViewModel response = new ResponseViewModel();
            try
            {
                var CustomerCardData = unitOfWork.CustomerPaymentCards.Get(x => x.CardNumber == CardNumber);
                if (CustomerCardData != null)
                {
                    Customer1 customer1 = new Customer1();

                    //customer1.id = "cus_hgvuhfnyd6de3cswvtagm2lzii";
                    var ParentCustomerData = unitOfWork.CustomerProfile.Get(x => x.CivilId == CivilId);
                    if (ParentCustomerData != null)
                    {
                        customer1.name = ParentCustomerData.FullName;
                        customer1.email = ParentCustomerData.Email;
                    }
                    else
                    {
                        response.Message = "CivilId Not Valid";
                        response.Status = HttpStatusCode.BadRequest;
                        return response;
                    }
                    #region InitialLogs
                    var logs = new LogsCheckout();

                    logs.CivilId = CivilId;
                    logs.CreatedAt = DateTime.Now;
                    logs.DataObj = CardNumber + "/" + Amount;
                    logs.Type = "Card Verification Request";

                    unitOfWork.LogsCheckout.Add(logs);
                    unitOfWork.LogsCheckout.Commit();
                    #endregion
                    using (var client = new HttpRequestMessage())
                    {
                        CardPaymentRequest request = new CardPaymentRequest();
                        RequestSource source = new RequestSource();
                        source.type = "token";
                        source.token = CustomerCardData.Token;
                        request.source = source;
                        request.currency = Currency;
                        request.amount = Amount;

                        
                        request.customer = customer1;
                        client.Headers.TryAddWithoutValidation("Authorization", "sk_test_adc5580d-5f3c-4e27-90f4-5de80c404629");
                        client.Method = HttpMethod.Post;
                        client.RequestUri = new Uri("https://api.sandbox.checkout.com/payments");
                        client.Content = new StringContent(System.Text.Json.JsonSerializer.Serialize(request), System.Text.Encoding.UTF8,
                                MediaTypeNames.Application.Json);
                        var data = await _httpClient.SendAsync(client);
                        if (data.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            response.Message = "UnAuthorized Request";
                            response.status = HttpStatusCode.Unauthorized;
                            return response;
                        }
                        if (data.StatusCode == HttpStatusCode.UnprocessableEntity)
                        {
                            response.Message = "UnProcessible Entity";
                            response.status = HttpStatusCode.UnprocessableEntity;
                            return response;
                        }
                        if (data.StatusCode == HttpStatusCode.TooManyRequests)
                        {
                            response.Message = "Too Many Request";
                            response.status = HttpStatusCode.TooManyRequests;
                            return response;
                        }
                        if (data.StatusCode == HttpStatusCode.BadGateway)
                        {
                            response.Message = "Bad Gateway Request";
                            response.status = HttpStatusCode.BadGateway;
                            return response;
                        }
                        var check = await data.Content.ReadAsStringAsync();

                        CardPaymentResponse ActualData = JsonConvert.DeserializeObject<CardPaymentResponse>(check);

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
                        unitOfWork.CustomerPaymentTansactions.Commit();
                        #endregion

                        #region FinalLog
                        var finallog = new LogsCheckout();

                        finallog.CivilId = CustomerCardData.CivilId;
                        finallog.CreatedAt = DateTime.Now;
                        finallog.DataObj = check;
                        finallog.Type = "Card Verification Request";
                        unitOfWork.LogsCheckout.Add(finallog);
                        unitOfWork.LogsCheckout.Commit();
                        #endregion

                        response.Status = HttpStatusCode.OK;
                        response.Message = "Sucessfully";
                        response.obj = "Sucessfully";
                        return response;
                    }
                }
                else
                {
                    response.Message = "Card not Verified";
                    response.Status = HttpStatusCode.BadRequest;
                    response.obj= "Card not Verified";
                    return response;
                }
            }
            catch (Exception ex)
            {
                response.Status = HttpStatusCode.InternalServerError;
                response.obj = ex.Message;
                response.Message = ex.Message;
                return response;
            }
        }
    }
}
