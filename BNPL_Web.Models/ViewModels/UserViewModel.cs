using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BNPL_Web.Common.ViewModels
{
    public class UserViewModel
    {
        public string? UserId { get; set; }
        public string? UserName { get; set; }
        public string? FirstNameEn { get; set; }
        public string? LastNameEn { get; set; }
        public string? MiddlelNameEn { get; set; }
        public string? FirstNameAr { get; set; }
        public string? LastNameAr { get; set; }
        public string? MiddlelNameAr { get; set; }
        public string? Title { get; set; }
        public string? Gender { get; set; }
        public string? CivilId { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string ? Language { get; set; }
        public string? RoleId { get; set; }
        public DateTime? DateOfBirth { get; set; }
    }
    public class SystemUserModel
    {
        public Guid? UserId { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? RoleId { get; set; }
        public DateTime? DateOfBirth { get; set; }
    }
}
