using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotWebShop.Application.UsersAdmin
{
    public class CreateUser
    {
        private UserManager<IdentityUser> _userManager;

        public CreateUser(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public class Request
        {
            public string UserName { get; set; }
            public string Password { get; set; }
        }

        public async Task<bool> Do(Request request)
        {
            var managerUser = new IdentityUser()
            {
                UserName = request.UserName
            };

            await _userManager.CreateAsync(managerUser, "password");

            var managerClaim = new System.Security.Claims.Claim("Role", "Manager");

            _userManager.AddClaimAsync(managerUser, managerClaim);

            return true;
        }
    }
}
