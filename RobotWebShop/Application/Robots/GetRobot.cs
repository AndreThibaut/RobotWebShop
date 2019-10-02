using Microsoft.EntityFrameworkCore;
using RobotWebShop.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RobotWebShop.Application.Robots
{
    public class GetRobot
    {
        private AppDbContext _ctx;

        public GetRobot(AppDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<RobotsViewModel> Do(string name)
        {

            var stockOnHold = _ctx.StockOnHolds.Where(x => x.ExpiryDate < DateTime.Now).ToList();

            if(stockOnHold.Count > 0)
            {
                var stockToReturn = _ctx.Stock.Where(x => stockOnHold.Any(y => y.StockID == x.StockID)).ToList();

                foreach(var stock in stockToReturn)
                {
                    stock.Quantity = stock.Quantity + stockOnHold.FirstOrDefault(x => x.StockID == stock.StockID).Quantity;
                }

                _ctx.StockOnHolds.RemoveRange(stockOnHold);

                await _ctx.SaveChangesAsync();
            }

            return _ctx.Robots
            .Include(x => x.Stock)
            .Where(x => x.Model == name)
            .Select(x => new RobotsViewModel
            {
                Model = x.Model,
                Desciption = "Functionality: " + x.Function + " Tarvel Mode: " + x.MovementType,
                Price = $"{x.Price.ToString("N2")} €",

                Stock = x.Stock.Select(y => new StockViewModel
                {
                    StockID = y.StockID,
                    Model = y.Model,
                    InStock = y.Quantity > 0
                })
            })
            .FirstOrDefault();
        }
        public class RobotsViewModel
        {
            public string Model { get; set; }
            public string Desciption { get; set; }
            public string Price { get; set; }

            public IEnumerable<StockViewModel> Stock { get; set; }

        }
        public class StockViewModel
        {
            public int StockID { get; set; }
            public string Model { get; set; }
            public bool InStock { get; set; }
        }
    }
}
