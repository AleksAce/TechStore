using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace DataAccess.Abstract
{
    //Specific to categories
    public interface ICategoryRepository : IStoreRepository<Category>
    {

        Task<List<string>> GetAllCategoryNames();
        Task<Category> GetByNameAsync(string name);

    }
}
