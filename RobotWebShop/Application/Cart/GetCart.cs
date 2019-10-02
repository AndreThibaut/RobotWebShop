using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RobotWebShop.Database;
using RobotWebShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotWebShop.Application.Cart
{
    public class GetCart
    {
        private ISession _session;
        private AppDbContext _ctx;

        public GetCart(ISession session, AppDbContext ctx)
        {
            _session = session;
            _ctx = ctx;
        }

        public IEnumerable<Response> Do()
        {
            var stringObject = _session.GetString("Cart");

            if (string.IsNullOrEmpty(stringObject))
            {
                return new List<Response>();
            }

            var cartList = JsonConvert.DeserializeObject<List<CartRobot>>(stringObject);


            var response = _ctx.Stock
                .Include(x => x.Robot)
                .Where(x => cartList.Any(y => y.StockID == x.StockID))
                .Select(x => new Response
                {
                    Model = x.Robot.Model,
                    Price = $"{x.Robot.Price.ToString("N2")} €",
                    RealPrice= x.Robot.Price,
                    StockID = x.StockID,
                    Quantity = cartList.FirstOrDefault(y => y.StockID == x.StockID).Quantity
                })
                .ToList();
            

            return response;
        }

        public class Response
        {
            public string Model { get; set; }
            public string Price { get; set; }
            public decimal RealPrice { get; set; }
            public int StockID { get; set; }
            public int Quantity { get; set; }
        }
    }
}
