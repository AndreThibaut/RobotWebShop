using RobotWebShop.Database;
using RobotWebShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RobotWebShop.Application.Robots
{
    public class CreateRobot
    {
        private AppDbContext _context;

        public CreateRobot(AppDbContext context)
        {
            _context = context;
        }

        public async Task<RobotViewModel> Do(RobotViewModel vm)
        {
            var robot = new Robot
            {
                Model = vm.Model,
                Function = vm.Function,
                MovementType = vm.MovementType,
                Price = vm.Price
            };
            _context.Robots.Add(robot);
            await _context.SaveChangesAsync();

            return new RobotViewModel
            {
                Model = robot.Model,
                Function = robot.Function,
                MovementType = robot.MovementType,
                Price = robot.Price
            };
        }
    }
}
