using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RobotWebShop.Application.RobotsAdmin;
using RobotWebShop.Application.StockAdmin;
using RobotWebShop.Database;
using RobotWebShop.Models;

namespace RobotWebShop.Controllers
{
    [Route("[controller]")]
    [Authorize(Policy = "Manager")]
    public class StocksController : Controller
    {
        private AppDbContext _ctx;

        public StocksController(AppDbContext ctx)
        {
            _ctx = ctx;
        }



        [HttpGet("")]
        public IActionResult GetStock() => Ok(new GetStock(_ctx).Do());

        [HttpPost("")]
        public async Task<IActionResult> CreateStock([FromBody] CreateStock.Request request) => Ok(await new CreateStock(_ctx).Do(request));

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStock(int id) => Ok(await new DeleteStock(_ctx).Do(id));

        [HttpPut("")]
        public async Task<IActionResult> UpdateStock([FromBody] UpdateStock.Request request) => Ok(await new UpdateStock(_ctx).Do(request));
    }
}
