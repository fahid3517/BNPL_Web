using BNPL_Web.DatabaseModels.DbImplementation;
using BNPL_Web.DatabaseModels.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BNPL_Web.DatabaseModels.Models
{
    public class BNPL_Context:DbContext
    {
        public BNPL_Context(DbContextOptions<BNPL_Context> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public virtual DbSet<AspNetMembership> AspNetMemberships { get; set; } = null!;
        public virtual DbSet<AspNetProfile> AspNetProfiles { get; set; } = null!;
        public virtual DbSet<AspNetProfileRoles> AspNetProfileRoles { get; set; } = null!;
        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; } = null!;
        public virtual DbSet<ApplicationUser> AspNetUsers { get; set; } = null!;
        public virtual DbSet<CustomerProfile> CustomerProfiles { get; set; } = null!;
        public virtual DbSet<SystemUsersProfile> SystemUsers { get; set; } = null!;
        public virtual DbSet<UserProfiles> UserProfiles { get; set; } = null!;
    }
}