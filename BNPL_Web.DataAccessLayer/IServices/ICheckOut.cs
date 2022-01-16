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
        Task<ResponseViewModel> CardVerificationRequestAsync(
         HttpMethod httpMethod,
         CardPaymentRequest requestBody,
         string CivilId,
         string CardNumber,
         DateTime ExpireDate
         );
        ResponseViewModel GetAllCustomerCard(string CivilId);
        Task<ResponseViewModel> CutomerPayment(
        string cardnumber, long Amount,string CivilId,string Currency
         );
    }
}
