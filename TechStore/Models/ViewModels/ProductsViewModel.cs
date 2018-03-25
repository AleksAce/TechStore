using DataAccess.Abstract;
using Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
            AvailableInStockTime = product.AvailableInStockTime;
            Price = product.Price;
           
        }
        public int ProductID { get; set; }
        public int LeftInStock { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        [DataType(DataType.Currency)]
        public float Price { get; set; }
        [Display(Name ="Days Left")]
        public int AvailableInStockTime { get; set; }

        //Categories
        public string CategoryName { get; set; }





    }
}