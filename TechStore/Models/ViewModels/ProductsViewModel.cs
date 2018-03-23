using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TechStore.Models.ViewModels
{
    public class ProductsViewModel
    {
        public ProductsViewModel(Product product)
        {
            Name = product.Name;
            LeftInStock = product.LeftInStock;
            Description = product.Description;
            //Note: if no category defined use Not Defined
            CategoryName = product.Category != null ? product.Category.Name : "Not Defined";
            ProductID = product.ProductID;
        }
        public int ProductID { get; set; }
        public int LeftInStock { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string CategoryName { get; set; }

        


    }
}