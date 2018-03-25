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
    public class ProductController : Controller
    {
        IProductRepository _productRepository;
        ICategoryRepository _categoryRepository;
        public ProductController(IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }
        public async Task<ActionResult> GetOrders(int id)
        {
            Product prod = await _productRepository.GetByIDAsync(id);
            List<ProductOrderViewModel> povm = new List<ProductOrderViewModel>();
            foreach(var o in prod.Orders)
            {
                ProductOrderViewModel ovm = new ProductOrderViewModel(o);
                povm.Add(ovm);
            }
            return Json(povm, JsonRequestBehavior.AllowGet);
        }
        // GET: Product
        public async Task<ActionResult> Index()
        {
            List<Product> products = await _productRepository.GetAll();
            List<CreateProductViewModel> createProductViewModels = new List<CreateProductViewModel>();
            foreach(var p in products)
            {
                CreateProductViewModel createProductViewModel = new CreateProductViewModel(p, _categoryRepository);
                createProductViewModels.Add(createProductViewModel);
            }

            return View("Index",createProductViewModels);
        }

        // GET: Product/Details/5
        public async Task<ActionResult> Details(int id)
        {
            Product product = await _productRepository.GetByIDAsync(id);
            if (product != null)
            {
                CreateProductViewModel createProductViewModel = new CreateProductViewModel(product, _categoryRepository);
                return View(createProductViewModel);
            }
            else
            {
                ViewBag.Message = "Product Not Found";
                return View("Error");
            }
        }

        // GET: Product/Create
        public ActionResult Create()
        {
            return View("Create",new CreateProductViewModel(_categoryRepository));
        }

        // POST: Product/Create
        [HttpPost]
        public async Task<ActionResult> Create(CreateProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                
                    Product product = new Product()
                    {
                        DateAdded = DateTime.Now,
                        Name = model.Name,
                        PhotoURL = model.PhotoURL,
                        Price = model.Price,
                        Description = model.Description,
                        IsInStock = true,
                        LeftInStock = model.LeftInStock,
                        Manufacturer = model.Manufacturer,
                    };
                    _productRepository.Add(product);
                    await _productRepository.SaveAll();
                    await _productRepository.AddProductToCategoryAsync(product.Name, model.CategoryName);
                    await _productRepository.SaveAll();
                    return RedirectToAction("Index");
                }
                catch(Exception ex)
                {
                    return View("Create",model);
                }
            }
            else
            {
                return View("Create",model);
            }
        }

        // GET: Product/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            Product product = await _productRepository.GetByIDAsync(id);
            CreateProductViewModel createProductViewModel = new CreateProductViewModel(product, _categoryRepository);
            return View(createProductViewModel);
        }

        // POST: Product/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(CreateProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Category category = await _categoryRepository.GetByNameAsync(model.CategoryName);

                    Product product =  await _productRepository.GetByIDAsync(model.ProductID);
                    product.DateUpdated = DateTime.Now;
                    product.Name = model.Name;
                    product.PhotoURL = model.PhotoURL;
                    product.Price = model.Price;
                    product.Description = model.Description;
                    product.IsInStock = true;
                    product.LeftInStock = model.LeftInStock;
                    product.Manufacturer = model.Manufacturer;
                    await _productRepository.AddProductToCategoryAsync(product.ProductID, category.CategoryID);
                    await _productRepository.SaveAll();
                    return RedirectToAction("Index");
                }
                catch
                {
                    return View(model);
                }
            }
            else
            {
                return View(model);
            }
        }

        // GET: Product/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
           
            Product product = await _productRepository.GetByIDAsync(id);
            CreateProductViewModel createProductViewModel = new CreateProductViewModel(product, _categoryRepository);
          
            return View(createProductViewModel);
        }

        // POST: Product/Delete/5
        [HttpPost]
        public async Task<ActionResult> Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                Product product = await _productRepository.GetByIDAsync(id);
                _productRepository.Delete(product);
                await _productRepository.SaveAll();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
