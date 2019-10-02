using RobotWebShop.Database;
using RobotWebShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RobotWebShop.Application.StockAdmin
{
    public class CreateStock
    {
        private AppDbContext _ctx;

        public CreateStock(AppDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<Response> Do(Request request) 
        {
            var stock = new Stock
            {
                Model = request.Model,
                Quantity = request.Quantity,
                RobotID =  request.RobotID
            };

            _ctx.Stock.Add(stock);
            await _ctx.SaveChangesAsync();

            return new Response 
            {
                StockID = stock.StockID,
                Model= stock.Model,
                Quantity = stock.Quantity
            };
            
        }

        public class Request
        {
            public int RobotID { get; set; }
            public string Model { get; set; }
            public int Quantity { get; set; }
        }

        public class Response
        {
            public int StockID { get; set; }
            public string Model { get; set; }
            public int Quantity { get; set; }
        }
    }
}
