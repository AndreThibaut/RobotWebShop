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
    public class GetOrder
    {
        private ISession _session;
        private AppDbContext _ctx;

        public GetOrder(ISession session, AppDbContext ctx)
        {
            _session = session;
            _ctx = ctx;
        }

        public Response Do()
        {
            var cart = _session.GetString("Cart");

            var cartList = JsonConvert.DeserializeObject<List<CartRobot>>(cart);

            var listOfRobots = _ctx.Stock
                .Include(x => x.Robot)
                .Where(x => cartList.Any(y => y.StockID == x.StockID))
                .Select(x => new Robot
                {
                    RobotID = x.RobotID,
                    StockID = x.StockID,
                    Price = (int)(x.Robot.Price *100),
                    Quantity = cartList.FirstOrDefault(y => y.StockID == x.StockID).Quantity
                })
                .ToList();

            var customerInfoString = _session.GetString("Customer-Info");
            var customerInformation = JsonConvert.DeserializeObject<CustomerInformation>(customerInfoString);

            return new Response 
            {
                Robots = listOfRobots,
                CustomerInformation = new CustomerInformation
                {
                    FirstName = customerInformation.FirstName,
                    LastName = customerInformation.LastName,
                    email = customerInformation.email,
                    PhoneNumber = customerInformation.PhoneNumber,
                    Address1 = customerInformation.Address1,
                    Address2 = customerInformation.Address2,
                    City = customerInformation.City,
                    PostCode = customerInformation.PostCode
                }
            };
        }
        public class Response
        {
            public IEnumerable<Robot> Robots { get; set; }
            public CustomerInformation CustomerInformation { get; set; }

            public int GetTotalCharge() => Robots.Sum(x => x.Price * x.Quantity);
        }
        public class Robot
        {
            public int RobotID { get; set; }
            public int StockID { get; set; }
            public int Quantity { get; set; }

            public int Price { get; set; }
        }
        public class CustomerInfo
        {

            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string email { get; set; }
            public string PhoneNumber { get; set; }
            public string Address1 { get; set; }
            public string Address2 { get; set; }
            public string City { get; set; }
            public string PostCode { get; set; }
        }
    }
}
