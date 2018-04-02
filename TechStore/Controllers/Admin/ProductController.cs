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
        IOrderRepository _orderRepository;

        public ProductController(IProductRepository productRepository, ICategoryRepository categoryRepository,
            IOrderRepository orderRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _orderRepository = orderRepository;
        }
        
        //CATEGORIES
        [HttpGet]
        public async Task<ActionResult> CategoriesPerProduct(int productID)
        {
            Product product = await _productRepository.GetByIDAsync(productID);
            List<ProductCategoryViewModel> pcvm = new List<ProductCategoryViewModel>();
            foreach (var c in product.Categories)
            {
                ProductCategoryViewModel cvm = new ProductCategoryViewModel(c);
                pcvm.Add(cvm);
            }
            return Json(pcvm, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public async Task<ActionResult> CategoriesNotInProduct(int productID)
        {
            Product product = await _productRepository.GetByIDAsync(productID);
            List<Category> categoriesInproduct = product.Categories;
            List<Category> allCategories = await _categoryRepository.GetAllAsync();

            List<Category> categoriesNotInproduct = allCategories.Where(c => c.Products == null || (c.Products.ToList().Find(p => p.ProductID == productID)) == null).ToList();

            List<ProductCategoryViewModel> pcvm = new List<ProductCategoryViewModel>();
            foreach (var c in categoriesNotInproduct)
            {
                ProductCategoryViewModel cvm = new ProductCategoryViewModel(c);
                pcvm.Add(cvm);
            }
            return Json(pcvm, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public async Task<string> RemoveProductFromCategory(int productID, int categoryID)
        {
            try
            {
                await _productRepository.RemoveProductFromCategoryAsync(productID, categoryID);
                await _productRepository.SaveAll();
                return "Product successfully removed";
            }
            catch
            {
                return "Product failed to be removed";
            }
        }
        [HttpGet]
        public async Task<string> AddProductToCategory(int productID, int categoryID)
        {
            //Todo: FInd out why it refreshes
            try
            {
                await _productRepository.AddProductToCategoryAsync(productID, categoryID);
                await _productRepository.SaveAll();
                return "Product successfully added";
            }
            catch
            {
                return "Product failed to be added";
            }
        }
        [HttpGet]
        public async Task<ActionResult> GetCategories(int id)
        {
            Product prod = await _productRepository.GetByIDAsync(id);
            List<ProductCategoryViewModel> pcvm = new List<ProductCategoryViewModel>();
            foreach (var c in prod.Categories)
            {
                ProductCategoryViewModel cvm = new ProductCategoryViewModel(c);
                pcvm.Add(cvm);
            }
            return Json(pcvm, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
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
            List<Product> products = await _productRepository.GetAllAsync();
            List<CreateProductViewModel> createProductViewModels = new List<CreateProductViewModel>();
            foreach(var p in products)
            {
                CreateProductViewModel createProductViewModel = new CreateProductViewModel(p);
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
                CreateProductViewModel createProductViewModel = new CreateProductViewModel(product);
                return View("Details",createProductViewModel);
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
            return View("Create",new CreateProductViewModel());
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
                    foreach (var cat in model.Categories)
                    {
                        if (!product.Categories.Contains(cat))
                        {
                            await _productRepository.AddProductToCategoryAsync(product.Name, cat.Name);
                        }
                    }
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
            CreateProductViewModel createProductViewModel = new CreateProductViewModel(product);
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
                   
                    Product product =  await _productRepository.GetByIDAsync(model.ProductID);
                    product.DateUpdated = DateTime.Now;
                    product.Name = model.Name;
                    product.PhotoURL = model.PhotoURL;
                    product.Price = model.Price;
                    product.Description = model.Description;
                    product.IsInStock = true;
                    product.LeftInStock = model.LeftInStock;
                    product.Manufacturer = model.Manufacturer;
                    foreach (var cat in model.Categories)
                    {
                        if (!product.Categories.Contains(cat))
                        {
                            await _productRepository.AddProductToCategoryAsync(product.Name, cat.Name);
                        }
                    }
                    await _productRepository.SaveAll();
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

        // GET: Product/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
           
            Product product = await _productRepository.GetByIDAsync(id);
            CreateProductViewModel createProductViewModel = new CreateProductViewModel(product);
          
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
                return View("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
