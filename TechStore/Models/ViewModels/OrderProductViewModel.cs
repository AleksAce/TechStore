using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TechStore.Models.ViewModels
{
    public class OrderProductViewModel
    {
        public OrderProductViewModel()
        {
           
        }
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public float Price { get; set; }
        public int ProductInfoID { get; set; }
    }
}