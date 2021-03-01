using System;
using System.Security.Claims;
using Demo.InfraStructure;
using Demo.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Demo
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
            //services.Configure<CookiePolicyOptions>(options =>
            //{
            //    options.AccessDeniedPath = "/Identity/Account/AccessDenied";
            //    options.Cookie.Name = "YourAppCookieName";
            //    options.Cookie.HttpOnly = true;
            //    options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
            //    options.LoginPath = "/Identity/Account/Login";
            //    // ReturnUrlParameter requires 
            //    //using Microsoft.AspNetCore.Authentication.Cookies;
            //    options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;

            //});

            //services.ConfigureApplicationCookie(options =>
            //{
            //   // options.AccessDeniedPath = "/Identity/Account/AccessDenied";
            //    options.Cookie.Name = "YourAppCookieName";
            //    options.Cookie.HttpOnly = true;
            //    options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
            //    //options.LoginPath = "/Identity/Account/Login";
            //    // ReturnUrlParameter requires 
            //    //using Microsoft.AspNetCore.Authentication.Cookies;
            //    options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
            //   // options.SlidingExpiration = true;
            //});
            services.AddDbContext<ApplicationDbContext>(option => option.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));


            services.AddSingleton<IClaimsTransformation, HRClaimsProvider>();
         //   services.AddSingleton<IClaimsTransformation, HR0ClaimsProvider>();


            services.AddIdentity<AppUser, IdentityRole>(option => {

                option.User.RequireUniqueEmail = true;
                option.Password.RequiredLength = 4;
                option.SignIn.RequireConfirmedEmail = true;
            })
             .AddEntityFrameworkStores<ApplicationDbContext>()
             .AddDefaultTokenProviders();



            services.AddAuthorization(option =>
            {
                option.AddPolicy("HasCity", policy => {
                    policy.RequireClaim(ClaimTypes.StateOrProvince);
                   // policy.RequireRole("admin");
 

                });

            });

            services.AddAuthorization(option =>
            {
                option.AddPolicy("TehranCity", policy => {
                    policy.RequireClaim(ClaimTypes.StateOrProvince,"Tehran");
                   // policy.RequireRole("admin");


                });
                option.AddPolicy("HasCity", policy => {
                    policy.RequireClaim(ClaimTypes.StateOrProvince);
                    // policy.RequireRole("admin");


                });
                option.AddPolicy("blockCheck", policy => {
                    //policy.RequireClaim(ClaimTypes.StateOrProvince);
                    // policy.RequireRole("admin");
                    policy.RequireAuthenticatedUser();
                    policy.AddRequirements(new SuspendUserRequirement("c@C"));

                });


            });

            



            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseAuthentication();
            
            app.UseCookiePolicy();


            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
