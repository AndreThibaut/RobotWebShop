using RobotWebShop.Database;
using RobotWebShop.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotWebShop.Application.OrdersAdmin
{
    public class GetOrders
    {
        private AppDbContext _ctx;

        public GetOrders(AppDbContext ctx)
        {
            _ctx = ctx;
        }

        public IEnumerable<Response> Do(int status) => 
            _ctx.Orders
                .Where(x => x.Status == (OrderStatus)status)
                .Select(x => new Response
                {
                    ID = x.OrderID,
                    OrderRef = x.OrderRef,
                    Email = x.email
                })
               .ToList();
    public class Response
        {
            public int ID { get; set; }
            public string OrderRef { get; set; }
            public string Email { get; set; }
        }
    }
}
