using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BNPL_Web.Common.ViewModels;
using BNPL_Web.Common.ViewModels.Authorization;
using BNPL_Web.Common.ViewModels.Common;

namespace BNPL_Web.DataAccessLayer.IServices
{
    public interface IRoleService
    {
        ResponseViewModel Activate(RolesViewModel value);
        ResponseViewModel Add(RolesViewModel value);
        ResponseViewModel GetAssignPrivilegeByRoleId(string id);
        ResponseViewModel GetById(int id);
        ResponseViewModel GetAllPrivilegeAndRole();
        ResponseViewModel GetAllRole();
        ResponseViewModel Update(RolesViewModel value);
        ResponseViewModel Delete(Guid Id);
        ResponseViewModel AssignViewsToRole(AssignPrivilegesViewModel[] value);
    }
}
