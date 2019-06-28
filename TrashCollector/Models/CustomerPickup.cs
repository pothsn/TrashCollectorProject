using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrashCollector.Models
{
    public class CustomerPickup
    {
        public Customer Customer { get; set; }

        public Pickup Pickup { get; set; }

        public List<Customer> Customers { get; set; }

        public List<Pickup> Pickups { get; set; }
    }
    
}