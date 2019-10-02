using RobotWebShop.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RobotWebShop.Application.RobotsAdmin
{
    public class GetRobot
    {
        private AppDbContext _ctx;

        public GetRobot(AppDbContext ctx)
        {
            _ctx = ctx;
        }

        public RobotViewModel Do(int id) =>
            _ctx.Robots.Where(x => x.RobotID == id).Select(x => new RobotViewModel 
            {
                RobotID = x.RobotID,
                Model = x.Model,
                Function = x.Function,
                MovementType = x.MovementType,
                Price = x.Price
            })
            .FirstOrDefault();
    }
}
