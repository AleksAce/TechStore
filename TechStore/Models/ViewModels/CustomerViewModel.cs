using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TechStore.Models.ViewModels
{
    public class CustomerViewModel
    {
        public CustomerViewModel()
        {

        }
        public CustomerViewModel(Customer customer)
        {
            CustomerID = customer.CustomerID;
            DateRegistered = customer.DateRegistered;
            UserName = customer.UserName;
            //Might be a bug
            OrdersIssued = customer.OrdersIssued;
            
        }
        public int CustomerID { get; set; }
        public string UserName { get; set; }
        public DateTime? DateRegistered { get; set; }
        //??
        

        public List<Order> OrdersIssued { get; set; }
    }
}