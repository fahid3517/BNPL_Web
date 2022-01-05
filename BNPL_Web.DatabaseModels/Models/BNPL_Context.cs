using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BNPL_Web.DatabaseModels.Authentication;
using BNPL_Web.DatabaseModels.DTOs;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BNPL_Web.DatabaseModels.Models
{
    public class BNPL_Context : IdentityDbContext<ApplicationUser>
    {
        public BNPL_Context(DbContextOptions<BNPL_Context> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<CustomerProfile> CustomerProfile { get; set; }
        public DbSet<BackOfficeUserProfile> BackOfficeUserProfile { get; set; }
        public DbSet<SystemUsersProfile> SystemUsersProfile { get; set; }
        public DbSet<Privilages> Privilages { get; set; }
    }
}
