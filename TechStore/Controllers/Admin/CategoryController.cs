using DataAccess.Abstract;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TechStore.Models.ViewModels;

namespace TechStore.Controllers.Admin
{
    public class CategoryController : Controller
    {
        ICategoryRepository _categoryRepository;
        IProductRepository _productRepository;

        public CategoryController(ICategoryRepository categoryRepository, IProductRepository productRepository)
        {
            _categoryRepository = categoryRepository;
            _productRepository = productRepository;
        }
        [HttpGet]
        public async Task<ActionResult> ProductsPerCategory(int categoryID)
        {
            Category category = await _categoryRepository.GetByIDAsync(categoryID);
            List<CategoryProductViewModel> cpvm = new List<CategoryProductViewModel>();
            foreach (var p in category.Products)
            {
                CategoryProductViewModel pvm = new CategoryProductViewModel(p);
                cpvm.Add(pvm);
            }
            return Json(cpvm, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public async Task<ActionResult> ProductsNotInCategory(int categoryID)
        {
            Category category = await _categoryRepository.GetByIDAsync(categoryID);
            List<Product> productsInCategory = category.Products;
            List<Product> allProducts = await _productRepository.GetAllAsync();
            
            List<Product> productsNotInCategory = allProducts.Where(p=> p.Categories == null || (p.Categories.ToList().Find(c=>c.CategoryID == categoryID)) == null).ToList();
            
            List<CategoryProductViewModel> cpvm = new List<CategoryProductViewModel>();
            foreach (var p in productsNotInCategory)
            {
                CategoryProductViewModel pvm = new CategoryProductViewModel(p);
                cpvm.Add(pvm);
            }
            return Json(cpvm, JsonRequestBehavior.AllowGet);
        }
        // GET: Category
        public async Task<ActionResult> Index()
        {
            try
            {
                List<CreateCategoryViewModel> createCategoryViewModels = new List<CreateCategoryViewModel>();
                List<Category> categories = await _categoryRepository.GetAllAsync();
                foreach (var cat in categories)
                {
                    CreateCategoryViewModel cvm = new CreateCategoryViewModel(cat);
                    createCategoryViewModels.Add(cvm);
                }
                return View("Index",createCategoryViewModels);
            }
            catch
            {
                ViewBag.Message = "Something went wrong";
                return View("Error");
            }
        }

        // GET: Category/Details/5
        public async Task<ActionResult> Details(int id)
        {
            try
            {
                Category cat = await _categoryRepository.GetByIDAsync(id);
                CreateCategoryViewModel cvm = new CreateCategoryViewModel(cat);
                return View(cvm);
            }
            catch
            {
                ViewBag.Message = "Something went wrong";
                return View("Error");
            }
        }

        // GET: Category/Create
        public ActionResult Create()
        {
            return View(new CreateCategoryViewModel());
        }

        // POST: Category/Create
        [HttpPost]
        public async Task<ActionResult> Create(CreateCategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // TODO: Add insert logic here
                    Category category = new Category()
                    {
                        Name = model.Name
                    };
                    _categoryRepository.Add(category);
                    await _categoryRepository.SaveAll();
                    return RedirectToAction("Index");
                }
                catch
                {
                    return View("Create",model);
                }
            }
            else
            {
                return View("Create",model);
            }
        }

        // GET: Category/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            Category cat = await _categoryRepository.GetByIDAsync(id);
            CreateCategoryViewModel cvm = new CreateCategoryViewModel(cat);
            return View(cvm);
        }

        // POST: Category/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(CreateCategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // TODO: Add insert logic here
                    Category category = await _categoryRepository.GetByIDAsync(model.CategoryID);
                    category.Name = model.Name;
                    //TODO:Implement products adding;
                    await _categoryRepository.SaveAll();
                    return RedirectToAction("Index");
                }
                catch
                {
                    return View("Edit",model);
                }
            }
            else
            {
                return View("Edit",model);
            }
        }

        // GET: Category/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            Category cat = await _categoryRepository.GetByIDAsync(id);
            CreateCategoryViewModel cvm = new CreateCategoryViewModel(cat);
            return View(cvm);
        }

        // POST: Category/Delete/5
        [HttpPost]
        public async Task<ActionResult> Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                Category cat = await _categoryRepository.GetByIDAsync(id);
                _categoryRepository.Delete(cat);
                await _categoryRepository.SaveAll();
                return View("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
