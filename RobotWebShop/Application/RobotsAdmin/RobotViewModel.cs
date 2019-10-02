using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RobotWebShop.Application.RobotsAdmin
{
    public class RobotViewModel
    {
        public int RobotID { get; set; }
        public string Model { get; set; }
        public string Function { get; set; }
        public string MovementType { get; set; }
        public decimal Price { get; set; }
    }
}
