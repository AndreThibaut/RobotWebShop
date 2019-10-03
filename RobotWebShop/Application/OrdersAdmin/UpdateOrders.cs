using RobotWebShop.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotWebShop.Application.OrdersAdmin
{
    public class UpdateOrders
    {
        private AppDbContext _ctx;

        public UpdateOrders(AppDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<bool> Do(int id)
        {
            var order = _ctx.Orders.FirstOrDefault(x => x.OrderID == id);
            order.Status++;

            return await _ctx.SaveChangesAsync() > 0;
        }
    }
}
