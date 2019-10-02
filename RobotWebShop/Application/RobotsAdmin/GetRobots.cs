using RobotWebShop.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RobotWebShop.Application.RobotsAdmin
{
    public class GetRobots
    {
        private AppDbContext _ctx;

        public GetRobots(AppDbContext ctx)
        {
            _ctx = ctx;
        }

        public IEnumerable<RobotViewModel> Do() =>
            _ctx.Robots.ToList().Select(x => new RobotViewModel 
            {
                RobotID = x.RobotID,
                Model = x.Model,
                Function = x.Function,
                MovementType = x.MovementType,
                Price = x.Price
            });
    }
}
