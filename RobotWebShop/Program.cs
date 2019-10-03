using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using RobotWebShop.Database;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace RobotWebShop
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();

            try
            {
                using (var scope = host.Services.CreateScope())
                {
                    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                    var userManagement = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

                    context.Database.EnsureCreated();
                    if (!context.Users.Any())
                    {
                        var adminUser = new IdentityUser()
                        {
                            UserName = "Admin"
                        };

                        var managerUser = new IdentityUser()
                        {
                            UserName = "Manager"
                        };

                        userManagement.CreateAsync(adminUser, "password").GetAwaiter().GetResult();
                        userManagement.CreateAsync(managerUser, "password").GetAwaiter().GetResult();

                        var adminClaim = new Claim("Role", "Admin");
                        var managerClaim = new Claim("Role", "Manager");

                        userManagement.AddClaimAsync(managerUser, managerClaim).GetAwaiter().GetResult();
                        userManagement.AddClaimAsync(adminUser, adminClaim).GetAwaiter().GetResult();
                    }

                }
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
            }

            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
