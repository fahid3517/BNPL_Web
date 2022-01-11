using System.Configuration;
using System.Net;
using BNPL_Web.Authentications;
using BNPL_Web.Authorizations;
using BNPL_Web.Common.ViewModels;
using BNPL_Web.Common.ViewModels.Common;
using BNPL_Web.DataAccessLayer.IServices;
using BNPL_Web.DatabaseModels.Authentication;
using BNPL_Web.DatabaseModels.DbImplementation;
using BNPL_Web.DatabaseModels.DTOs;
//using BNPL_Web.DatabaseModels.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace BNPL_Web.DataAccessLayer.Services
{
    public class UserService : IUserService
    {

        public IConfiguration _Configuration;

        public readonly IUnitOfWork unitOfWork;
        // private readonly BNPL_Context _db;
        public UserService(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            this.unitOfWork = unitOfWork;
            _Configuration = configuration;
        }
        public ResponseViewModel Add(UserViewModel value)
        {
            ResponseViewModel response = new ResponseViewModel();
            try
            {
                var CheckData = unitOfWork.CustomerProfile.Get(x => x.CivilId == value.CivilId);
                if (CheckData != null)
                {
                    response.Message = "Already Exist CivilId";
                    response.Status = HttpStatusCode.BadRequest;
                    response.obj = "Already Exist CivilId";
                    return response;
                }

                CustomerProfile data = new CustomerProfile();

                data.UserId = value.UserId.ToString();
                data.RoleId = "dfdffa39-3048-447a-f78c-08d9d408f6dc";
                data.FirstNameAr = value.FirstNameAr;
                data.MiddleNameAr = value.LastNameAr;
                data.LastNameAr = value.LastNameAr;
                data.FirstNameEn = value.FirstNameEn;
                data.LastNameEn = value.LastNameEn;
                data.MiddleNameEn = value.MiddlelNameEn;

                data.Language = value.Language;
                data.Gender = value.Gender;
                data.DateOfBirth = value.DateOfBirth;
                data.Email = value.Email;
                data.Titile = value.Title;
                data.CivilId = value.CivilId;

                unitOfWork.CustomerProfile.Add(data);
                unitOfWork.CustomerProfile.Commit();

                response.Message = "Successfully Added";
                response.obj = data;
                response.status = HttpStatusCode.OK;
                return response;
            }
            catch (Exception ex)
            {
                response.Status = HttpStatusCode.InternalServerError;
                response.obj = ex.Message;
                response.Message = ex.Message;
                return response;
            }
        }

        public ResponseViewModel AddBackOfficeUserProfile(UserViewModel value)
        {
            ResponseViewModel response = new ResponseViewModel();
            try
            {
                AspNetMembership data = new AspNetMembership();

                data.UserId = Guid.Parse(value.UserId.ToString());
                data.Email = value.Email;
                data.UserName = value.UserName;
                data.CreatedAt = DateTime.Now;

                unitOfWork.AspNetMembership.Add(data);
                unitOfWork.AspNetMembership.Commit();

                response.Message = "Successfully Added";
                response.obj = "Successfully Added";
                response.status = HttpStatusCode.OK;
                return response;
            }
            catch (Exception ex)
            {
                response.Status = HttpStatusCode.InternalServerError;
                response.obj = ex.Message;
                response.Message = ex.Message;
                return response;
            }
        }

        public ResponseViewModel SystemUserProfile(SystemUserModel value)
        {
            ResponseViewModel response = new ResponseViewModel();
            try
            {
                SystemUser data = new SystemUser();

                data.UserId = (Guid)value.UserId;

                //data.FullName = value.UserName;
                //data.RoleId = value.RoleId;
                //unitOfWork.SystemUsersProfile.Add(data);
                //unitOfWork.SystemUsersProfile.Commit();

                response.Message = "Successfully Added";
                response.obj = "Successfully Added";
                response.status = HttpStatusCode.OK;
                return response;
            }
            catch (Exception ex)
            {
                response.Status = HttpStatusCode.InternalServerError;
                response.obj = ex.Message;
                response.Message = ex.Message;
                return response;
            }
        }

        public PaginatedRecordModel<UserViewModel> GetPaginatedRecords(PaginationSearchModel model)


        {
            UserViewModel model1 = new UserViewModel();
            int recordsFiltered;
            int totalRecords;
            int pageNo = model.PageStart / model.PageSize;

            //Get the Sorting column
            Func<UserViewModel, Int32, string> getColName = (
                (t, iSortCol) =>
                  iSortCol == 1 ? t.UserName.ToString() :
                  iSortCol == 2 ? t.RoleId.ToString() :
                  iSortCol == 3 ? t.Email == null ? "N/A" : t.Email.ToString() :
                  t.UserId.ToString()
            );

            IEnumerable<UserViewModel> data = unitOfWork.AspNetUser.GetAll().Select(p => new UserViewModel()
            {
                UserName = p.UserName,
                Email = p.Email == null ? "N/A" : p.Email,
                RoleId = GetRoleName(p.Id),
            });



            totalRecords = data.Count();
            if (!string.IsNullOrEmpty(model.Search))
            {

                // In case of by default
                data = data.Where(x => x.UserName.ToUpper().Contains(model.Search.ToUpper())
                || x.RoleId.ToUpper().Contains(model.Search.ToUpper()) || x.Email.ToUpper().Contains(model.Search.ToUpper())

              );
            }

            recordsFiltered = data.Count();
            if (model.direction.Contains("asc"))
            {
                //data = data
                //.OrderBy(x => getColName(x, model.sorting))
                //.Skip(pageNo * model.PageSize)
                //.Take(model.PageSize);
            }
            else
            {
                //data = data
                //.OrderByDescending(x => getColName(x, model.sorting))
                //.Skip(pageNo * model.PageSize)
                //.Take(model.PageSize);
            }

            var dataObject = new PaginatedRecordModel<UserViewModel>();
            dataObject.draw = model.Draw;
            dataObject.data = data;
            dataObject.recordsTotal = totalRecords;
            dataObject.recordsFiltered = recordsFiltered;
            return dataObject;
        }
        public string GetRoleName(string UserId)
        {
            string RoleName = "";
            var UserData = unitOfWork.UserProfile.Get(x => x.UserId == UserId);
            if (UserData != null)
            {
                var role1 = unitOfWork.AspNetProfile.Get(x => x.Id == Guid.Parse(UserData.ProfileId.ToString()));

                if (role1 != null)
                {

                    return RoleName = role1.ProfileName;
                }

            }
            return RoleName;
        }
        public int GenerateRandomNo()
        {
            int _min = 1000;
            int _max = 9999;
            Random _rdm = new Random();
            return _rdm.Next(_min, _max);
        }

        public ResponseViewModel AddOtp(int OTP, string ContactNo, string UserId)
        {
            ResponseViewModel response = new ResponseViewModel();
            try
            {
                var OTPVerfi = new OTPVerification();

                OTPVerfi.Type = "Register OTP";
                OTPVerfi.PhoneNumber = ContactNo;
                OTPVerfi.UserId = UserId;
                OTPVerfi.Code = OTP.ToString();

                //var userTable = unitOfWork.CustomerProfile.Get(x => x.UserId == UserId);
                //userTable.ContractNumber = ContactNo;


                //unitOfWork.CustomerProfile.Update(userTable);

                unitOfWork.OTPVerification.Add(OTPVerfi);
                unitOfWork.OTPVerification.Commit();

                response.Message = "";
                response.obj = OTPVerfi.Code;
                response.status = HttpStatusCode.OK;
                return response;
            }
            catch (Exception ex)
            {
                response.Status = HttpStatusCode.InternalServerError;
                response.obj = ex.Message;
                response.Message = ex.Message;
                return response;
            }
        }

        public ResponseViewModel VerifyOtp(string UserId, string ContactNo, string OTP)
        {
            ResponseViewModel response = new ResponseViewModel();
            try
            {

                var CheckOtp = unitOfWork.OTPVerification.Get(x => x.UserId == UserId && x.Code == OTP && x.PhoneNumber == ContactNo);


                if (CheckOtp == null)
                {
                    response.Status = HttpStatusCode.BadRequest;
                    response.obj = "Not Valid Otp";
                    response.Message = "Not Valid Otp";
                    return response;
                }

                var User = unitOfWork.CustomerProfile.Get(x => x.UserId == UserId);
                if (User != null)
                {
                    User.ContractNumber = ContactNo;
                    unitOfWork.CustomerProfile.Update(User);
                    unitOfWork.CustomerProfile.Commit();
                }
                response.Message = "";
                response.obj = "";
                response.status = HttpStatusCode.OK;
                return response;

            }
            catch (Exception ex)
            {
                response.Status = HttpStatusCode.InternalServerError;
                response.obj = ex.Message;
                response.Message = ex.Message;
                return response;
            }
        }
    }
}
