using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    //Note: Fixed price for the specified Order
    public class ProductWithCompletedOrder
    {
        [Key]
        public int ProductWithCompletedOrderID { get; set; }
        public int productID { get; set; }
        public virtual Order order { get; set; }
        public float PricePayed { get; set; }
    }
}
