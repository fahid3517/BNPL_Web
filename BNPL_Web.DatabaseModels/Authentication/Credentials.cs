using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BNPL_Web.DatabaseModels.Authentication
{
    public class Credentials
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        public Credentials(string username, string password)
        {
            this.UserName = username;
            this.Password = password;
        }

        public Credentials()
        {
            this.UserName = string.Empty;
            this.Password = string.Empty;
        }
    }
}
