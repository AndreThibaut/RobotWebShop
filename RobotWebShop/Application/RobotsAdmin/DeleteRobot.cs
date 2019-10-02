using RobotWebShop.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RobotWebShop.Application.RobotsAdmin
{
    public class DeleteRobot
    {
        private AppDbContext _context;

        public DeleteRobot(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Do(int id) 
        {
            var Robot = _context.Robots.FirstOrDefault(x => x.RobotID == id);
            _context.Robots.Remove(Robot);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
