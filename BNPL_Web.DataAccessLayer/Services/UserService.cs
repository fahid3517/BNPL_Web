using System.Net;
using BNPL_Web.Authentications;
using BNPL_Web.Common.ViewModels;
using BNPL_Web.Common.ViewModels.Common;
using BNPL_Web.DataAccessLayer.IServices;
using BNPL_Web.DatabaseModels.Authentication;
using BNPL_Web.DatabaseModels.DbImplementation;
using BNPL_Web.DatabaseModels.DTOs;
using BNPL_Web.DatabaseModels.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace BNPL_Web.DataAccessLayer.Services
{
    public class UserService : IUserService
    {
        public IUnitOfWork unitOfWork { get; set; }
        private readonly BNPL_Context _db;
        public UserService(IUnitOfWork unitOfWork, BNPL_Context _db)
        {
            this.unitOfWork = unitOfWork;
            this._db = _db;
        }
        public ResponseViewModel Add(UserViewModel value)
        {
            ResponseViewModel response = new ResponseViewModel();
            try
            {
                CustomerProfile data = new CustomerProfile();

                data.UserId = value.UserId;
                data.RoleId = value.RoleId;
                data.FullName = value.UserName;
                data.DateOfBirth = value.DateOfBirth;

                unitOfWork.CustomerProfile.Add(data);
                unitOfWork.CustomerProfile.Commit();

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

        public ResponseViewModel AddBackOfficeUserProfile(UserViewModel value)
        {
            ResponseViewModel response = new ResponseViewModel();
            try
            {
                BackOfficeUserProfile data = new BackOfficeUserProfile();

                data.UserId = value.UserId;
                data.RoleId = value.RoleId;
                data.FullName = value.UserName;

                unitOfWork.BackOfficeUserProfile.Add(data);
                unitOfWork.BackOfficeUserProfile.Commit();

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

        public ResponseViewModel SystemUserProfile(UserViewModel value)
        {
            ResponseViewModel response = new ResponseViewModel();
            try
            {
                SystemUsersProfile data = new SystemUsersProfile();

                data.UserId = value.UserId;

                data.FullName = value.UserName;
                data.RoleId = value.RoleId;
                unitOfWork.SystemUsersProfile.Add(data);
                unitOfWork.SystemUsersProfile.Commit();

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
                RoleId = GetRoleName(p.UserName),
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
        public string GetRoleName(string UserName)
        {
            string RoleName = "";
            var UserData = unitOfWork.AspNetUser.Get(x => x.UserName == UserName);
            if (UserData != null)
            {
                ApplicationUserRole role1 = unitOfWork.AspNetUserRole.Get(x => x.UserId == UserData.Id);
                var Result = _db.UserRoles.Where(x => x.UserId == UserData.Id).FirstOrDefault();
                if (Result != null)
                {
                    var role = unitOfWork.AspNetRole.Get(x => x.Id == Result.RoleId);
                    if(role!=null)
                        return RoleName = role.Name;
                }

            }
           
            var result = "";
            return RoleName;
        }
    }
}
