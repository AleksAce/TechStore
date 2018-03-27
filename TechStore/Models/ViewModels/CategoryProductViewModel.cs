using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TechStore.Models.ViewModels
{
    public class CategoryProductViewModel
    {
        public CategoryProductViewModel(Product product)
        {
            ProductID = product.ProductID;
            ProductName = product.Name;
        }
        public int ProductID { get; set; }
        public string ProductName { get; set; }
    }
    public class ProductCategoryViewModel
    {
        public ProductCategoryViewModel(Category category)
        {
            CategoryID = category.CategoryID;
            CategoryName = category.Name;
        }
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
    }
}