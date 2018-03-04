using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public class CategoryRepository : StoreBaseRepository<Category>, ICategoryRepository
    {

        public CategoryRepository() : base()
        {

        }
    }
    //Specific to categories
    internal interface ICategoryRepository
    {
    }
}
