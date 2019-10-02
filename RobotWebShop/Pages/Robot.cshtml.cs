using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RobotWebShop.Application.Cart;
using RobotWebShop.Application.Robots;
using RobotWebShop.Database;
using RobotWebShop.Models;

namespace RobotWebShop.Pages
{
    public class RobotModel : PageModel
    {
        private AppDbContext _ctx;

        public RobotModel(AppDbContext ctx)
        {
            _ctx = ctx;
        }

        [BindProperty]
        public AddToCart.Request CartViewModel { get; set; }

        public GetRobot.RobotsViewModel Robot { get; set; }
        public async Task<IActionResult> OnGet( string model)
        {
            Robot = await new GetRobot(_ctx).Do(model);
            if(Robot == null)
            {
                return RedirectToPage("Index");
            }
            else
            {
                return Page();
            }
        }

        public async Task<IActionResult> OnPost()
        {
            var stockAdded = await new AddToCart(HttpContext.Session, _ctx).Do(CartViewModel);

            if (stockAdded)
            {
                return RedirectToPage("Cart");
            }
            else
            {
                //add Warning
                return Page();
            }

            
        }
    }
}
