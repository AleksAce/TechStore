using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace DataAccess.Abstract
{
    //Specific to Products
    public interface IProductRepository : IStoreRepository<Product>
    {
        Task<Product> GetProductByNameAsync(string name);
        Task AddProductToCategoryAsync(int productID, int categoryID);
        Task RemoveProductFromCategoryAsync(int productID, int categoryID);
        Task AddProductToCategoryAsync(string productName, string categoryName);
        Task<List<Product>> GetMainPageProducts();
    }
}
