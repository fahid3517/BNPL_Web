using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BNPL_Web.DatabaseModels.Authentication;
using BNPL_Web.DatabaseModels.DbImplementation;

namespace BNPL_Web.DatabaseModels.DTOs
{
    public class CustomerProfile
    {
        [Key]
        public Guid Id { get; set; }
        public string? RoleId { get; set; }
        public string? UserId { get; set; }
        public string? CivilId { get; set; }
        public string? Titile { get; set; }
        public string? FirstNameEN { get; set; }
        public string? MiddleNameEN { get; set; }
        public string? LastNameEN { get; set; }
        public string? FirstNameAR { get; set; }
        public string? MiddleNameAR { get; set; }
        public string? LastNameAR { get; set; }
        public string? FullName { get; set; }
        public string? Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Language { get; set; }
        public string? ContractNumber { get; set; }
        public string? Email { get; set; }
        public bool? VerifiedEmail { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
