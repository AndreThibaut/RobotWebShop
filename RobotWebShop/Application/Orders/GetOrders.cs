﻿using Microsoft.EntityFrameworkCore;
using RobotWebShop.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotWebShop.Application.Orders
{
    public class GetOrders
    {
        private AppDbContext _ctx;

        public GetOrders(AppDbContext ctx)
        {
            _ctx = ctx;
        }

        public class Response
        {
            public string OrderRef { get; set; }

            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string email { get; set; }
            public string PhoneNumber { get; set; }
            public string Address1 { get; set; }
            public string Address2 { get; set; }
            public string City { get; set; }
            public string PostCode { get; set; }

            public IEnumerable<Robot> Robots { get; set; }

            public string TotalValue { get; set; }
        }
        public class Robot
        {
            public string Model { get; set; }
            public string Type { get; set; }
            public string Description { get; set; }
            public string Price { get; set; }
            public int Quantity { get; set; }
        }

        public Response Do(string reference) =>
            _ctx.Orders
                .Where(x => x.OrderRef == reference)
                .Include(x => x.OrderStocks)
                .ThenInclude(x => x.Stock)
                .ThenInclude(x => x.Robot)
                .Select(x => new Response
                {
                    OrderRef = x.OrderRef,

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
                        Type = y.Stock.Robot.MovementType,
                        Description = y.Stock.Model,
                        Price = $"{y.Stock.Robot.Price.ToString("N2")} €",
                        Quantity = y.Quantity,
                    }),
                    TotalValue = $"{x.OrderStocks.Sum(y => y.Stock.Robot.Price).ToString("N2")} €",
                })
                .FirstOrDefault();
        
    }
}
