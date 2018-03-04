using DataAccess.Abstract;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Services
{
    public class CategoryService : IStoreService<Category>, ICategoryService
    {
        private CategoryRepository categoryRepository;
        public CategoryService()
        {
            categoryRepository = new CategoryRepository();
        }
        public void AddItem(Category entity)
        {
            categoryRepository.Add(entity);
        }

        public void DeleteItem(Category entity)
        {
            categoryRepository.Delete(entity);
        }

        public async  Task DeleteItemByIDAsync(int id)
        {
        await    categoryRepository.DeleteByIDAsync(id);
        }

        public void EditItem(Category entity)
        {
            categoryRepository.Edit(entity);
        }

        public async Task<List<Category>> GetAllItemsAsync()
        {
           return await categoryRepository.GetAllAsync();
        }

        public async Task<Category> GetItemByIDAsync(int id)
        {
            return await categoryRepository.GetByIDAsync(id);
        }

        public async Task SaveAllItemsAsync()
        {
            await categoryRepository.SaveAll();
        }
    }

    internal interface ICategoryService
    {
    }
}
