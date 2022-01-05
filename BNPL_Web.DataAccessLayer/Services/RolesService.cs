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
                //if (!value.Id.Equals(""))
                //{
                //    var role = unitOfWork.AspNetRole.GetMany(a => a.Id == value.Id).FirstOrDefault();
                //    List<RolePrivilages> priviliges = unitOfWork.RolePrivilages.GetMany(p => p.RoleId == role.Id).ToList();

                //    foreach (var privilege in priviliges)
                //    {
                //        unitOfWork.RolePrivilages.Delete(privilege);
                //    }

                //    unitOfWork.RolePrivilages.Commit();

                //    foreach (var privilege in role.DbRolePrivileges.ToList())
                //    {
                //        unitOfWork.RolePrivilages.Delete(privilege);
                //    }


                //    foreach (var privilege in value.allPrivelages)
                //    {
                //        long id = 0;
                //        long.TryParse(privilege.Value, out id);
                //        Privilages a = unitOfWork.Privilages.GetById(id);
                //        var temp = new RolePrivilages()
                //        {
                //            PrivilegeId = a.Id,
                //            RoleId = role.Id
                //        };
                //        unitOfWork.RolePrivilages.Add(temp);
                //    }
                //    message = "Role Updated Successfully.";
                //    unitOfWork.RolePrivilages.Commit();


                //}
                //else if (value.Id.Equals(""))
                //{
                ApplicationRole role = new ApplicationRole();

                role.Name = value.Name;
                role.NormalizedName = value.Name.ToUpper();
                role.Id = Guid.NewGuid().ToString();



                //foreach (var privilege in value.allPrivelages)
                //{
                //    long id = 0;
                //    long.TryParse(privilege.Value, out id);
                //    Privilages a = unitOfWork.Privilages.GetById(id);
                //    var temp = new RolePrivilages()
                //    {
                //        PrivilegeId = a.Id,
                //        RoleId = role.Id
                //    };
                //    role.DbRolePrivileges.Add(temp);
                //}
                message = "Role Added Successfully.";
                unitOfWork.AspNetRole.Add(role);
                unitOfWork.AspNetRole.Commit();
                // }

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
                List<RolePrivilages> _data = unitOfWork.RolePrivilages.GetMany(p => p.RoleId == RoleId).ToList();
                if (_data.Count() > 0)
                {
                    foreach (var data in _data)
                    {
                        unitOfWork.RolePrivilages.Delete(data);
                    }
                }

                for (int i = 0; i < value.Length; i++)
                {
                    RolePrivilages db_role_privileges = new RolePrivilages();

                    db_role_privileges.RoleId = value[i].RoleId;
                    db_role_privileges.PrivilegeId = value[i].PrivilegeId;

                    unitOfWork.RolePrivilages.Add(db_role_privileges);

                }
                unitOfWork.RolePrivilages.Commit();
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
            throw new NotImplementedException();
        }

        public ResponseViewModel GetAllPrivilegeAndRole()
        {
            ResponseViewModel response = new ResponseViewModel();
            AssignPrivilegesViewModel model = new AssignPrivilegesViewModel();
            IEnumerable<RolesModel> Roles = unitOfWork.AspNetRole.GetAll().Select(p => new RolesModel()
            {
                RoleId = p.Id,
                Name = p.Name
            });

            IEnumerable<PrivilegesModel> Privileges = unitOfWork.Privilages.GetAll().Select(p => new PrivilegesModel()
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
            throw new NotImplementedException();
        }

        public ResponseViewModel GetAssignPrivilegeByRoleId(string id)
        {

            ResponseViewModel response = new ResponseViewModel();
            try
            {
                IEnumerable<AssignPrivilegesViewModel> _data = unitOfWork.RolePrivilages.GetMany(p => p.RoleId == id, "Privilege").Select(p => new AssignPrivilegesViewModel()
                {
                    RoleId = p.RoleId,
                    PrivilegeId = p.PrivilegeId,
                    Category = p.Privilege.Category,
                    Portal = p.Privilege.Portal
                });
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
            throw new NotImplementedException();
        }
    }
}
