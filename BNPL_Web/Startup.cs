﻿using BNPL_Web.Authentications;
using BNPL_Web.Common.Interface;
using BNPL_Web.DatabaseModels.Authentication;
using BNPL_Web.DatabaseModels.DbImplementation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Serialization;
using Project.DataAccessLayer.Shared;
using Project.DatabaseModel.DbImplementation;
using System.Configuration;
using System.Net;
using System.Reflection;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using BNPL_Web.DatabaseModels.Models;
using BNPL_Web.Authorizations;

namespace BNPL_Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var mvcBuilder = services.AddRazorPages();
            mvcBuilder.AddRazorRuntimeCompilation();
            mvcBuilder.AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();

            });
            services.ConfigureApplicationCookie(options =>
            {
                options.Events.OnRedirectToLogin = async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    await context.Response.WriteAsync("Some custom error message if required");
                };
            });

            services.AddAuthentication(sharedOptions =>
            {
                sharedOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                sharedOptions.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
                sharedOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
               .AddJwtBearer(options =>
               {
                   var keyByteArray = Encoding.ASCII.GetBytes(this.Configuration.GetValue<String>("Tokens:Key"));
                   var signinKey = new SymmetricSecurityKey(keyByteArray);

                   options.TokenValidationParameters = new TokenValidationParameters
                   {
                       IssuerSigningKey = signinKey,
                       ValidAudience = "Audience",
                       ValidIssuer = "Issuer",
                       ValidateIssuerSigningKey = true,
                       ValidateLifetime = true,
                       ClockSkew = TimeSpan.FromMinutes(0)
                   };
               });

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
            // services.AddTransient<IAuthorizationMiddlewareResultHandler, ApiCustomAuthorizeAttribute>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped(typeof(IFileManager), typeof(FileManager));
            var apiKey = Configuration["Twilio:"];

            services.AddHttpClient<TwilioVerifyClient>(client =>
            {
                client.BaseAddress = new Uri("https://api.twilio.com/2022-01-11/Accounts/{SKa4d3d5c0df9f3dbbeabb3a65ddb729b4}/c089399c224231bf782f81a20e745e84.json");
                ///client.BaseAddress = new Uri("https://api.authy.com/");
                ///client.DefaultRequestHeaders.Add("X-Authy-API-Key", apiKey);
            });
            //IdentityConfig(services);

            services.ConfigureApplicationCookie(options =>
            {
                //Cookie settings
                options.Cookie.HttpOnly = true;
                // options.LoginPath = new PathString("/SelfPortal/Account/Login");
                options.AccessDeniedPath = new PathString("/Self/Account/Login");
                //options.Cookie.Expiration
                options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                options.SlidingExpiration = true;
            });
         RegisterDependancy(services, ServiceLifetime.Scoped);
            services.AddMvc();
            services.AddDbContext<BNPL_Context>(options =>
                 options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<BNPL_Context>()
                .AddDefaultTokenProviders();
            RegisterDependancy(services, ServiceLifetime.Scoped);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();


            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            //Area Routing
            app.UseEndpoints(endpoints =>
            {

                //HrPortal Route
                endpoints.MapAreaControllerRoute(
                    name: "HrPortal",
                    areaName: "HrPortal",
                    pattern: "HR/{controller=Home}/{action=Index}/{id?}");
                //Master Data Area 
                endpoints.MapAreaControllerRoute(
                    name: "SelfPortal",
                    areaName: "SelfPortal",
                    pattern: "Self/{controller=Home}/{action=Index}/{id?}");
                //Master Data Area 
                endpoints.MapAreaControllerRoute(
                   name: "MasterData",
                   areaName: "MasterData",
                   pattern: "MasterData/{controller=Home}/{action=Index}/{id?}");


                //Default Route(Area)
                endpoints.MapAreaControllerRoute(
                    name: "default",
                    areaName: "SelfPortal",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                // Default Route
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllers();

            });
            app.UseStaticFiles(new StaticFileOptions()
            {
                ServeUnknownFileTypes = true, // this was needed as IIS would not serve extensionless URLs from the directory without it
                FileProvider = new PhysicalFileProvider(
                Path.Combine(Directory.GetCurrentDirectory(), @"Scripts")),
                RequestPath = new PathString("/Scripts"),
            });
            
            app.UseStaticFiles(new StaticFileOptions()
            {
                ServeUnknownFileTypes = true, // this was needed as IIS would not serve extensionless URLs from the directory without it
                FileProvider = new PhysicalFileProvider(
                 Path.Combine(Directory.GetCurrentDirectory(), @"Areas/SelfPortal/Scripts")),
                RequestPath = new PathString("/Scripts"),
            });
            //app.UseStaticFiles(new StaticFileOptions()
            //{
            //    ServeUnknownFileTypes = true, // this was needed as IIS would not serve extensionless URLs from the directory without it
            //    FileProvider = new PhysicalFileProvider(
            //    Path.Combine(Directory.GetCurrentDirectory(), @"UploadedFiles")),
            //    RequestPath = new PathString("/UploadedFiles"),
            //});
            app.UseStaticFiles(new StaticFileOptions()
            {
                ServeUnknownFileTypes = true, // this was needed as IIS would not serve extensionless URLs from the directory without it
                FileProvider = new PhysicalFileProvider(
               Path.Combine(Directory.GetCurrentDirectory(), @"Areas/SelfPortal/Scripts")),
                RequestPath = new PathString("/Viwes"),
            });
            //  ApplicationDbInitializer.SeedUsers(userManager, roleManager);
        }


       // private void IdentityConfig(IServiceCollection services)
       // {

       //     services.AddIdentity<ApplicationUser, IdentityRole>(
       //options =>
       //{
       //    options.SignIn.RequireConfirmedAccount = false;
       //    options.Password.RequireDigit = true;
       //    options.Password.RequireLowercase = true;
       //    options.Password.RequireNonAlphanumeric = true;
       //    options.Password.RequireUppercase = true;
       //    options.Password.RequiredLength = 6;
       //    options.Password.RequiredUniqueChars = 1;
       //    Other options go here
       //})
       //.AddEntityFrameworkStores<IdentityService>();

       // }
        private void RegisterDependancy(IServiceCollection services, ServiceLifetime lifetime = ServiceLifetime.Scoped)
        {
            var typesFromAssemblies = Assembly.Load("BNPL_Web.DataAccessLayer").GetTypes().Where(x => x.Name.EndsWith("Service") && !x.IsInterface);
            foreach (var type in typesFromAssemblies)
            {
                var interfaceType = type.GetInterfaces().FirstOrDefault();

                if (interfaceType == null)
                {
                    services.Add(new ServiceDescriptor(type, type, lifetime));
                    continue;
                }
                services.Add(new ServiceDescriptor(interfaceType, type, lifetime));
            }
        }
    }
}
