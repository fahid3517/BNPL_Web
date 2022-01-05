using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using BNPL_Web.Authentications;
using BNPL_Web.Common.ViewModels;
using BNPL_Web.Common.ViewModels.Authorization;
using BNPL_Web.Common.ViewModels.Common;
using BNPL_Web.DataAccessLayer.IServices;
using BNPL_Web.DatabaseModels.DbImplementation;
using BNPL_Web.DatabaseModels.DTOs;

namespace BNPL_Web.DataAccessLayer.Services
{
    public class RolesService : IRoleService
    {
        public IUnitOfWork unitOfWork { get; set; }
        public RolesService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public ResponseViewModel Activate(RolesViewModel value)
        {
            throw new NotImplementedException();
        }

        public ResponseViewModel Add(RolesViewModel value)
        {
            ResponseViewModel response = new ResponseViewModel();
            try
            {
                string message = "";
                if (!value.Id.Equals(""))
                {
                    var role = unitOfWork.AspNetRole.GetMany(a => a.Id == value.Id).FirstOrDefault();
                    List<RolePrivilages> priviliges = unitOfWork.RolePrivilages.GetMany(p => p.RoleId == role.Id).ToList();

                    foreach (var privilege in priviliges)
                    {
                        unitOfWork.RolePrivilages.Delete(privilege);
                    }

                    unitOfWork.RolePrivilages.Commit();

                    foreach (var privilege in role.DbRolePrivileges.ToList())
                    {
                        unitOfWork.RolePrivilages.Delete(privilege);
                    }


                    foreach (var privilege in value.allPrivelages)
                    {
                        long id = 0;
                        long.TryParse(privilege.Value, out id);
                        Privilages a = unitOfWork.Privilages.GetById(id);
                        var temp = new RolePrivilages()
                        {
                            PrivilegeId = a.Id,
                            RoleId = role.Id
                        };
                        unitOfWork.RolePrivilages.Add(temp);
                    }
                    message = "Role Updated Successfully.";
                    unitOfWork.RolePrivilages.Commit();


                }
                else if (value.Id.Equals(""))
                {
                    var role = new ApplicationRole
                    {
                        Name = value.RoleName,
                        NormalizedName = value.RoleName.ToUpper(),
                        Id = Guid.NewGuid().ToString(),
                    };


                    foreach (var privilege in value.allPrivelages)
                    {
                        long id = 0;
                        long.TryParse(privilege.Value, out id);
                        Privilages a = unitOfWork.Privilages.GetById(id);
                        var temp = new RolePrivilages()
                        {
                            PrivilegeId = a.Id,
                            RoleId = role.Id
                        };
                        role.DbRolePrivileges.Add(temp);
                    }
                    message = "Role Added Successfully.";
                    unitOfWork.AspNetRole.Add(role);
                    unitOfWork.AspNetRole.Commit();
                }

                response.Status = HttpStatusCode.OK;
                response.Message = message;

                return response;

            }
            catch (Exception ex)
            {

                if (ex.GetBaseException().Message.Contains("Cannot insert duplicate key"))
                {
                    response.Status = HttpStatusCode.BadRequest;
                    response.Message = "Role Name Already Exists";
                    response.obj = "Role Name Already Exists";
                    return response;
                }
                response.Status = HttpStatusCode.BadRequest;
                response.Message = ex.GetBaseException().Message;
                response.obj = ex.GetBaseException().Message;
                return response;
            }
        }

        public ResponseViewModel AssignViewsToRole(AssignPrivilegesViewModel[] value)
        {
            throw new NotImplementedException();
        }

        public ResponseViewModel Delete(string Id)
        {
            throw new NotImplementedException();
        }

        public ResponseViewModel GetAllPrivilegeAndRole()
        {
            throw new NotImplementedException();
        }

        public ResponseViewModel GetAllRole()
        {
            throw new NotImplementedException();
        }

        public ResponseViewModel GetAssignPrivilegeByRoleId(string id)
        {
            throw new NotImplementedException();
        }

        public ResponseViewModel GetById(int id)
        {
            throw new NotImplementedException();
        }

        public ResponseViewModel Update(RolesViewModel value)
        {
            throw new NotImplementedException();
        }
    }
}
