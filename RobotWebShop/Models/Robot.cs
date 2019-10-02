using System;
using System.Collections.Generic;
using System.Text;

namespace RobotWebShop.Models
{
    public class Robot
    {
        public int RobotID { get; set; }
        public string Model { get; set; }
        public string Function { get; set; }
        public string MovementType { get; set; }
        public decimal Price { get; set; }

        public ICollection<Stock> Stock { get; set; }
    }
}
