using RobotWebShop.Database;
using RobotWebShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RobotWebShop.Application.StockAdmin
{
    public class UpdateStock
    {
        private AppDbContext _ctx;

        public UpdateStock(AppDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<Response> Do(Request request)
        {
            var stocks = new List<Stock>();

            foreach (var stock in request.Stock)
            {
                stocks.Add(new Stock
                {
                    StockID = stock.StockID,
                    Model = stock.Model,
                    Quantity = stock.Quantity,
                    RobotID = stock.RobotID
                });
            }


            _ctx.Stock.UpdateRange(stocks);
            await _ctx.SaveChangesAsync();

            return new Response
            {
                Stock = request.Stock
            };

        }

        public class StockViewModel
        {
            public int StockID { get; set; }
            public int RobotID { get; set; }
            public string Model { get; set; }
            public int Quantity { get; set; }
        }

        public class Request
        {
            public IEnumerable<StockViewModel> Stock { get; set; }
        }

        public class Response
        {
            public IEnumerable<StockViewModel> Stock { get; set; }
        }
    }
}
