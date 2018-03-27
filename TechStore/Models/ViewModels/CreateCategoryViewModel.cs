using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TechStore.Models.ViewModels
{
    public class CreateCategoryViewModel
    {
        public CreateCategoryViewModel()
        {
            
        }
        public CreateCategoryViewModel(Category category)
        {

            Name = category.Name;
            CategoryID = category.CategoryID;
            products = category.Products;

        }
        public int CategoryID { get; set; }
        public string Name { get; set; }
        public List<Product> products { get; set; }
    }
}