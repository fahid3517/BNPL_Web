using Newtonsoft.Json;

namespace BNPL_Web.Notification.Models
{
   
        public class ResponseModel
        {
            [JsonProperty("isSuccess")]
            public bool IsSuccess { get; set; }
            [JsonProperty("message")]
            public string Message { get; set; }
        }
    
}
