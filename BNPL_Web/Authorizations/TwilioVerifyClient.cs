using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;

namespace BNPL_Web.Authorizations
{
    public class TwilioVerifyClient
    {
        private readonly HttpClient _client;
        public TwilioVerifyClient(HttpClient client)
        {
            _client = client;
        }
        public async Task<TwilioSendVerificationCodeResponse> StartVerification(int countryCode, string phoneNumber)
        {
            try
            {
                var requestContent = new FormUrlEncodedContent(new[] {
               new KeyValuePair<string, string>("via", "sms"),
               new KeyValuePair<string, string>("country_code", countryCode.ToString()),
               new KeyValuePair<string, string>("phone_number", phoneNumber),
           });

                var response = await _client.PostAsync("protected/json/phones/verification/start", requestContent);

                var content = await response.Content.ReadAsStringAsync();

                // this will throw if the response is not valid
                return JsonConvert.DeserializeObject<TwilioSendVerificationCodeResponse>(content);
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public async Task<TwilioCheckCodeResponse> CheckVerificationCode(int countryCode, string phoneNumber, string verificationCode)
        {
            var queryParams = new Dictionary<string, string>()
           {
               {"country_code", countryCode.ToString()},
               {"phone_number", phoneNumber},
               {"verification_code", verificationCode },
           };

            var url = QueryHelpers.AddQueryString("protected/json/phones/verification/check", queryParams);

            var response = await _client.GetAsync(url);

            var content = await response.Content.ReadAsStringAsync();

            // this will throw if the response is not valid
            return JsonConvert.DeserializeObject<TwilioCheckCodeResponse>(content);
        }

    }
}
