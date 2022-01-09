using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BNPL_Web.Common.ViewModels
{
    public class AdminLoginResponse
    {
        [JsonProperty(PropertyName = "accessToken")]
        public string AccessToken { get; set; } = "";
    }
}
