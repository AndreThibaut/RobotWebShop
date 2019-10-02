using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RobotWebShop.Application.Cart;
using RobotWebShop.Database;

namespace RobotWebShop.Pages
{
    public class CartModel : PageModel
    {
        private AppDbContext _ctx;

        public CartModel(AppDbContext ctx)
        {
            _ctx = ctx;
        }

        public IEnumerable<GetCart.Response> Cart{ get; set; }

        public IActionResult OnGet()
        {
            Cart = new GetCart(HttpContext.Session, _ctx).Do();

            return Page();
        }
    }
}
