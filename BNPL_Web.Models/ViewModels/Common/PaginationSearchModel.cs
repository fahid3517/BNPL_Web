using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BNPL_Web.Common.ViewModels.Common
{
   public class PaginationSearchModel
    {
        public string User { get; set; }
        public string Search { get; set; }
        public int PageStart { get; set; }
        public int PageSize { get; set; }
        public int Draw { get; set; }
        public string direction { get; set; }
        public int sorting { get; set; }
        public int Year { get; set; }
        public int month { get; set; }
        public int type { get; set; }
    }
}
