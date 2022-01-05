using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BNPL_Web.Common.ViewModels.Base
{
    public class BaseViewModel
    {
        public int ?Id { get; set; }
        public string ?Code { get; set; }
        public string? Name { get; set; }
        public bool ?ValidFlag { get; set; }
        public string ?ValidFlagDescription { get; set; }
        public string ?StatusDescription { get; set; }
        public DateTime? ChangedOn { get; set; }
        public string ?ChangedBy { get; set; }

    }
}
