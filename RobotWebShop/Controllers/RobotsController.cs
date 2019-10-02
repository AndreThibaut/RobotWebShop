using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RobotWebShop.Application.RobotsAdmin;
using RobotWebShop.Database;
using RobotWebShop.Models;

namespace RobotWebShop.Controllers
{
    [Route("[controller]")]
    [Authorize(Policy = "Manager")]
    public class RobotsController : Controller
    {
        private AppDbContext _ctx;

        public RobotsController(AppDbContext ctx)
        {
            _ctx = ctx;
        }



        [HttpGet("")]
        public IActionResult GetRobots() => Ok(new GetRobots(_ctx).Do());

        [HttpGet("{id}")]
        public IActionResult GetRobot(int id) => Ok(new GetRobot(_ctx).Do(id));

        [HttpPost("")]
        public async Task<IActionResult> CreateRobot([FromBody] RobotViewModel vm) => Ok(await new CreateRobot(_ctx).Do(vm));

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRobot(int id) => Ok(await new DeleteRobot(_ctx).Do(id));

        [HttpPut("")]
        public async Task<IActionResult> UpdateRobot([FromBody] RobotViewModel vm) => Ok(await new UpdateRobot(_ctx).Do(vm));
    }
}
