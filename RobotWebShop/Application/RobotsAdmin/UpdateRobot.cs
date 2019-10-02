using RobotWebShop.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RobotWebShop.Application.RobotsAdmin
{
    public class UpdateRobot
    {
        private AppDbContext _context;

        public UpdateRobot(AppDbContext context)
        {
            _context = context;
        }

        public async Task<RobotViewModel> Do(RobotViewModel vm) 
        {
            var robot = _context.Robots.FirstOrDefault(x => x.RobotID == vm.RobotID);

            robot.Model = vm.Model;
            robot.Function = vm.Function;
            robot.MovementType = vm.MovementType;
            robot.Price = vm.Price;


            await _context.SaveChangesAsync();
            return new RobotViewModel
            {
                RobotID = robot.RobotID,
                Model = robot.Model,
                Function = robot.Function,
                MovementType = robot.MovementType,
                Price = robot.Price,
            };
        }
    }
}
