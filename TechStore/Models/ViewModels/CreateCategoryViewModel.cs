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

        }
        public int CategoryID { get; set; }
        public string Name { get; set; }
    }
}