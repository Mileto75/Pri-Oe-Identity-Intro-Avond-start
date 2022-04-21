using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Pri.Oe.Identity.Intro.Mvc.ApplicationClaimTypes;
using Pri.Oe.Identity.Intro.Mvc.Services.Users;
using System.Linq;
using System.Net.Mail;
using System.Security.Claims;

namespace Pri.Oe.Identity.Intro.Mvc
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
            //add the httpcontext
            services.AddHttpContextAccessor();
            //add Authentication
            services.AddAuthentication("CookieAuth")
                .AddCookie("CookieAuth", options => 
                {
                    //cookie based authentication options
                    options.Cookie.Name = "CookieAuth";
                    options.LoginPath = "/Users/Login";
                    options.LogoutPath = "/Users/Logout";
                    options.AccessDeniedPath = "/Users/AccessDenied";
                    //for testing purposes only
                    options.ExpireTimeSpan = System.TimeSpan.FromMinutes(1);
                });
            //register Authorization using policies
            services.AddAuthorization(options => 
            {
                //simple policy based on existing claim
                options.AddPolicy("HasModule", policy =>
                {
                    //must have at least one module claim
                    policy.RequireClaim(AppClaimTypes.Module);
                });
                //next policy
                //policy based on a claim and content of the claim
                //only Pri students policy
                options.AddPolicy("PriStudent", policy =>
                {
                    policy.RequireClaim(AppClaimTypes.Module, "Pri");
                });
                //policy on a multiple claims
                //must have email and module
                options.AddPolicy("EmailAndModule", policy => 
                {
                    policy.RequireClaim(ClaimTypes.Email)
                        .RequireClaim(AppClaimTypes.Module);

                });
                //policy on multiple claims
                //must have module wba or Pri
                options.AddPolicy("WbaOrPri", policy =>
                {
                    policy.RequireClaim(AppClaimTypes.Module,"Pri","Wba");
                });
            });
            //add the userservice
            services.AddScoped<IUserService, UserService>();
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
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

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
