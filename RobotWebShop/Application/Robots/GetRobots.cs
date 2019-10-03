using Microsoft.EntityFrameworkCore;
using RobotWebShop.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RobotWebShop.Application.Robots
{
    public class GetRobots
    {
        private AppDbContext _ctx;

        public GetRobots(AppDbContext ctx)
        {
            _ctx = ctx;
        }

        public IEnumerable<RobotViewModel> Do() =>
            _ctx.Robots
            .Include(x => x.Stock)
            .Select(x => new RobotViewModel 
            {
                Model = x.Model,
                Function = x.Function,
                MovementType = x.MovementType,
                Price = x.Price,

                StockCount = x.Stock.Sum(y => y.Quantity)
            })
            .ToList();

        public class RobotViewModel
        {
            public string Model { get; set; }
            public string Function { get; set; }
            public string MovementType { get; set; }
            public decimal Price { get; set; }
            public int StockCount { get; set; }

        }
    }
}
