
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Models
{
    public class Order
    {
        public int OrderID { get; set; }
        public DateTime OrderDate { get; set; }

        //ProductInfo
        public virtual List<Product> ProductsOrdered { get; set; }

        public virtual Customer customer { get; set; }
        //CUSTOMER TODO:

            //Added master comment
            //Added branch comment
        public Order()
        {
            
        }
    }
}
