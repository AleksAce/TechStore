﻿using Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Abstract;

namespace DataAccess.Repositories
{
    //StoreBase Repository Implements the IStoreRepository Interface, so no worries
    public class ProductRepository : StoreBaseRepository<Product>  , IProductRepository
    {
        public ProductRepository() : base()
        {
            
        }
        
        
        public  Task<Product> GetProductByNameAsync(string name)
        {
                return  dbSet.Include(p => p.Categories).SingleOrDefaultAsync(p => p.Name == name);
        }
        public override Task<List<Product>> GetAllAsync()
        {
           return dbSet.Include(p => p.Categories).ToListAsync();
           
            
        }
        public async override Task<Product> GetByIDAsync(int id)
        {
                return await dbSet.Include(p => p.Categories).SingleOrDefaultAsync(p => p.ProductID == id);
        }

        public async Task AddProductToCategoryAsync(int productID, int categoryID)
        {
            try
            {
                Product prod = await GetByIDAsync(productID);
                Category cat = await context.Categories.FindAsync(categoryID);
                prod.Categories.Add(cat);
                //cat.Products.Add(prod);
            }catch(Exception ex)
            {
                //Could not Find Product or Category
                return;
            }
        }

        public async Task RemoveProductFromCategoryAsync(int productID, int categoryID)
        {
            try
            {
                Product prod = await GetByIDAsync(productID);
                Category cat = await context.Categories.FindAsync(categoryID);
                prod.Categories.Remove(cat);
                
            }catch(Exception ex)
            {
                //Could not find product or category
                return;
            }
        }

        public async Task AddProductToCategoryAsync(string productName, string categoryName)
        {
            try
            {
                Product prod = await GetProductByNameAsync(productName);
                Category cat = await context.Categories.SingleOrDefaultAsync(c=>c.Name == categoryName);
                prod.Categories.Add(cat);
                //cat.Products.Add(prod);
            }
            catch (Exception ex)
            {
                //Could not Find Product or Category
                return;
            }
        }

        public Task<List<Product>> GetMainPageProducts()
        {
           return dbSet.Where(p => p.isForMainPage == true).ToListAsync();
        }
    }
}
