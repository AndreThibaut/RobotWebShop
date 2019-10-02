using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RobotWebShop.Application.Orders;
using RobotWebShop.Database;

namespace RobotWebShop.Pages
{
    public class OrderModel : PageModel
    {
        private AppDbContext _ctx;

        public OrderModel(AppDbContext ctx)
        {
            _ctx = ctx;    
        }

        public GetOrders.Response Order { get; set; }

        public void OnGet(string reference)
        {
            Order = new GetOrders(_ctx).Do(reference);
        }
    }
}
