using System;
using System.Collections.Generic;

namespace BNPL_Web.DatabaseModels.DTOs
{
    public partial class CustomerProfile
    {
        public Guid Id { get; set; }
        public string? UserId { get; set; }
        public string? CivilId { get; set; }
        public string? Titile { get; set; }
        public string? FirstNameEn { get; set; }
        public string? MiddleNameEn { get; set; }
        public string? LastNameEn { get; set; }
        public bool? IsVerify { get; set; }
        public string? FirstNameAr { get; set; }
        public string? MiddleNameAr { get; set; }
        public string? LastNameAr { get; set; }
        public string? FullName { get; set; }
        public string? Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Language { get; set; }
        public string? ContractNumber { get; set; }
        public bool VerifiedContact { get; set; }
        public string? Email { get; set; }
        public bool? VerifiedEmail { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
