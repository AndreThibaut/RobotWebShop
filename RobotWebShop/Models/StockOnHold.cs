using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotWebShop.Models
{
    public class StockOnHold
    {
        public int ID { get; set; }

        public string SessionID { get; set; }

        public int StockID { get; set; }
        public Stock Stock { get; set; }

        public int Quantity { get; set; }
        public DateTime ExpiryDate { get; set; }
    }
}
