using BNPL_Web.Common.ViewModels;
using BNPL_Web.Common.ViewModels.Common;
using BNPL_Web.DataAccessLayer.IServices;
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
        public CheckOutService(HttpClient _httpClient)
        {
            this._httpClient = _httpClient;
        }
        public async Task<ResponseViewModel> SendRequestAsync(HttpMethod httpMethod, string path, object requestBody)
        {
            ResponseViewModel response = new ResponseViewModel();
            try
            {
                using (var client = new HttpRequestMessage())
                {
                    client.Headers.TryAddWithoutValidation("Authorization", "sk_test_adc5580d-5f3c-4e27-90f4-5de80c404629");
                    client.Method = HttpMethod.Post;
                    client.RequestUri = new Uri(path);
                    client.Content = new StringContent(System.Text.Json.JsonSerializer.Serialize(requestBody), System.Text.Encoding.UTF8,
                            MediaTypeNames.Application.Json);
                    var data = await _httpClient.SendAsync(client);
                    var check = await data.Content.ReadAsStringAsync();
                    CardPaymentResponse ActualData = JsonConvert.DeserializeObject<CardPaymentResponse>(check);

                    response.status = data.StatusCode;
                    response.Message = "Sucessfully";
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
