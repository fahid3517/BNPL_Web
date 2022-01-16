using BNPL_Web.Common.ViewModels;
using BNPL_Web.Common.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BNPL_Web.DataAccessLayer.IServices
{
    public interface IUserService
    {
        ResponseViewModel Add(CustomerViewModel value);
        ResponseViewModel AddBackOfficeUserProfile(UserViewModel value);
        ResponseViewModel SystemUserProfile(SystemUserModel value);
        PaginatedRecordModel<UserViewModel> GetPaginatedRecords(PaginationSearchModel model);
        //bool SendSMS();
        ResponseViewModel AddOtp(int OTP,string ContactNo,string UserId);
        ResponseViewModel VerifyOtp(string UserId, string ContactNo, string OTP);
        
    }
}
