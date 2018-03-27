using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TechStore.Models.ViewModels
{
    public class OrderViewModel
    {
        public OrderViewModel(Order order)
        {
            OrderID = order.OrderID;
            OrderDate = order.OrderDate.ToLongDateString();
            products = new List<Product>();
            customer = order.customer;
        }
        public int OrderID { get; set; }
        public string OrderDate { get; set; }
        //??
        public List<Product> products { get; set; }

        public Customer customer { get; set; }
    }
}