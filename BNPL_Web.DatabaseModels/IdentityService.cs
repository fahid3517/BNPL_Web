
using System.Threading.Tasks;
using BNPL_Web.DatabaseModels.Authentication;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;


#nullable disable

namespace Project.DatabaseModel.Models
{
    public partial class IdentityService : IdentityDbContext<ApplicationUser>
    {
        public IdentityService(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//       {
//            if (!optionsBuilder.IsConfigured)
//            {
////#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//                optionsBuilder.UseSqlServer(Configuration.GetConnectionString("JYA_HRMS_2.0"));
//            }
//        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);


       
    }

    public  class ApplicationDbInitializer
    {
        public  static void SeedUsers(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            const string name = "superadmin";
            const string password = "Abc@12345";
            const string roleName = "Admin";

            //Create Role Admin if it does not exist
            var role =   roleManager.FindByNameAsync(roleName).Result;
            if (role == null)
            {
                role = new IdentityRole(roleName);
                roleManager.CreateAsync(role).Wait();
            }

            var user = userManager.FindByNameAsync(name).Result;
            if (user == null)
            {
                user = new ApplicationUser { UserName = name, Email = name };
                 userManager.CreateAsync(user, password).Wait();
                 userManager.SetLockoutEnabledAsync(user, false).Wait();
            }

            // Add user admin to Role Admin if not already added
            var rolesForUser = userManager.GetRolesAsync(user).Result;
            if (rolesForUser.Count==0)
            {
                  userManager.AddToRoleAsync(user, role.Name).Wait();
            }
        }
    }
}
