using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RobotWebShop.Application.RobotsAdmin;
using RobotWebShop.Application.StockAdmin;
using RobotWebShop.Application.UsersAdmin;
using RobotWebShop.Database;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RobotWebShop.Controllers
{
    [Route("[controller]")]
    [Authorize(Policy = "Admin")]
    public class UsersController : Controller
    {
        private CreateUser _createUser;

        public UsersController(CreateUser createUser)
        {
            _createUser = createUser;
        }

        public async Task<IActionResult> CreateUser([FromBody]CreateUser.Request request)
        {
            await _createUser.Do(request);

            return Ok();
        }

    }
}
        

