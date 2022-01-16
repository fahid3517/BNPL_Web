using BNPL_Web.Authentications;
using BNPL_Web.Common.Interface;
using BNPL_Web.DatabaseModels.Authentication;
using BNPL_Web.DatabaseModels.DbImplementation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Serialization;
using Project.DatabaseModel.DbImplementation;
using System.Configuration;
using System.Net;
using System.Reflection;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using BNPL_Web.DatabaseModels.Models;
using Project.DataAccessLayer.SharedServices;
using BNPL_Web.Notification.Interface;
using BNPL_Web.Notification.Service;
using CorePush.Google;
using CorePush.Apple;
using BNPL_Web.Notification.Models;
using Microsoft.OpenApi.Models;
using BNPL_Web.smsService;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;

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
            // Register the swagger generator
            //services.AddSwaggerGen(c => {
            //    c.SwaggerDoc(name: "V1", new OpenApiInfo { Title = "My API", Version = "V1" });
            //});
            services.AddApiVersioning(o =>
            {
                o.ReportApiVersions = true;
                o.AssumeDefaultVersionWhenUnspecified = true;
                o.DefaultApiVersion = new ApiVersion(2, 0);
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Swagger  Documentation", Version = "v1" });
                c.SwaggerDoc("App-v1", new OpenApiInfo { Title = "Swagger Admin Documentation", Version = "v1" });

                c.OperationFilter<RemoveVersionFromParameter>();
                c.DocumentFilter<ReplaceVersionWithExactValueInPath>();

                // Ensure the routes are added to the right Swagger doc
                c.DocInclusionPredicate((docName, apiDesc) =>
                {
                    if (!apiDesc.TryGetMethodInfo(out MethodInfo methodInfo))
                    {
                        return false;
                    }

                    if (methodInfo.DeclaringType.FullName.Contains("Admin"))
                    {
                        IEnumerable<ApiVersion> versions = methodInfo.DeclaringType
                        .GetCustomAttributes(true)
                        .OfType<ApiVersionAttribute>()
                        .SelectMany(a => a.Versions);

                        return versions.Any(v => $"App-v{v.ToString()}" == docName);
                    }
                    else
                    {
                        IEnumerable<ApiVersion> versions = methodInfo.DeclaringType
                        .GetCustomAttributes(true)
                        .OfType<ApiVersionAttribute>()
                        .SelectMany(a => a.Versions);

                        return versions.Any(v => $"v{v.ToString()}" == docName);
                    }
                });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please insert JWT with Bearer into field",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
   {
     new OpenApiSecurityScheme
     {
       Reference = new OpenApiReference
       {
         Type = ReferenceType.SecurityScheme,
         Id = "Bearer"
       }
      },
      new string[] { }
    }
  });
               // c.IncludeXmlComments(Path.Combine(
               //    Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), $"{this.GetType().Assembly.GetName().Name}.xml"
               //));
                c.CustomSchemaIds(x => x.FullName);
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
            services.AddScoped<ISmsService, SmsService>();
            // services.AddTransient<IAuthorizationMiddlewareResultHandler, ApiCustomAuthorizeAttribute>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped(typeof(IFileManager), typeof(FileManager));

            services.AddTransient<INotificationService, NotificationService>();
            services.AddHttpClient<FcmSender>();
            services.AddHttpClient<ApnSender>();

            // Configure strongly typed settings objects
            var appSettingsSection = Configuration.GetSection("FcmNotification");
            services.Configure<FcmNotificationSetting>(appSettingsSection);
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
               // app.UseSwagger();
                //app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApiDB v1"));
                
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

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Swagger Admin Documentation V1");
                c.SwaggerEndpoint("/swagger/App-v1/swagger.json", "Swagger Mobile App Documentation V1");
            });

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
           
            app.UseStaticFiles(new StaticFileOptions()
            {
                ServeUnknownFileTypes = true, // this was needed as IIS would not serve extensionless URLs from the directory without it
                FileProvider = new PhysicalFileProvider(
               Path.Combine(Directory.GetCurrentDirectory(), @"Areas/SelfPortal/Scripts")),
                RequestPath = new PathString("/Viwes"),
            });
        }


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
        public class RemoveVersionFromParameter : IOperationFilter
        {
            public void Apply(OpenApiOperation operation, OperationFilterContext context)
            {
                var versionParameter = operation.Parameters.Single(p => p.Name == "version");
                operation.Parameters.Remove(versionParameter);
            }
        }

        public class ReplaceVersionWithExactValueInPath : IDocumentFilter
        {
            public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
            {
                var paths = new OpenApiPaths();
                foreach (var path in swaggerDoc.Paths)
                {
                    paths.Add(path.Key.Replace("v{version}", swaggerDoc.Info.Version), path.Value);
                }
                swaggerDoc.Paths = paths;
            }
        }
    }
}
