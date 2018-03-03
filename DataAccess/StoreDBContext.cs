using DataAccess.Services;
using Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DataAccess
{

    public class StoreDBContext : DbContext
    {

        public StoreDBContext() : base("StoreDBContext")
        {
            //  Database.SetInitializer(new StoreDBInitializer());
            Database.SetInitializer(new DropCreateDatabaseAlways<StoreDBContext>());
            //this.Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }


    }


    public class StoreDBInitializer : DropCreateDatabaseAlways<StoreDBContext>
    {
        protected override void Seed(StoreDBContext context)
        {
            ProductService productService = new ProductService();

            context.Categories.Add(new Category() { Name = "Default1" });
            context.Products.Add(new Product() { Name = "Product1" });
            context.Products.Add(new Product() { Name = "Product2" });
            context.Products.Add(new Product() { Name = "Product3" });
            context.Products.Add(new Product() { Name = "Product4" });
            context.Products.Add(new Product() { Name = "Product5" });

            

            foreach (Product prod in context.Products.ToList())
            {
                productService.AddToCategory(prod.ProductID, 1).RunSynchronously();
            }
            base.Seed(context);
        }

    }
}
