using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BNPL_Web.Common.ViewModels.Common
{
    public class Response<T>
    {
        [JsonProperty(PropertyName = "isError")]
        public bool IsError { get; set; }

        [JsonProperty(PropertyName = "message")]
        public string Message { get; set; }

        [JsonProperty(PropertyName = "exception")]
        public string Exception { get; set; } = "";

        [JsonProperty(PropertyName = "data")]
        public T Data { get; set; }
    }
}
