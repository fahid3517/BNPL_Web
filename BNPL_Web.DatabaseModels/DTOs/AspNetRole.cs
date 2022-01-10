using System;
using System.Collections.Generic;

namespace BNPL_Web.DatabaseModels.DTOs
{
    public class AspNetRole
    {
        public int Id { get; set; }
        public string Privilege { get; set; } 
        public string Category { get; set; } 
        public string Portal { get; set; } 
        public int? SortOrder { get; set; }
    }
}
