using DataAccess.Abstract;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Services
{
    //Specific to products
    public interface IProductService
    {
        Product GetProductByName(string name);
        Task AddToCategory(int productID, int categoryID);
    }
    
    public class ProductService : IStoreService<Product>, IProductService
    {
        //ALWAYS use specific repository, can override the default methods
        private ProductRepository productRepository;
        public ProductService() 
        {
            productRepository = new ProductRepository();
           
        }
        public Product GetProductByName(string name)
        {
            return productRepository.GetProductByName(name);
        }
        public async Task AddToCategory(int productID, int categoryID)
        {
           await productRepository.AddToCategory(productID, categoryID);
        }

        public void AddItem(Product entity)
        {
            productRepository.Add(entity);
        }

        public void EditItem(Product entity)
        {
            productRepository.Edit(entity);
        }

        public void DeleteItem(Product entity)
        {
            productRepository.Delete(entity);
        }

        public async Task DeleteItemByIDAsync(int id)
        {
           await productRepository.DeleteByIDAsync(id);
        }

        public async Task<Product> GetItemByIDAsync(int id)
        {
            return await productRepository.GetByIDAsync(id);
        }

        public async Task<List<Product>> GetAllItemsAsync()
        {
            return await productRepository.GetAllAsync();
        }

        public async Task SaveAllItemsAsync()
        {
            await productRepository.SaveAll();
        }
    }
}
