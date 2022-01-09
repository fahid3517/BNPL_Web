using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BNPL_Web.DatabaseModels.Authentication;
using BNPL_Web.DatabaseModels.DbImplementation;
using BNPL_Web.DatabaseModels.DTOs;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BNPL_Web.DatabaseModels.Models
{
    public class BNPL_Context : DbContext
    {
        public BNPL_Context(DbContextOptions<BNPL_Context> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<CustomerProfile> CustomerProfile { get; set; }
        public DbSet<SystemUsers> SystemUsers { get; set; }
        public DbSet<AspNetRoles> AspNetRoles { get; set; }
        public DbSet<AspNetProfile> AspNetProfile { get; set; }
        public DbSet<AspNetProfileRoles> AspNetProfileRoles { get; set; }
        public DbSet<UserProfiles> UserProfiles { get; set; }
        public DbSet<AspNetMembership> AspNetMembership { get; set; }
    }
}
