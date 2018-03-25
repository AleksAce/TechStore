using DataAccess.Abstract;
using Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TechStore.Models.ViewModels
{
    public class CreateProductViewModel
    {

        ICategoryRepository _categoryRepository;
        public CreateProductViewModel()
        {
            _categoryRepository = new CategoryRepository();
            //THIS DOESNT WORK.. FIX IT
            Categories = GetSelectListItems(_categoryRepository.GetAllCategoryNames().Result);
            // Categories = GetSelectListItems(new List<string>() { "No Category Specified" });
        }

        public CreateProductViewModel(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
            //THIS DOESNT WORK.. FIX IT
            Categories = GetSelectListItems(_categoryRepository.GetAllCategoryNames().Result);
           // Categories = GetSelectListItems(new List<string>() { "No Category Specified" });
        }
        public CreateProductViewModel(Product product, ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
            //THIS DOESNT WORK.. FIX IT
            Categories = GetSelectListItems(_categoryRepository.GetAllCategoryNames().Result);
           // Categories = GetSelectListItems(new List<string>() { "No Category Specified" });

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
            CategoryName = product.Category != null ? product.Category.Name : "";
           

        }

        //Note: Makes a selectlist from categoryNames;
        public List<SelectListItem> GetSelectListItems(List<string> categories)
        {
            List<SelectListItem> items = new List<SelectListItem>();
            //NOTE: Wasn't able to fetch categories
            if (categories == null || categories.Count == 0)
            {
                items.Add(new SelectListItem() { Text = "ERROR: No Categories Provided", Value = null});
                return items;
            }
            foreach (var cat in categories)
            {
                SelectListItem item = new SelectListItem()
                {
                    Text = cat,
                    Value = cat
                };
                items.Add(item);
            }
            return items;
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

        public int AvailableInStockTime { get; set; }
        public DateTime? AvailableUntill { get; set; }
        //Category Info

        public string CategoryName { get; set; }
        public List<SelectListItem> Categories { get; set; }

        public List<Order> orders = new List<Order>();
    }
}