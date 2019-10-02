using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using RobotWebShop.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotWebShop.Application.Cart
{
    public class GetCustomerInformation
    {
        private ISession _session;

        public GetCustomerInformation(ISession session)
        {
            _session = session;
        }

        public Response Do()
        {
            var stringObject = _session.GetString("Customer-Info");

            if (string.IsNullOrEmpty(stringObject))
            {
                return null;
            }

            var customerInformation = JsonConvert.DeserializeObject<CustomerInformation>(stringObject);



            return new Response
            {
                FirstName = customerInformation.FirstName,
                LastName = customerInformation.LastName,
                email = customerInformation.email,
                PhoneNumber = customerInformation.PhoneNumber,
                Address1 = customerInformation.Address1,
                Address2 = customerInformation.Address2,
                City = customerInformation.City,
                PostCode = customerInformation.PostCode
            };
        }

        public class Response
        {

            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string email { get; set; }
            public string PhoneNumber { get; set; }
            public string Address1 { get; set; }
            public string Address2 { get; set; }
            public string City { get; set; }
            public string PostCode { get; set; }
        }
    }
}
