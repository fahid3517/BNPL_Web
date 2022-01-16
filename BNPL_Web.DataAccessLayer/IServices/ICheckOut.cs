using BNPL_Web.Common.ViewModels;
using BNPL_Web.Common.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BNPL_Web.DataAccessLayer.IServices
{
    public interface ICheckOut
    {
             Task<ResponseViewModel> SendRequestAsync(
              HttpMethod httpMethod,
              CardPaymentRequest requestBody,
              string CardNumber,
              DateTime ExpireDate
              );
    } 
}
