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
            CustomerName = order.customer.UserName;
        }
        public int OrderID { get; set; }
        [Required]
        public string CustomerName { get; set; }
        public DateTime OrderDate { get; set; }
    }
}