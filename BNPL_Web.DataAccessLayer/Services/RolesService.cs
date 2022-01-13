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
using BNPL_Web.DatabaseModels.Models;

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
            //unitOfWork.AspNetProfileRole.Get(x =>x.role)
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
                    var role = unitOfWork.AspNetProfile.GetMany(a => a.Id.ToString() == value.Id).FirstOrDefault();
                    List<AspNetProfileRole> priviliges = unitOfWork.AspNetProfileRole.GetMany(p => p.RoleId.ToString() == role.Id.ToString()).ToList();

                    foreach (var privilege in priviliges)
                    {
                        unitOfWork.AspNetProfileRole.Delete(privilege);
                    }

                    unitOfWork.AspNetProfileRole.Commit();

                    foreach (var privilege in value.allPrivelages)
                    {
                        long id = 0;
                        long.TryParse(privilege.Value, out id);
                        AspNetRole a = unitOfWork.AspNetRole.GetById(id);
                        var temp = new AspNetProfileRole()
                        {
                            ProfileId = a.Id,
                            RoleId = role.Id,

                        };
                        unitOfWork.AspNetProfileRole.Add(temp);
                    }
                    message = "Role Updated Successfully.";
                    unitOfWork.AspNetProfileRole.Commit();


                }
                else if (value.Id.Equals(""))
                {
                    AspNetProfile role = new AspNetProfile();

                    role.ProfileName = value.RoleName;
                    role.Description = value.RoleName.ToUpper();

                    unitOfWork.AspNetProfile.Add(role);
                    unitOfWork.AspNetProfile.Commit();

                    foreach (var privilege in value.allPrivelages)
                    {
                        long id = 0;
                        long.TryParse(privilege.Value, out id);
                        AspNetRole a = unitOfWork.AspNetRole.GetById(id);
                        var temp = new AspNetProfileRole()
                        {
                            ProfileId = a.Id,
                            RoleId = role.Id,
                        };
                        unitOfWork.AspNetProfileRole.Add(temp);
                    }
                    message = "Role Added Successfully.";
                    unitOfWork.AspNetProfile.Commit();
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
            return response;

        }

        public ResponseViewModel AssignViewsToRole(AssignPrivilegesViewModel[] value)
        {
            ResponseViewModel response = new ResponseViewModel();

            try
            {
                Guid RoleId = (Guid)value[0].RoleId;
                List<AspNetProfileRole> _data = unitOfWork.AspNetProfileRole.GetMany(p => p.RoleId == RoleId).ToList();
                if (_data.Count() > 0)
                {
                    foreach (var data in _data)
                    {
                        unitOfWork.AspNetProfileRole.Delete(data);
                    }
                }

                for (int i = 0; i < value.Length; i++)
                {
                    AspNetProfileRole db_role_privileges = new AspNetProfileRole();

                    db_role_privileges.RoleId = (Guid)value[i].RoleId;
                    db_role_privileges.ProfileId = value[i].PrivilegeId;

                    unitOfWork.AspNetProfileRole.Add(db_role_privileges);

                }
                unitOfWork.AspNetProfileRole.Commit();
                response.Status = HttpStatusCode.Created;
                response.Message = "Privileges Assign Successfully!";
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

        public ResponseViewModel Delete(Guid Id)
        {

            ResponseViewModel response = new ResponseViewModel();
            try
            {

                var role = unitOfWork.AspNetProfile.GetMany(a => a.Id.ToString() == Id.ToString()).FirstOrDefault();
                if (role != null)
                {
                    var role_Priviliges = unitOfWork.AspNetProfileRole.GetMany(x => x.RoleId == Guid.Parse(role.Id.ToString()));
                    foreach (var privilege in role_Priviliges)
                    {
                        unitOfWork.AspNetProfileRole.Delete(privilege);

                    }
                    unitOfWork.AspNetProfileRole.Commit();
                    unitOfWork.AspNetProfile.Delete(role);
                    unitOfWork.AspNetProfile.Commit();
                }
                else
                {
                    response.Status = HttpStatusCode.BadRequest;
                    response.Message = "Role Not Found.";
                    response.obj = "Role Not Found";

                    return response;
                }

                response.Status = HttpStatusCode.OK;
                response.Message = "Role Deleted Successfully.";
                response.obj = "Role Deleted Successfully";

                return response;

            }
            catch (Exception ex)
            {

                response.Status = HttpStatusCode.BadRequest;
                response.Message = ex.GetBaseException().Message;
                return response;
            }
        }

        public ResponseViewModel GetAllPrivilegeAndRole()
        {
            ResponseViewModel response = new ResponseViewModel();
            AssignPrivilegesViewModel model = new AssignPrivilegesViewModel();
            IEnumerable<RolesModel> Roles = unitOfWork.AspNetProfile.GetAll().Select(p => new RolesModel()
            {
                RoleId = p.Id,
                Name = p.ProfileName
            });

            IEnumerable<PrivilegesModel> Privileges = unitOfWork.AspNetRole.GetAll().Select(p => new PrivilegesModel()
            {
                PrivilegeId = p.Id,
                Name = p.Privilege,
                Category = p.Category,
                Portal = p.Portal
            });


            model.Roles = Roles;
            model.Privileges = Privileges;

            response.Status = HttpStatusCode.OK;
            response.obj = model;
            return response;
        }
        public ResponseViewModel GetAllRole()
        {
            ResponseViewModel response = new ResponseViewModel();
            try
            {

                IEnumerable<RolesModel> Roles = unitOfWork.AspNetProfile.GetAll().Select(p => new RolesModel()
                {
                    Id = p.Id.ToString(),
                    Name = p.ProfileName
                }).ToList();
                response.Status = HttpStatusCode.OK;
                response.obj = Roles;
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
        public ResponseViewModel GetAssignPrivilegeByRoleId(string id)
        {

            ResponseViewModel response = new ResponseViewModel();
            try
            {
                var data = unitOfWork.AspNetProfileRole.GetAll();
                IEnumerable<AssignPrivilegesViewModel> _data = unitOfWork.AspNetProfileRole.GetMany(p => p.RoleId == Guid.Parse(id.ToString()), "Profile").Select(p => new AssignPrivilegesViewModel()
                {
                    RoleId = p.RoleId,
                    PrivilegeId =p.ProfileId,
                    Category = p.Profile.Category,
                    Portal = "Main"
                }).ToList();
                response.Status = HttpStatusCode.OK;
                response.obj = _data;
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

        public ResponseViewModel GetById(int id)
        {
            throw new NotImplementedException();
        }

        public ResponseViewModel Update(RolesViewModel value)
        {
            ResponseViewModel response = new ResponseViewModel();
            try
            {
                var _dbvalue = unitOfWork.AspNetProfile.Get(p => p.Id.ToString() == value.Id);
                if (_dbvalue == null)
                {
                    response.Status = HttpStatusCode.BadRequest;
                    response.obj = "Not Exist";
                    response.Message = "Not Exist";
                    return response;
                }

                _dbvalue.ProfileName = value.RoleName;

                unitOfWork.AspNetProfile.Update(_dbvalue);
                unitOfWork.AspNetProfile.Commit();

                response.Status = HttpStatusCode.OK;
                response.Message = "Updated";


                return response;

            }
            catch (Exception ex)
            {

                if (((ex.InnerException).InnerException).Message.Contains("Cannot insert duplicate key"))
                {
                    response.Status = HttpStatusCode.NotFound;
                    response.Message = "Role Name Already Exists";

                    return response;
                }
                response.Status = HttpStatusCode.NotFound;
                response.Message = ex.Message;
                return response;
            }
        }
    }
}
