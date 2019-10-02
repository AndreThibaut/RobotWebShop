using Microsoft.AspNetCore.Mvc;
using RobotWebShop.Application.Cart;
using RobotWebShop.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotWebShop.ViewComponents
{
    public class CartViewComponent : ViewComponent
    {
        private AppDbContext _ctx;

        public CartViewComponent(AppDbContext ctx)
        {
            _ctx = ctx;
        }

        public IViewComponentResult Invoke(string view = "Default") 
        {
            if(view == "Small")
            {
                var totalPrice = new GetCart(HttpContext.Session, _ctx).Do().Sum(x => x.RealPrice * x.Quantity);
                return View(view, $"{totalPrice} €");
            }
            return View(view, new GetCart(HttpContext.Session, _ctx).Do());
        }
    }
}
