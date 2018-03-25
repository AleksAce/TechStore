﻿using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TechStore.Models.ViewModels
{
    public class ProductOrderViewModel
    {
        
        public ProductOrderViewModel(Order order)
        {
            OrderID = order.OrderID;
            OrderDate = order.OrderDate.ToLongDateString();
        }
        public int OrderID { get; set; }
        public string OrderDate { get; set; }
        //Users
    }
}