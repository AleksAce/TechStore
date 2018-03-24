using DataAccess.Abstract;
using Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TechStore.Models.ViewModels
{
    public class CreateProductViewModel
    {


        public CreateProductViewModel()
        {
           
        }
        public CreateProductViewModel(Product product)
        {
           

            ProductID = product.ProductID;
            Name = product.Name;
            Manufacturer = product.Manufacturer;
            Description = product.Description;
            PhotoURL = product.PhotoURL;
            Price = product.Price;
            IsInStock = product.IsInStock;
            LeftInStock = product.LeftInStock;
            AvailableInStockTime = product.AvailableInStockTime;
            DateAdded = product.DateAdded;
            DateUpdated = product.DateUpdated;
            CategoryName = "";
        }
        //Basic Info
        public int ProductID { get; set; }
        [Required]
        public string Name { get; set; }
        public string Manufacturer { get; set; }
        public string Description { get; set; }
        public DateTime? DateAdded { get; set; }
        public DateTime? DateUpdated { get; set; }
        //Todo: Make this add from directory with <input type=file>
        public string PhotoURL { get; set; }
        [Required]
        public float Price { get; set; }
        //Stock info
        public bool IsInStock { get; set; }
        public int LeftInStock { get; set; }
        public DateTime? AvailableInStockTime { get; set; }
        //Category Info

        public string CategoryName { get; set; }

    }
}