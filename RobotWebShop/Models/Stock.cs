using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RobotWebShop.Models
{
    public class Stock
    {
        public int StockID { get; set; }
        public string Model { get; set; }
        public int Quantity { get; set; }

        public int RobotID { get; set; }
        public Robot Robot { get; set; }

        public ICollection<OrderStock> OrderStocks { get; set; }
    }
}
