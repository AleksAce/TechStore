using Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TechStore.Models.ViewModels
{
    public class CreateOrderViewModel
    {
        public CreateOrderViewModel()
        {

        }
        public CreateOrderViewModel(Order order)
        {
            OrderID = order.OrderID;
            if (order.customer != null)
            {
                CustomerName = order.customer.UserName;
            }
            else
            {
                CustomerName = "This order has no customer.";
            }
            OrderDate = order.OrderDate;
        }
        public int OrderID { get; set; }
        [Required]
        public string CustomerName { get; set; }
        public DateTime OrderDate { get; set; }
    }
}