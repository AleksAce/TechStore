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
    public interface IProductService :IStoreService<Product>
    {
        Product GetProductByName(string name);
        Task AddToCategory(int productID, int categoryID);
        Task RemoveFromCategory(int productID, int categoryID);
    }
    
    public class ProductService :  IProductService
    {
        //ALWAYS use specific repository, can override the default methods
        private ProductRepository _productRepository;
        private CategoryRepository _categoryRepository;
        public ProductService(ProductRepository productRepository, CategoryRepository categoryRepository) 
        {
            _productRepository = productRepository;
            _categoryRepository =categoryRepository;
           
        }
        public Product GetProductByName(string name)
        {
            return _productRepository.GetProductByName(name);
        }
        public async Task AddToCategory(int productID, int categoryID)
        {
            Product prod = await _productRepository.GetByIDAsync(productID);
            Category cat = await _categoryRepository.GetByIDAsync(categoryID);
            prod.Category = cat;
            await _productRepository.SaveAll();
        }

        public async Task RemoveFromCategory(int productID, int categoryID)
        {
            Product prod = await _productRepository.GetByIDAsync(productID);
            Category cat = await _categoryRepository.GetByIDAsync(categoryID);
            //prod.Category = null;
            cat.Products.Remove(prod);
            await _categoryRepository.SaveAll();
        }

        public void AddItem(Product entity)
        {
            _productRepository.Add(entity);
        }

        public void EditItem(Product entity)
        {
            _productRepository.Edit(entity);
        }

        public void DeleteItem(Product entity)
        {
            _productRepository.Delete(entity);
        }

        public async Task DeleteItemByIDAsync(int id)
        {
           await _productRepository.DeleteByIDAsync(id);
        }

        public async Task<Product> GetItemByIDAsync(int id)
        {
            return await _productRepository.GetByIDAsync(id);
        }

        public async Task<List<Product>> GetAllItemsAsync()
        {
            return await _productRepository.GetAllAsync();
        }

        public async Task SaveAllItemsAsync()
        {
            await _productRepository.SaveAll();
        }
    }
}
