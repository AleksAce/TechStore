﻿using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public class CategoryRepository : StoreBaseRepository<Category>, ICategoryRepository
    {

        public CategoryRepository() : base()
        {

        }
        public override async Task<List<Category>> GetAll()
        {
            List<Category> categories = new List<Category>();

            try
            {
                categories= dbSet.ToList<Category>();

            }
            catch(Exception ex)
            {
                //Something went wrong
                Console.WriteLine(ex.Message);
            }
            return categories;
        }

        public async Task<List<string>> GetAllCategoryNames()
        {
            List<string> categorynames = new List<string>();
            try
            {
                //START FROM EHRE THIS DOESNT WORK
                List<Category> categories = await this.GetAll();
                foreach (var cat in categories)
                {
                    categorynames.Add(cat.Name);
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("Something went wrong", ex.Message);
            }
                return categorynames;
            
                 
        }

        public override async Task<Category> GetByIDAsync(int id)
        {
            return await dbSet.Include(x => x.Products).SingleOrDefaultAsync(c=>c.CategoryID == id);
        }

        public async Task<Category> GetByNameAsync(string name)
        {
            return await dbSet.Include(c=>c.Products).SingleOrDefaultAsync(c => c.Name == name);
        }

       
        
    }
    //Specific to categories
    public interface ICategoryRepository : IStoreRepository<Category>
    {
        
        Task<List<string>> GetAllCategoryNames();
        Task<Category> GetByNameAsync(string name);

    }
}
