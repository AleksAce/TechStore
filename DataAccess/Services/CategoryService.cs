using DataAccess.Abstract;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Services
{
    public interface ICategoryService : IStoreService<Category>
    {
    }
    public class CategoryService :  ICategoryService
    {
        private CategoryRepository _categoryRepository;
        public CategoryService(CategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public void AddItem(Category entity)
        {
            _categoryRepository.Add(entity);
        }

        public void DeleteItem(Category entity)
        {
            _categoryRepository.Delete(entity);
        }

        public async  Task DeleteItemByIDAsync(int id)
        {
        await    _categoryRepository.DeleteByIDAsync(id);
        }

        public void EditItem(Category entity)
        {
            _categoryRepository.Edit(entity);
        }

        public async Task<List<Category>> GetAllItemsAsync()
        {
           return await _categoryRepository.GetAllAsync();
        }

        public async Task<Category> GetItemByIDAsync(int id)
        {
            return await _categoryRepository.GetByIDAsync(id);
        }

        public async Task SaveAllItemsAsync()
        {
            await _categoryRepository.SaveAll();
        }
    }

    
}
