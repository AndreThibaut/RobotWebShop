using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using RobotWebShop.Application.Cart;
using RobotWebShop.Application.Orders;
using RobotWebShop.Database;
using Stripe;

namespace RobotWebShop.Pages.Checkout
{
    public class PaymentModel : PageModel
    {
        
        public string PublicKey { get; }
        private AppDbContext _ctx;

        public PaymentModel(IConfiguration config, AppDbContext ctx)
        {
            //PublicKey = config["Stripe:PublicKey"].ToString();
            _ctx = ctx;
        }

        

        public IActionResult OnGet()
        {
            var information = new GetCustomerInformation(HttpContext.Session).Do();

            if (information == null)
            {
                return RedirectToPage("/Checkout/CustomerInformation");
            }
            return Page();
        }

        public async Task<IActionResult> OnPost(string stripeEmail, string stripeToken)
        {
            var customers = new CustomerService();
            var charges = new ChargeService();

            var CartOrder = new GetOrder(HttpContext.Session, _ctx).Do();

            var customer = customers.Create(new CustomerCreateOptions
            {
                Email = stripeEmail,
                SourceToken = stripeToken
            });

            var charge = charges.Create(new ChargeCreateOptions
            {
                Amount = CartOrder.GetTotalCharge(),
                Description = "Sample Charge",
                Currency = "eur",
                CustomerId = customer.Id
            });

            var sessionID = HttpContext.Session.Id;

#pragma warning disable CA2007 // Consider calling ConfigureAwait on the awaited task
            await new CreateOrder(_ctx).Do(new CreateOrder.Request 
            {
                StripeRef = charge.Id,
                SessionID = sessionID,

                FirstName = CartOrder.CustomerInformation.FirstName,
                LastName = CartOrder.CustomerInformation.LastName,
                email = CartOrder.CustomerInformation.email,
                PhoneNumber = CartOrder.CustomerInformation.PhoneNumber,
                Address1 = CartOrder.CustomerInformation.Address1,
                Address2 = CartOrder.CustomerInformation.Address2,
                City = CartOrder.CustomerInformation.City,
                PostCode = CartOrder.CustomerInformation.PostCode,

                Stocks = CartOrder.Robots.Select(x => new CreateOrder.Stock 
                { 
                    StockID = x.StockID,
                    Quantity = x.Quantity
                }).ToList()
            });
#pragma warning restore CA2007 // Consider calling ConfigureAwait on the awaited task
            HttpContext.Session.Clear();
            return RedirectToPage("/Index");
        }
    }
}
