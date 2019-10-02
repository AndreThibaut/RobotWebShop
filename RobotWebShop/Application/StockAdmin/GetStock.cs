using Microsoft.EntityFrameworkCore;
using RobotWebShop.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RobotWebShop.Application.StockAdmin
{
    public class GetStock
    {
        private AppDbContext _ctx;

        public GetStock(AppDbContext ctx)
        {
            _ctx = ctx;
        }

        public IEnumerable<RobotViewModel> Do()
        {
            var stock = _ctx.Robots
                .Include(x => x.Stock)
                .Select(x => new RobotViewModel
                {
                    RobotID = x.RobotID,
                    Model = x.Model,
                    Stock = x.Stock.Select(y => new StockViewModel 
                { 
                    StockID = y.StockID, 
                    RobotID = y.RobotID,
                    Model = y.Model,
                    Quantity = y.Quantity
                })
                })
                .ToList();


            return stock;
        }

        public class StockViewModel
        {
            public int StockID { get; set; }
            public int RobotID { get; set; }
            public string Model { get; set; }
            public int Quantity { get; set; }
        }

        public class RobotViewModel
        {
            public int RobotID { get; set; }
            public string Model { get; set; }
            public IEnumerable<StockViewModel> Stock { get; set; }
        }
    }
}
