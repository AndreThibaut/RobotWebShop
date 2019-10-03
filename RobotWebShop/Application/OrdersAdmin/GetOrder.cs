using Microsoft.EntityFrameworkCore;
using RobotWebShop.Database;
using RobotWebShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RobotWebShop.Application.OrdersAdmin
{
    public class GetOrder
    {
        private AppDbContext _ctx;

        public GetOrder(AppDbContext ctx)
        {
            _ctx = ctx;
        }

        public Response Do(int id) =>
            _ctx.Orders
                .Where(x => x.OrderID == id)
                .Include(x => x.OrderStocks)
                .ThenInclude(x => x.Stock)
                .ThenInclude(x => x.Robot)
                .Select(x => new Response 
                {
                    OrderID = x.OrderID,
                    OrderRef = x.OrderRef,
                    StripeRef = x.StripeRef,

                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    email = x.email,
                    PhoneNumber = x.PhoneNumber,
                    Address1 = x.Address1,
                    Address2 = x.Address2,
                    City = x.City,
                    PostCode = x.PostCode,

                    Robots = x.OrderStocks.Select(y => new Robot
                    {
                        Model = y.Stock.Robot.Model,
                        Type = y.Stock.Robot.Function,
                        Quantity = y.Quantity,
                        Description = y.Stock.Model,
                        Price = $"{y.Stock.Robot.Price.ToString("N2")} €",
                    }),
                }).FirstOrDefault();

        public class Response
        {
            public int OrderID { get; set; }
            public string OrderRef { get; set; }
            public string StripeRef { get; set; }

            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string email { get; set; }
            public string PhoneNumber { get; set; }

            public string Address1 { get; set; }
            public string Address2 { get; set; }
            public string City { get; set; }
            public string PostCode { get; set; }

            public IEnumerable<Robot> Robots { get; set; }
        }

        public class Robot
        {
            public string Model { get; set; }
            public string Type { get; set; }
            public string Description { get; set; }
            public string Price { get; set; }
            public int Quantity { get; set; }
        }
    }
}
