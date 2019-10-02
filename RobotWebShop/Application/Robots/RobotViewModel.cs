using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RobotWebShop.Application.Robots
{
    public class RobotViewModel
    {
        public string Model { get; set; }
        public string Function { get; set; }
        public string MovementType { get; set; }
        public decimal Price { get; set; }
    }
}
