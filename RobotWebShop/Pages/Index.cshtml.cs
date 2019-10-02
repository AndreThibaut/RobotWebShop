using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RobotWebShop.Application.Robots;
using RobotWebShop.Database;

namespace RobotWebShop.UI.Pages
{
    public class IndexModel : PageModel
    {
        private AppDbContext _ctx;

        public IndexModel(AppDbContext ctx)
        {
            _ctx = ctx;
        }

        [BindProperty]
        public IEnumerable<RobotViewModel> Robots { get; set; }

        public void OnGet()
        {
            Robots = new GetRobots(_ctx).Do();
        }
    }
}
