using Microsoft.AspNetCore.Http;
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
    public class AddToCart
    {
        private ISession _session;
        private AppDbContext _ctx;

        public AddToCart(ISession session, AppDbContext ctx)
        {
            _session = session;
            _ctx = ctx;
        }

        public async Task<bool> Do(Request request)
        {
            var stockOnHold = _ctx.StockOnHolds.Where(x => x.SessionID == _session.Id).ToList();
            var stockToHold = _ctx.Stock.Where(x => x.StockID == request.StockID).FirstOrDefault();

            if(stockToHold.Quantity < request.Quantity)
            {
                return false;
            }
            

            _ctx.StockOnHolds.Add(new StockOnHold
            {
                StockID = stockToHold.StockID,
                SessionID = _session.Id,
                Quantity = request.Quantity,
                ExpiryDate = DateTime.Now.AddMinutes(20)
            });

            stockToHold.Quantity = stockToHold.Quantity - request.Quantity;

            foreach(var stock in stockOnHold)
            {
                stock.ExpiryDate = DateTime.Now.AddMinutes(20);
            }

            await _ctx.SaveChangesAsync();


            var cartList = new List<CartRobot>();
            var stringObject = _session.GetString("Cart");

            if (!string.IsNullOrEmpty(stringObject))
            {
                cartList = JsonConvert.DeserializeObject<List<CartRobot>>(stringObject);
            }

            if(cartList.Any(x => x.StockID == request.StockID))
            {
                cartList.Find(x => x.StockID == request.StockID).Quantity += request.Quantity;
            }
            else
            {
                cartList.Add(new CartRobot
                {
                    StockID = request.StockID,
                    Quantity = request.Quantity
                });
            }

            stringObject = JsonConvert.SerializeObject(cartList);

            _session.SetString("Cart", stringObject);

            return true;
        }

        public class Request
        {
            public int StockID { get; set; }
            public int Quantity { get; set; }
        }
    }
}
