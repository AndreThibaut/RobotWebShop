using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RobotWebShop.Application.UsersAdmin;
using RobotWebShop.Database;
using Stripe;

namespace RobotWebShop.UI
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<IdentityUser, IdentityRole>(options => 
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
            })
                .AddEntityFrameworkStores<AppDbContext>();

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Accounts/Login";
            });

            services.AddAuthorization(options => 
            {
                options.AddPolicy("Admin", policy => policy.RequireClaim("Role", "Admin"));
                options.AddPolicy("Manager", policy => policy.RequireClaim("Role", "Manager"));
                options.AddPolicy("Manager", policy => 
                    policy.RequireAssertion(context => 
                        context.User.HasClaim("Role", "Manager") || context.User.HasClaim("Role", "Admin")
                        ));
            });

            services
                .AddMvc()
                .AddRazorPagesOptions(options =>
                {
                    options.Conventions.AuthorizeFolder("/Admin");
                    options.Conventions.AuthorizePage("/Admin/ConfigureUsers", "Admin");
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddSession(options =>
                {
                    options.Cookie.Name = "Cart";
                    options.Cookie.MaxAge = TimeSpan.FromMinutes(20);
                }
            );

            services.AddTransient<CreateUser>();

            
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
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseSession();

            app.UseAuthentication();

            StripeConfiguration.SetApiKey(Configuration.GetSection("Stripe")["SecretKey"]);

            app.UseMvcWithDefaultRoute();
        }
    }
}
