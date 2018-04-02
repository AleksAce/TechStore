using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TechStore.Models.ViewModels
{
    public class CustomerOrderViewModel
    {
        public CustomerOrderViewModel(Order order)
        {
            OrderID = order.OrderID;
            OrderDate = order.OrderDate.ToLongDateString();
            CustomerUserName = order.customer.UserName;
        }
        public int OrderID { get; set; }
        public string OrderDate { get; set; }
        public string CustomerUserName { get; set; }
        public float FullPrice { get; set; }
    }
}