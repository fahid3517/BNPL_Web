using BNPL_Web.Common.ViewModels.Base;

namespace BNPL_Web.Common.ViewModels.Authorization
{
    public class AssignPrivilegesViewModel : BaseViewModel
    {
        public int PrivilegeId { get; set; }
        public Guid ?RoleId { get; set; }
        public string ?Type { get; set; }
        public string? Category { get; set; }
        public string? Portal { get; set; }

        public IEnumerable<RolesModel> ?Roles { get; set; }
        public IEnumerable<PrivilegesModel>? Privileges { get; set; }
        public List<AssignPrivilegesViewModel>? List { get; set; }
    }
    public class RolesModel : AssignPrivilegesViewModel
    {
        public new string? Id { get; set; }
    }
    public class PrivilegesModel : AssignPrivilegesViewModel
    {

    }
}
