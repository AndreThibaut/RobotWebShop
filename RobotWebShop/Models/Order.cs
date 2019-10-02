using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RobotWebShop.Models
{
    public class Order
    {
        public int OrderID { get; set; }
        public string OrderRef { get; set; }
        public string StripeRef { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string email { get; set; }
        public string PhoneNumber { get; set; }

        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string PostCode { get; set; }

        public ICollection<OrderStock> OrderStocks { get; set; }
    }
}
