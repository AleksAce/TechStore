using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TechStore.Models.ViewModels
{
    public class OrderProductViewModel
    {
        public OrderProductViewModel(Product product)
        {
            ProductID = product.ProductID;
            ProductName = product.Name;
        }
        public int ProductID { get; set; }
        public string ProductName { get; set; }
    }
}