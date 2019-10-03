using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RobotWebShop.Application.OrdersAdmin;
using RobotWebShop.Database;
using System.Threading.Tasks;

namespace RobotWebShop.Controllers
{
    [Route("[controller]")]
    [Authorize(Policy = "Manager")]
    public class OrdersController : Controller
    {
        private AppDbContext _ctx;

        public OrdersController(AppDbContext ctx)
        {
            _ctx = ctx;
        }


        [HttpGet("")]
        public IActionResult GetOrders(int status) =>Ok(new GetOrders(_ctx).Do(status));

        [HttpGet("{id}")]
        public IActionResult GetOrder(int id) => Ok(new GetOrder(_ctx).Do(id));

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder(int id) => Ok(await new UpdateOrders(_ctx).Do(id));
        
    }
}
