using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Customer
    {
        public int CustomerID { get; set; }
        public string UserName { get; set; }
        public DateTime? DateRegistered { get; set; }
        public virtual List<Order> OrdersIssued { get; set; }
    }
}
