using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RobotWebShop.Models
{
    public class OrderStock
    {
        public int OrderID { get; set; }
        public Order Order { get; set; }

        public int StockID { get; set; }
        public Stock Stock { get; set; }

        public int Quantity { get; set; }
    }
}
