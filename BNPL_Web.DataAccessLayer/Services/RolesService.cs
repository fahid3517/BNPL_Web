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
                    var role = unitOfWork.AspNetProfile.GetMany(a => a.ProfileId.ToString() == value.Id).FirstOrDefault();
                    List<AspNetProfileRoles> priviliges = unitOfWork.AspNetProfileRoles.GetMany(p => p.RoleId == role.ProfileId.ToString()).ToList();

                    foreach (var privilege in priviliges)
                    {
                        unitOfWork.AspNetProfileRoles.Delete(privilege);
                    }

                    unitOfWork.AspNetProfileRoles.Commit();

                    foreach (var privilege in value.allPrivelages)
                    {
                        long id = 0;
                        long.TryParse(privilege.Value, out id);
                        AspNetRoles a = unitOfWork.AspNetRoles.GetById(id);
                        var temp = new AspNetProfileRoles()
                        {
                            ProfileId = a.Id.ToString(),
                            RoleId = role.ProfileId.ToString(), 
                        };
                        unitOfWork.AspNetProfileRoles.Add(temp);
                    }
                    message = "Role Updated Successfully.";
                    unitOfWork.AspNetProfileRoles.Commit();


                }
                else if (value.Id.Equals(""))
                {
                    AspNetProfile role = new AspNetProfile();

                    role.ProfileName = value.RoleName;
                    role.Description = value.RoleName.ToUpper();



                    foreach (var privilege in value.allPrivelages)
                    {
                        long id = 0;
                        long.TryParse(privilege.Value, out id);
                        AspNetProfile a = unitOfWork.AspNetProfile.GetById(id);
                        var temp = new AspNetProfileRoles()
                        {
                            RoleId = a.ProfileId.ToString(),
                            ProfileId = role.ProfileId.ToString()
                        };
                        ///role..Add(temp);
                    }
                    message = "Role Added Successfully.";
                    unitOfWork.AspNetProfile.Add(role);
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
        }

        public ResponseViewModel AssignViewsToRole(AssignPrivilegesViewModel[] value)
        {
            ResponseViewModel response = new ResponseViewModel();

            try
            {
                string RoleId = value[0].RoleId;
                List<AspNetProfileRoles> _data = unitOfWork.AspNetProfileRoles.GetMany(p => p.RoleId == RoleId).ToList();
                if (_data.Count() > 0)
                {
                    foreach (var data in _data)
                    {
                        unitOfWork.AspNetProfileRoles.Delete(data);
                    }
                }

                for (int i = 0; i < value.Length; i++)
                {
                    AspNetProfileRoles db_role_privileges = new AspNetProfileRoles();

                    db_role_privileges.RoleId = value[i].RoleId;
                    db_role_privileges.ProfileId = value[i].PrivilegeId.ToString();

                    unitOfWork.AspNetProfileRoles.Add(db_role_privileges);

                }
                unitOfWork.AspNetProfileRoles.Commit();
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

        public ResponseViewModel Delete(string Id)
        {

            ResponseViewModel response = new ResponseViewModel();
            try
            {

                var role = unitOfWork.AspNetProfile.GetMany(a => a.ProfileId.Equals(Id)).FirstOrDefault();
                if (role != null)
                {
                    var role_Priviliges = unitOfWork.AspNetProfileRoles.GetMany(x => x.RoleId == role.ProfileId.ToString());
                    foreach (var privilege in role_Priviliges)
                    {
                        unitOfWork.AspNetProfileRoles.Delete(privilege);

                    }
                    unitOfWork.AspNetProfileRoles.Commit();
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
            //IEnumerable<RolesModel> Roles = unitOfWork.AspNetRole.GetAll().Select(p => new RolesModel()
            //{
            //    RoleId = p.Id,
            //    Name = p.Name
            //});

            //IEnumerable<PrivilegesModel> Privileges = unitOfWork.Privilages.GetAll().Select(p => new PrivilegesModel()
            //{
            //    PrivilegeId = p.Id,
            //    Name = p.Privilege,
            //    Category = p.Category,
            //    Portal = p.Portal
            //});


            //model.Roles = Roles;
            //model.Privileges = Privileges;

            response.Status = HttpStatusCode.OK;
            response.obj = model;
            return response;
        }
        public ResponseViewModel GetAllRole()
        {
            ResponseViewModel response = new ResponseViewModel();
            try
            {

                //IEnumerable<RolesModel> Roles = unitOfWork.AspNetRole.GetAll().Select(p => new RolesModel()
                //{
                //    Id = p.Id,
                //    Name = p.Name
                //}).ToList();
                response.Status = HttpStatusCode.OK;
               // response.obj = Roles;
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
                //IEnumerable<AssignPrivilegesViewModel> _data = unitOfWork.RolePrivilages.GetMany(p => p.RoleId == id, "Privilege").Select(p => new AssignPrivilegesViewModel()
                //{
                //    RoleId = p.RoleId,
                //    PrivilegeId = p.PrivilegeId,
                //    Category = p.Privilege.Category,
                //    Portal = p.Privilege.Portal
                //});
                response.Status = HttpStatusCode.OK;
                //response.obj = _data;
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
                //var _dbvalue = unitOfWork.AspNetRole.Get(p => p.Id == value.Id);
                //if (_dbvalue == null)
                //{
                //    response.Status = HttpStatusCode.BadRequest;
                //    response.obj = "Not Exist";
                //    response.Message = "Not Exist";
                //    return response;
                //}

                //_dbvalue.Name = value.RoleName;

                //unitOfWork.AspNetRole.Update(_dbvalue);
               // unitOfWork.AspNetRole.Commit();

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
